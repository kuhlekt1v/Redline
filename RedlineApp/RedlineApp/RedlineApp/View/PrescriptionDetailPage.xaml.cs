/*
    File name: PreconditionDetailPage.xaml.cs
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
    public partial class PrescriptionDetailPage : ContentPage
    {
        private SQLiteConnection _connection;
        Prescription selectedPrescription;

        public PrescriptionDetailPage(Prescription selectedPrescription)
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteInterface>().GetConnection();
            this.selectedPrescription = selectedPrescription;
            prescriptionEntry.Text = selectedPrescription.PrescriptionType;
        }

        //Connect to DB and create prescription table
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _connection.CreateTable<Prescription>();

        }

        //Button click action to assign new value to prescription
        async void UpdateButton_Clicked(System.Object sender, System.EventArgs e)
        {
            selectedPrescription.PrescriptionType = prescriptionEntry.Text;

            int rows = _connection.Update(selectedPrescription);

            if (rows > 0)
                await DisplayAlert("Success", "Prescription successfully updated", "Ok");

            else
                await DisplayAlert("Failure", "Prescription failed to be updated", "Ok");

            if (rows > 0)
            {
                await Navigation.PushAsync(new PrescriptionPage());
            }
        }

        //Button click to delete selected prescription
        async void DeleteButton_Clicked(System.Object sender, System.EventArgs e)
        {
            int rows = _connection.Delete(selectedPrescription);

            if (rows > 0)
                await DisplayAlert("Success", "Prescription successfully deleted", "Ok");

            else
                await DisplayAlert("Failure", "Prescription failed to be deleted", "Ok");

            if (rows > 0)
            {
                await Navigation.PushAsync(new PrescriptionPage());
            }
        }
    }
}
