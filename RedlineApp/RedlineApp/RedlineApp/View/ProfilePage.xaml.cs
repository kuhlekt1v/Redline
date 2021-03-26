/*
    File name: ProfilePage.xaml.cs
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
    public partial class ProfilePage : ContentPage
    {
        private SQLiteConnection _connection;

        public ProfilePage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteInterface>().GetConnection();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _connection.CreateTable<ProfileDetails>();

        }

        private void BackButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ContactPage());
        }

        async private void NextButton_Clicked(System.Object sender, System.EventArgs e)
        {
            ProfileDetails profileDetails = new ProfileDetails()
            {
                DateOfBirth = DatePickerSelected.Date,

                Height = (string)HeightFeet.SelectedItem,

                Weight = WeightEntry.Text,

                Sex = (string)SexPicker.SelectedItem,

                BloodType = (string)BloodTypePicker.SelectedItem,

                UserId = 1
            };

            int rows = _connection.Insert(profileDetails);

            if (rows > 0)
                await DisplayAlert("Success", "Profile Details successfully added", "Ok");

            else
                await DisplayAlert("Failure", "Profile Details failed to be added", "Ok");

            if (rows > 0)
            {
                await Navigation.PushAsync(new AllergyPage());
            }

            
        }
    }
}

