/*
    File name: App.xaml.cs
    Purpose:   Provides initial entry into application.
    Author:    Cody Sheridan
    Version:   1.0.0
*/

using Xamarin.Forms;

namespace RedlineApp
{
    public partial class App : Application
    {
        // Initialize login page.
        public App()
        {
            InitializeComponent();
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
