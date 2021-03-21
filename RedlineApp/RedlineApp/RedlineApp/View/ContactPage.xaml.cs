/*
    File name: ContactPage.xaml.cs
    Purpose:   Facilitate interaction with page.
    Author:    Daniel Mansilla
    Version:   1.0.0
*/

using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RedlineApp.View
{
    public partial class ContactPage : ContentPage
    {
        public ContactPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void NextButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage());
        }

        private void SkipButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}
