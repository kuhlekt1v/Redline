/*
    File name: LoginPage.xaml.cs
    Purpose:   Facilitate user interaction with page.
    Author:    Cody Sheridan
    Version:   1.0.0
*/

using RedlineApp.View;
using RedlineApp.ViewModel;
using Xamarin.Forms;

namespace RedlineApp
{
    public partial class LoginPage : ContentPage
    {
        LoginViewModel userAuthentication;

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
                    Navigation.PushAsync(new MainPage());
                }
                else
                {
                    DisplayAlert("Login Failed", "Username or Password Incorrect", "Ok");
                }
            }
            else
            {
                DisplayAlert("Warning", "Enter User Name and Password Please", "Ok");
            }
        }

        private void HelpButtonClicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Success", "Forgot password?", "Ok");
        }
    }
}
