/*
    File name: ContactPage.xaml.cs
    Purpose:   Facilitate interaction with page.
    Author:    Daniel Mansilla
    Version:   1.0.0
*/

using RedlineApp.Model;
using RedlineApp.Persistence;
using SQLite;
using SQLiteNetExtensions.Extensions;
using Xamarin.Forms;

namespace RedlineApp.View
{
    public partial class ContactPage : ContentPage
    {
        private SQLiteConnection _connection;

        public ContactPage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteInterface>().GetConnection();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _connection.CreateTable<ContactDetails>();
            var data = _connection.Table<UserAccount>();
            var activeUser = data.Where(x => x.ActiveUser == true).FirstOrDefault();
            var contact = _connection.Table<ContactDetails>().Where(x => x.UserId == activeUser.Id).FirstOrDefault();

            if (contact != null)
            {
                PhoneNumberEntry.Text = contact.PhoneNumber;
                AddressEntry.Text = contact.Address;
                EmergencyContactNumberEntry.Text = contact.EmergencyContactNumber;
                EmergencyContactNameEntry.Text = contact.EmergencyContactName;
            }

        }

        async private void NextButton_Clicked(System.Object sender, System.EventArgs e)
        {
            if (PhoneNumberEntry.Text == null || AddressEntry.Text == null || EmergencyContactNumberEntry.Text == null || EmergencyContactNameEntry.Text == null)
            {
                await DisplayAlert("Failure", "Please fill in all fields", "Ok");
            }
            else
            {
                var data = _connection.Table<UserAccount>();
                var activeUser = data.Where(x => x.ActiveUser == true).FirstOrDefault();
                var contact = _connection.Table<ContactDetails>().Where(x => x.UserId == activeUser.Id).FirstOrDefault();

                if (contact == null)
                {
                    ContactDetails contactDetails = new ContactDetails()
                    {
                        PhoneNumber = PhoneNumberEntry.Text,

                        Address = AddressEntry.Text,

                        EmergencyContactNumber = EmergencyContactNumberEntry.Text,

                        EmergencyContactName = EmergencyContactNameEntry.Text,

                        UserId = activeUser.Id
                    };

                    int rows = _connection.Insert(contactDetails);

                    if (rows > 0)
                        await DisplayAlert("Success", "Contact Details successfully added", "Ok");

                    else
                        await DisplayAlert("Failure", "Contact Details failed to be added", "Ok");

                    if (rows > 0)
                    {
                        await Navigation.PushAsync(new ProfilePage());
                    }
                }
                else
                {
                    contact.PhoneNumber = PhoneNumberEntry.Text;

                    contact.Address = AddressEntry.Text;

                    contact.EmergencyContactNumber = EmergencyContactNumberEntry.Text;

                    contact.EmergencyContactName = EmergencyContactNameEntry.Text;

                    int rows = _connection.Update(contact);

                    if (rows > 0)
                        await DisplayAlert("Success", "contact successfully updated", "Ok");

                    else
                        await DisplayAlert("Failure", "contact failed to be updated", "Ok");

                    if (rows > 0)
                    {
                        //_connection.UpdateWithChildren(contact);
                        await Navigation.PushAsync(new ProfilePage());
                    }
                }

                
            }

            
        }

        private void SkipButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}
