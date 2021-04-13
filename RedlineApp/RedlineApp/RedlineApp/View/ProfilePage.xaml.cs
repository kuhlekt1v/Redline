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
            var data = _connection.Table<UserAccount>();
            var activeUser = data.Where(x => x.ActiveUser == true).FirstOrDefault();
            var profile = _connection.Table<ProfileDetails>().Where(x => x.UserId == activeUser.Id).FirstOrDefault();

            if (profile != null)
            {
                DatePickerSelected.Date = profile.DateOfBirth;
                HeightFeet.SelectedItem = profile.HeightFeet;
                HeightInches.SelectedItem = profile.HeightInches;
                WeightEntry.Text = profile.Weight;
                SexPicker.SelectedItem = profile.Sex;
                BloodTypePicker.SelectedItem = profile.BloodType;

            }

        }

        private void BackButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ContactPage());
        }

        async private void NextButton_Clicked(System.Object sender, System.EventArgs e)
        {
            if (DatePickerSelected.Date == null || (string)HeightFeet.SelectedItem == null || (string)HeightFeet.SelectedItem == null ||
                WeightEntry.Text == null || (string)SexPicker.SelectedItem == null || (string)BloodTypePicker.SelectedItem == null)
            {
                await DisplayAlert("Failure", "Please fill in all fields", "Ok");
            }
            else
            {
                var data = _connection.Table<UserAccount>();
                var activeUser = data.Where(x => x.ActiveUser == true).FirstOrDefault();
                var profile = _connection.Table<ProfileDetails>().Where(x => x.UserId == activeUser.Id).FirstOrDefault();

                if (profile == null)
                {
                    ProfileDetails profileDetails = new ProfileDetails()
                    {
                        DateOfBirth = DatePickerSelected.Date,

                        HeightFeet = (string)HeightFeet.SelectedItem,

                        HeightInches = (string)HeightInches.SelectedItem,

                        Weight = WeightEntry.Text,

                        Sex = (string)SexPicker.SelectedItem,

                        BloodType = (string)BloodTypePicker.SelectedItem,

                        UserId = activeUser.Id
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
                else
                {
                    profile.DateOfBirth = DatePickerSelected.Date;

                    profile.HeightFeet = (string)HeightFeet.SelectedItem;

                    profile.HeightInches = (string)HeightInches.SelectedItem;

                    profile.Weight = WeightEntry.Text;

                    profile.Sex = (string)SexPicker.SelectedItem;

                    profile.BloodType = (string)BloodTypePicker.SelectedItem;

                    int rows = _connection.Update(profile);

                    if (rows > 0)
                        await DisplayAlert("Success", "profile successfully updated", "Ok");

                    else
                        await DisplayAlert("Failure", "profile failed to be updated", "Ok");

                    if (rows > 0)
                    {
                        //_connection.UpdateWithChildren(contact);
                        await Navigation.PushAsync(new AllergyPage());
                    }

                }
                
            }
            
        }
    }
}

