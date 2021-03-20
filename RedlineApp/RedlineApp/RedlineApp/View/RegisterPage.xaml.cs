/*
    File name: RegisterPage.xaml.cs
    Purpose:   Facilitate interaction with page.
    Author:    Cody Sheridan
    Version:   1.0.0
*/

using RedlineApp.Behaviors;
using RedlineApp.Model;
using RedlineApp.Persistence;
using RedlineApp.View;
using RedlineApp.ViewModel;
using SQLite;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedlineApp
{
    public partial class RegisterPage : ContentPage
    {
        private SQLiteConnection _connection;
        private RegisterViewModel registerViewModel;

        public RegisterPage()
        {
            InitializeComponent();
            registerViewModel = new RegisterViewModel();
            _connection = DependencyService.Get<ISQLiteInterface>().GetConnection();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        // Load existing UserAccount from databse on page appear.
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _connection.CreateTable<UserAccount>();
        }

        async void RegisterButtonClicked(object sender, EventArgs e)
        {
            var user = new UserAccount()
            {
                FirstName = FirstNameEntry.Text,
                MiddleInitial = MiddleInitialEntry.Text,
                LastName = LastNameEntry.Text,
                Username = UsernameEntry.Text,
                Password = PasswordEntry.Text,
                RegistrationDate = DateTime.UtcNow,
                Email = EmailEntry.Text
            };

            var returnValue = registerViewModel.AddNewUser(user);

            if (returnValue == "New user added!")
            {
                await DisplayAlert("Success", returnValue, "Ok");
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Error", returnValue, "Ok");
            }
        }

        private void BackButtonClicked(object sender, EventArgs e)
        {
            // Navigate to LoginPage.
            Navigation.PushAsync(new LoginPage());
        }
    }
}