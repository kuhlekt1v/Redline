/*
    File name: LogoutPage.xaml.cs
    Purpose:   Provide option to logout of app.
    Author:    Cody Sheridan
    Version:   1.0.0
*/

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedlineApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogoutPage : ContentPage
    {
        public LogoutPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        // Display confirmation alert
        protected async override void OnAppearing()
        {
            bool response = await DisplayAlert("Logout", "Are you sure?", "Yes", "No");

            if (response)
            {
               await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await Navigation.PushAsync(new MainPage());
            }

            base.OnAppearing();
            
        }
    }
}