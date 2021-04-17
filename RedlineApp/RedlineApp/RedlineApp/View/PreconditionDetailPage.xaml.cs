/*
    File name: PreconditionDetailPage.xaml.cs
    Purpose:   Facilitate interaction with page.
    Author:    Daniel Mansilla
    Version:   1.0.0
*/

using RedlineApp.Model;
using RedlineApp.Persistence;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RedlineApp.View
{
    public partial class PreconditionDetailPage : ContentPage
    {
        private SQLiteConnection _connection;
        Precondition selectedPrecondition;

        public PreconditionDetailPage(Precondition selectedPrecondition)
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteInterface>().GetConnection();
            this.selectedPrecondition = selectedPrecondition;
            preconditionEntry.Text = selectedPrecondition.PreconditionType;
        }

        //Connect to DB and create Precondition table
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _connection.CreateTable<Precondition>();

        }

        //Button click action to assign new value to Precondition
        async void UpdateButton_Clicked(System.Object sender, System.EventArgs e)
        {
            selectedPrecondition.PreconditionType = preconditionEntry.Text;

            int rows = _connection.Update(selectedPrecondition);

            if (rows > 0)
                await DisplayAlert("Success", "Precondition successfully updated", "Ok");

            else
                await DisplayAlert("Failure", "Precondition failed to be updated", "Ok");

            if (rows > 0)
            {
                await Navigation.PushAsync(new PreconditionPage());
            }
        }

        //Button click to delete selected Precondition
        async void DeleteButton_Clicked(System.Object sender, System.EventArgs e)
        {
            int rows = _connection.Delete(selectedPrecondition);

            if (rows > 0)
                await DisplayAlert("Success", "Precondition successfully deleted", "Ok");

            else
                await DisplayAlert("Failure", "Precondition failed to be deleted", "Ok");

            if (rows > 0)
            {
                await Navigation.PushAsync(new PreconditionPage());
            }
        }
    }
}
