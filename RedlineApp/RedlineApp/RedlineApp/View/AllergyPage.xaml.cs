/*
    File name: AllergyPage.xaml.cs
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
    public partial class AllergyPage : ContentPage
    {
        private SQLiteConnection _connection;

        public AllergyPage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteInterface>().GetConnection();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        //On page open connect to DB and find rows in allergy table where user is active.
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _connection.CreateTable<Allergy>();
            var data = _connection.Table<UserAccount>();
            var activeUser = data.Where(x => x.ActiveUser == true).FirstOrDefault();
            var allergies = _connection.Table<Allergy>().Where(x => x.UserId == activeUser.Id).ToList();
            allergyListView.ItemsSource = allergies;

            //if the last page was the main page then switch the buttons at bottom of screen.
            var previousPageIndex = Navigation.NavigationStack.Count - 2;
            var previousPageType = Navigation.NavigationStack[previousPageIndex];

            Button backbutton = new Button
            {
                Text = "Back",
                Style = (Style)Application.Current.Resources["PrimaryButton"]
            };

            if (previousPageType.GetType() == typeof(MainPage) || previousPageType.GetType() == typeof(AllergyDetailPage))
            {
                ButtonGrid.Children.Clear();
                ButtonGrid.Children.Add(backbutton);
                ButtonGrid.ColumnDefinitions.Clear();
                backbutton.Clicked += (sender, EventArgs) => {
                    Navigation.PushAsync(new MainPage());
                    };
            }
            
        }

        private void BackButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage());
        }

        private void NextButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new PrescriptionPage());
        }

        //Add allergy to DB table and assign userID to active user ID.
        async void AddButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var data = _connection.Table<UserAccount>();
            var activeUser = data.Where(x => x.ActiveUser == true).FirstOrDefault();

            Allergy allergy = new Allergy()
            {
                AllergyType = AllergyEntry.Text,
                UserId = activeUser.Id
            };

            int rows = _connection.Insert(allergy);

            if (rows > 0)
                await DisplayAlert("Success", "Allergy successfully added", "Ok");
                
            else
                await DisplayAlert("Failure", "Allergy failed to be added", "Ok");

            if (rows > 0)
            {
                await Navigation.PushAsync(new AllergyPage());
            }
        }

        //On item clicked redirect to details page for update or delete options.
        void AllergyListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var selectedAllergy = allergyListView.SelectedItem as Allergy;

            if(selectedAllergy != null)
            {
                Navigation.PushAsync(new AllergyDetailPage(selectedAllergy));
            }
        }

        void HomeButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}
