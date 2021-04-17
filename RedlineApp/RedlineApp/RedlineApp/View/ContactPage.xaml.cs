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

        //On page open create contact details table and find active user ID.
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _connection.CreateTable<ContactDetails>();
            var data = _connection.Table<UserAccount>();
            var activeUser = data.Where(x => x.ActiveUser == true).FirstOrDefault();
            var contact = _connection.Table<ContactDetails>().Where(x => x.UserId == activeUser.Id).FirstOrDefault();

            //if active user already has contact info then display in entry fields.
            if (contact != null)
            {
                PhoneNumberEntry.Text = contact.PhoneNumber;
                AddressEntry.Text = contact.Address;
                EmergencyContactNumberEntry.Text = contact.EmergencyContactNumber;
                EmergencyContactNameEntry.Text = contact.EmergencyContactName;
            }

            //if the last page was the main page then switch the buttons at bottom of screen.
            var previousPageIndex = Navigation.NavigationStack.Count - 2;
            var previousPageType = Navigation.NavigationStack[previousPageIndex];

            if (previousPageType.GetType() == typeof(MainPage))
            {
                updateNext.Text = "Save";
            }

        }

        //On next button click submit contact info or update if user changed existing info.
        async private void NextButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var previousPageIndex = Navigation.NavigationStack.Count - 2;
            var previousPageType = Navigation.NavigationStack[previousPageIndex];
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
                        if (previousPageType.GetType() == typeof(MainPage))
                        {
                            await Navigation.PushAsync(new MainPage());
                        }
                        else
                        {
                            await Navigation.PushAsync(new ProfilePage());
                        }
                        
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
                        if (previousPageType.GetType() == typeof(MainPage))
                        {
                            await Navigation.PushAsync(new MainPage());
                        }
                        else
                        {
                            await Navigation.PushAsync(new ProfilePage());
                        }
                    }
                }

                
            }

            
        }

        //redirects user to home page
        private void SkipButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}
