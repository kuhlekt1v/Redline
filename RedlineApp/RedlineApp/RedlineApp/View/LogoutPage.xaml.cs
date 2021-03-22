/*
    File name: LogoutPage.xaml.cs
    Purpose:   Provide option to logout of app.
    Author:    Cody Sheridan
    Version:   1.0.1
*/

using Xamarin.Forms;

namespace RedlineApp.View
{
    public partial class LogoutPage : ContentPage
    {

        public LogoutPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}