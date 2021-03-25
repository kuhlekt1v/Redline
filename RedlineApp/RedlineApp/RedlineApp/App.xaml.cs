/*
    File name: App.xaml.cs
    Purpose:   Provides initial entry into application.
    Author:    Cody Sheridan
    Version:   1.0.1
*/

using RedlineApp.Model;
using RedlineApp.Persistence;
using RedlineApp.View;
using SQLite;
using Xamarin.Forms;

namespace RedlineApp
{
    public partial class App : Application
    {
        private SQLiteConnection _connection;
        UserAccount userAccount;

        // Initialize login page.
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
            _connection = DependencyService.Get<ISQLiteInterface>().GetConnection();
            _connection.CreateTable<UserAccount>();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            // Force user log out on app exit.
            LogUserOut(userAccount);
        }

        protected override void OnResume()
        {
        }

        private void LogUserOut(UserAccount userAccount)
        {
            this.userAccount = userAccount;
            var user = _connection.Table<UserAccount>().Where(x => x.ActiveUser == true);

            // Force user logout on app exit.
            userAccount.ActiveUser = false;
            _connection.Update(userAccount);
        }
    }
}
