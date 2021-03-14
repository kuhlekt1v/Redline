/*
    File name: LoginPage.xaml.cs
    Purpose:   Facilitate user interaction with page.
    Author:    Cody Sheridan
    Version:   1.0.1
*/

using RedlineApp.View;
using RedlineApp.ViewModel;
using System;
using Xamarin.Forms;

namespace RedlineApp
{
    public partial class LoginPage : ContentPage
    {
        private LoginViewModel userAuthentication;

        public LoginPage()
        {
            InitializeComponent();
            userAuthentication = new LoginViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void SignUpLinkTapped(object sender, System.EventArgs e)
        {
            // Set register page as current page.
           Navigation.PushAsync(new RegisterPage());

        }

        private void LoginButtonClicked(object sender, System.EventArgs e)
        {
            if (userNameEntry.Text != null && passwordEntry.Text != null)
            {
                var validData = userAuthentication.ValidateUserLogin(userNameEntry.Text, passwordEntry.Text);
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
            var answer = await DisplayPromptAsync("Send Password",
                "Enter your account email address.", "OK", "Cancel", "Email", keyboard: Keyboard.Email);

            if (!string.IsNullOrWhiteSpace(answer))
            {
                var password = userAuthentication.PasswordReminder(answer);
                await DisplayAlert("REMINDER IN DEVELOPMENT", password, "Ok");
            }
        }
    }
}
