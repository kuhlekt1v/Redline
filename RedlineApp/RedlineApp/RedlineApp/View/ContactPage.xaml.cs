/*
    File name: ContactPage.xaml.cs
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
    public partial class ContactPage : ContentPage
    {
        private SQLiteConnection _connection;

        public ContactPage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteInterface>().GetConnection();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _connection.CreateTable<ContactDetails>();

        }

        async private void NextButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var data = _connection.Table<UserAccount>();
            var activeUser = data.Where(x => x.ActiveUser == true).FirstOrDefault();

            ContactDetails contactDetails = new ContactDetails()
            {
                PhoneNumber = PhoneNumberEntry.Text,

                Address = AddressEntry.Text,

                EmergencyContactNumber = EmergencyContactNumberEntry.Text,

                EmergencyContactName = EmergencyContactNameEntry.Text,

                UserId = activeUser.Id
            };

            int rows = _connection.Insert(contactDetails);

            if (rows > 0)
                await DisplayAlert("Success", "Contact Details successfully added", "Ok");

            else
                await DisplayAlert("Failure", "Contact Details failed to be added", "Ok");

            if (rows > 0)
            {
                await Navigation.PushAsync(new ProfilePage());
            }

            
        }

        private void SkipButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}
