/*
    File name: LoginPage.xaml.cs
    Purpose:   Facilitate user interaction with page.
    Author:    Cody Sheridan
    Version:   1.0.3
*/

using RedlineApp.Model;
using RedlineApp.Persistence;
using RedlineApp.ViewModel;
using SQLite;
using System;
using System.Net.Mail;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedlineApp.View
{
    public partial class LoginPage : ContentPage
    {
        private SQLiteConnection _connection;
        private LoginViewModel loginViewModel;

        public LoginPage()
        {
            InitializeComponent();
            loginViewModel = new LoginViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
            _connection = DependencyService.Get<ISQLiteInterface>().GetConnection();
            _connection.CreateTable<UserAccount>();
        }

        // Ensure no active users on page load.
        protected override void OnAppearing()
        {
            var data = _connection.Table<UserAccount>();
            var activeUser = data.Where(x => x.ActiveUser == true).FirstOrDefault();
            // Catch null if no active users on page load.
            if (activeUser != null)
            {
                activeUser.ActiveUser = false;
                _connection.Update(activeUser);
            }

            base.OnAppearing();
        }

        private void LoginButtonClicked(object sender, EventArgs e)
        {
            if (userNameEntry.Text != null && passwordEntry.Text != null)
            {
                var validData = loginViewModel.ValidateUserLogin(userNameEntry.Text, passwordEntry.Text);
                if (validData)
                {
                    // Clear fields and navigate to main page.
                    userNameEntry.Text = string.Empty;
                    passwordEntry.Text = string.Empty;
                    Navigation.PushAsync(new MainPage());
                }
                else
                {
                    DisplayAlert("We can't find that user name and password.", "Please try again.", "Close");
                }
            }
            else
            {
                DisplayAlert("", "Please enter a username and password to continue.", "Ok");
            }
        }

        async void HelpButtonClicked(object sender, EventArgs e)
        {
            var answer = await DisplayPromptAsync("Send Password Reminder",
                "Enter your account email address.", "OK", "Cancel", "Email", keyboard: Keyboard.Email);

            if (!string.IsNullOrWhiteSpace(answer))
            {
                var reminder = loginViewModel.SendPasswordReminder(answer);
                await DisplayAlert("Password Reminder Notification", reminder, "Ok");
            }
            else
            {
                await DisplayAlert("Password Reminder Notification", "Email required. Please try again.", "Ok");
            }
        }

        private void SignUpLinkTapped(object sender, EventArgs e)
        {
            // Set register page as current page.
            Navigation.PushAsync(new RegisterPage());
        }
    }
}