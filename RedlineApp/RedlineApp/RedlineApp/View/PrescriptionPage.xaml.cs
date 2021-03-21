/*
    File name: PrescriptionPage.xaml.cs
    Purpose:   Facilitate interaction with page.
    Author:    Daniel Mansilla
    Version:   1.0.0
*/

using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RedlineApp.View
{
    public partial class PrescriptionPage : ContentPage
    {
        public PrescriptionPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void BackButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AllergyPage());
        }

        private void NextButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new PreconditionPage());
        }
    }
}
