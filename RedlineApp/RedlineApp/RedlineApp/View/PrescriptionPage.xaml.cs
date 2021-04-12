/*
    File name: PrescriptionPage.xaml.cs
    Purpose:   Facilitate interaction with page.
    Author:    Daniel Mansilla
    Version:   1.0.0
*/

using RedlineApp.Model;
using RedlineApp.Persistence;
using SQLite;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RedlineApp.View
{
    public partial class PrescriptionPage : ContentPage
    {
        private SQLiteConnection _connection;

        public PrescriptionPage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteInterface>().GetConnection();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _connection.CreateTable<Prescription>();
            var data = _connection.Table<UserAccount>();
            var activeUser = data.Where(x => x.ActiveUser == true).FirstOrDefault();
            var prescriptions = _connection.Table<Prescription>().Where(x => x.UserId == activeUser.Id).ToList();
            //var prescriptions = _connection.Table<Prescription>().ToList();
            prescriptionListView.ItemsSource = prescriptions;
            ;
        }

        private void BackButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AllergyPage());
        }

        private void NextButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new PreconditionPage());
        }

        async void AddButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var data = _connection.Table<UserAccount>();
            var activeUser = data.Where(x => x.ActiveUser == true).FirstOrDefault();

            Prescription prescription = new Prescription()
            {
                PrescriptionType = PrescriptionEntry.Text,
                UserId = activeUser.Id
            };

            int rows = _connection.Insert(prescription);

            if (rows > 0)
                await DisplayAlert("Success", "Prescription successfully added", "Ok");

            else
                await DisplayAlert("Failure", "Prescription failed to be added", "Ok");

            if (rows > 0)
            {
                await Navigation.PushAsync(new PrescriptionPage());
            }
        }

        void PrescriptionListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var selectedPrescription = prescriptionListView.SelectedItem as Prescription;

            if (selectedPrescription != null)
            {
                Navigation.PushAsync(new PrescriptionDetailPage(selectedPrescription));
            }
        }

        void HomeButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MedicalInformationPage());
        }
    }
}
