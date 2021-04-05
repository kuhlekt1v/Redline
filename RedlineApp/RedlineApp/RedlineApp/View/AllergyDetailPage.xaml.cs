using RedlineApp.Model;
using RedlineApp.Persistence;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RedlineApp.View
{
    public partial class AllergyDetailPage : ContentPage
    {

        private SQLiteConnection _connection;
        Allergy selectedAllergy;

        public AllergyDetailPage(Allergy selectedAllergy)
        {
            InitializeComponent();

            _connection = DependencyService.Get<ISQLiteInterface>().GetConnection();

            this.selectedAllergy = selectedAllergy;

            allergyEntry.Text = selectedAllergy.AllergyType;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _connection.CreateTable<Allergy>();
            
        }


        async void UpdateButton_Clicked(System.Object sender, System.EventArgs e)
        {
            selectedAllergy.AllergyType = allergyEntry.Text;

            int rows = _connection.Update(selectedAllergy);

            if (rows > 0)
                await DisplayAlert("Success", "Allergy successfully updated", "Ok");

            else
                await DisplayAlert("Failure", "Allergy failed to be updated", "Ok");

            if (rows > 0)
            {
                _connection.UpdateWithChildren(selectedAllergy);
                await Navigation.PushAsync(new AllergyPage());
            }

        }

        async void DeleteButton_Clicked(System.Object sender, System.EventArgs e)
        {
            int rows = _connection.Delete(selectedAllergy);

            if (rows > 0)
                await DisplayAlert("Success", "Allergy successfully deleted", "Ok");

            else
                await DisplayAlert("Failure", "Allergy failed to be deleted", "Ok");

            if (rows > 0)
            {
                await Navigation.PushAsync(new AllergyPage());
            }


        }
    }
}
