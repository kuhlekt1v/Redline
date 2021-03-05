using RedlineApp.Model;
using RedlineApp.Persistence;
using SQLite;
using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedlineApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<UserAccount> _userAccounts; // Testing

        public RegisterPage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLite>().GetConnection();
        }

        // Load existing UserAccount from databse on page appear.
        protected override async void OnAppearing()
        {

            base.OnAppearing();

            await _connection.CreateTableAsync<UserAccount>();

            //var userAccounts = await _connection.Table<UserAccount>().ToListAsync();
            //_userAccounts = new ObservableCollection<UserAccount>(userAccounts); // Testing

            // Assign useraccounts to list view's ItemSource property.
            //usersListView.ItemsSource = _userAccounts;                           


        }

        async void RegisterNewUser(object sender, EventArgs e)
        {
            var userAccount = new UserAccount()
            {
                FirstName = FirstNameEntry.Text,
                MiddleInitial = MiddleInitialEntry.Text,
                LastName = LastNameEntry.Text,
                Username = UsernameEntry.Text,
                Password = PasswordEntry.Text,
                RegistrationDate = DateTime.UtcNow,
                Email = EmailEntry.Text
            };
        
           await _connection.InsertAsync(userAccount);

            //_userAccounts.Add(userAccount);

        }

        private void BackButtonClicked(object sender, EventArgs e)
        {
            // Navigate to LoginPage.
            Application.Current.MainPage = new LoginPage();
        }
    }
}