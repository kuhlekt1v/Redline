/*
    File name: RegisterPage.xaml.cs
    Purpose:   Facilitate interaction with page.
    Author:    Cody Sheridan
    Version:   1.0.2
*/

using RedlineApp.Model;
using RedlineApp.Persistence;
using RedlineApp.View;
using RedlineApp.ViewModel;
using SQLite;
using System;
using Xamarin.Forms;

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
            // User info to be added to db.
            var user = new UserAccount()
            {
                FirstName = FirstNameEntry.Text,
                LastName = LastNameEntry.Text,
                Username = UsernameEntry.Text,
                Password = PasswordEntry.Text,
                RegistrationDate = DateTime.UtcNow,
                Email = EmailEntry.Text,
                ActiveUser = true 
            };


            bool formIsValid = registerViewModel.AllFieldsFilled(user);
            bool isValidEmail = registerViewModel.ConfirmValidEmail(EmailEntry.Text);
            bool passwordIsValid = registerViewModel.ConfirmMatchingPassword(user.Password, ConfirmPasswordEntry.Text);
            

            // If all required fields are valid.
            if (formIsValid)
            {
                // Ensure both password fields match.
                if (!passwordIsValid)
                {
                    await DisplayAlert("Error", "Passwords must match.", "Ok");
                    registerViewModel.DisplayPasswordError(ConfirmPasswordEntry, Password_Confirmation);
                }
                // Ensure valid email entered
                else if(!isValidEmail)
                {
                    await DisplayAlert("Error", "Invalid email address.", "Ok");
                    registerViewModel.DisplayPasswordError(ConfirmPasswordEntry, Password_Confirmation);
                }
                else
                {
                    // Add record to database.
                    string returnValue = registerViewModel.AddNewUser(user);

                    if (returnValue == "New user added!")
                    {
                        // Form submitted successfully.
                        await DisplayAlert("Success", returnValue, "Ok");
                        //await Navigation.PushAsync(new MainPage());
                        await Navigation.PushAsync(new ContactPage());
                    }
                    else
                    {
                        // Something went wrong.
                        await DisplayAlert("Error", returnValue, "Ok");
                    }
                }
            }
            else
            {
                await DisplayAlert("Error", "Please complete all required fields.", "Ok");
            }
        }

        private void BackButtonClicked(object sender, EventArgs e)
        {
            // Navigate to LoginPage.
            Navigation.PushAsync(new LoginPage());
        }
    }
}