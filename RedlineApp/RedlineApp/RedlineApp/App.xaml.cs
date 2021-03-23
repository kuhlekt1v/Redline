/*
    File name: App.xaml.cs
    Purpose:   Provides initial entry into application.
    Author:    Cody Sheridan
    Version:   1.0.0
*/

using RedlineApp.View;
using Xamarin.Forms;

namespace RedlineApp
{
    public partial class App : Application
    {
        // Initialize login page.
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            /* PLACEHOLDER // NOTE:
             
               Trigger LoggedInUser = false here to ensure
               currently logged in user is logged out if they close
               app without formally logging out
            */
        }

        protected override void OnResume()
        {
        }
    }
}
