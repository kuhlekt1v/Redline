/*
    File name: AlertPage.xaml.cs
    Purpose:   Send information from database and location through sms.
    Author:    Amaris Sneed
    Version:   1.0.0
*/

using RedlineApp.Model;
using RedlineApp.Persistence;
using SQLite;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace RedlineApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertPage : ContentPage
    {

        private SQLiteConnection _connection;


        public AlertPage()
        {


            InitializeComponent();

            _connection = DependencyService.Get<ISQLiteInterface>().GetConnection();
            _connection.CreateTable<UserAccount>();
            _connection.CreateTable<Precondition>();
            _connection.CreateTable<Allergy>();
            _connection.CreateTable<Prescription>();

        }

        public async void SendAlertButton_Clicked(object sender, EventArgs e)
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var location = await Geolocation.GetLocationAsync(request);

            var activeUser = _connection.Table<UserAccount>().Where(x => x.ActiveUser).FirstOrDefault();
            var allergies = _connection.Table<Allergy>().Where(x => x.UserId == activeUser.Id).ToList();
            var preconditions = _connection.Table<Precondition>().Where(x => x.UserId == activeUser.Id).ToList();
            var prescriptions = _connection.Table<Prescription>().Where(x => x.UserId == activeUser.Id).ToList();

            string textAllergies = " ";
            foreach (var item in allergies)
            {
                textAllergies += item.AllergyType + ",";
            }

            string textPreconditions = " ";
            foreach (var item in preconditions)
            {
                textPreconditions += item.PreconditionType + ",";
            }

            string textPrescription = " ";

            foreach (var item in prescriptions)
            {
                textPrescription += item.PrescriptionType + ",";
            }

            await Sms.ComposeAsync(new SmsMessage($"{activeUser.FirstName}, \nmy location: \nlatitude: {location.Latitude} , \nlongitude:  {location.Longitude}  " + "\nallergies: " + textAllergies + "\npreconditions: " + textPreconditions + "\nprescriptions: " + textPrescription,
                "911"));
        }
    }
}