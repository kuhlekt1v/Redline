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

        //On page open create profile table and find active user ID.
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _connection.CreateTable<ProfileDetails>();
            var data = _connection.Table<UserAccount>();
            var activeUser = data.Where(x => x.ActiveUser == true).FirstOrDefault();
            var profile = _connection.Table<ProfileDetails>().Where(x => x.UserId == activeUser.Id).FirstOrDefault();

            //if active user already has profile info then display in entry fields.
            if (profile != null)
            {
                DatePickerSelected.Date = profile.DateOfBirth;
                HeightFeet.SelectedItem = profile.HeightFeet;
                HeightInches.SelectedItem = profile.HeightInches;
                WeightEntry.Text = profile.Weight;
                SexPicker.SelectedItem = profile.Sex;
                BloodTypePicker.SelectedItem = profile.BloodType;
            }

            //if the last page was the main page then switch the buttons at bottom of screen.
            var previousPageIndex = Navigation.NavigationStack.Count - 2;
            var previousPageType = Navigation.NavigationStack[previousPageIndex];

            if (previousPageType.GetType() == typeof(MainPage))
            {
                backHome.Text = "Home";
                updateNext.Text = "Save";
            }

        }

        private void BackButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var previousPageIndex = Navigation.NavigationStack.Count - 2;
            var previousPageType = Navigation.NavigationStack[previousPageIndex];
            if (previousPageType.GetType() == typeof(MainPage))
            {
                Navigation.PushAsync(new MainPage());
            }
            else
            {
                Navigation.PushAsync(new ContactPage());
            }
            
        }

        //On next button click submit profile info or update if user changed existing info.
        async private void NextButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var previousPageIndex = Navigation.NavigationStack.Count - 2;
            var previousPageType = Navigation.NavigationStack[previousPageIndex];
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
                        if (previousPageType.GetType() == typeof(MainPage))
                        {
                            await Navigation.PushAsync(new MainPage());
                        }
                        else
                        {
                            await Navigation.PushAsync(new AllergyPage());
                        }
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
                        if (previousPageType.GetType() == typeof(MainPage))
                        {
                            await Navigation.PushAsync(new MainPage());
                        }
                        else
                        {
                            await Navigation.PushAsync(new AllergyPage());
                        }
                    }

                }
                
            }
            
        }
    }
}

