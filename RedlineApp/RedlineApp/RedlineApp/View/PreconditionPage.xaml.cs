/*
    File name: PreconditionPage.xaml.cs
    Purpose:   Facilitate interaction with page.
    Author:    Daniel Mansilla
    Version:   1.0.0
*/

using RedlineApp.Model;
using RedlineApp.Persistence;
using SQLite;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RedlineApp.View
{
    public partial class PreconditionPage : ContentPage
    {
        private SQLiteConnection _connection;

        public PreconditionPage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteInterface>().GetConnection();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _connection.CreateTable<Precondition>();
            var preconditions = _connection.Table<Precondition>().ToList();
            preconditionListView.ItemsSource = preconditions;
        }


        private void BackButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new PrescriptionPage());
        }

        private void CompleteButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }



        async void AddButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Precondition precondition = new Precondition()
            {
                PreconditionType = PreconditionEntry.Text,
                UserId = 1
            };

            int rows = _connection.Insert(precondition);

            if (rows > 0)
                await DisplayAlert("Success", "Precondition successfully added", "Ok");

            else
                await DisplayAlert("Failure", "Precondition failed to be added", "Ok");

            if (rows > 0)
            {
                await Navigation.PushAsync(new PreconditionPage());
            }
        }

        void PreconditionListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var selectedPrecondition = preconditionListView.SelectedItem as Precondition;

            if (selectedPrecondition != null)
            {
                Navigation.PushAsync(new PreconditionDetailPage(selectedPrecondition));
            }
        }
    }
}
