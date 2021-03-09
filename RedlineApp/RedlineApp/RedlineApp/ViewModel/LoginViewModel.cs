/*
    File name: LoginViewModel.cs
    Purpose:   Provides data required by LoginPage View.
    Author:    Cody Sheridan
    Version:   1.0.0
*/

using RedlineApp.Model;
using RedlineApp.Persistence;
using SQLite;
using Xamarin.Forms;

namespace RedlineApp.ViewModel
{
    public class LoginViewModel
    {
        private SQLiteConnection _connection;

        // Create connection to UserAccount ISQLite table.
        public LoginViewModel()
        {
            _connection = DependencyService.Get<ISQLiteInterface>().GetConnection();
            _connection.CreateTable<UserAccount>();
        }


        // Confirm Username and Password entry match a registered user account.
        public bool ValidateUserLogin(string userName, string password)
        {
            var data = _connection.Table<UserAccount>();
            var credentials = data.Where(x => x.Username == userName 
                && x.Password == password).FirstOrDefault();

            if (credentials != null)
            {
                return true;
            } 
            else
            {
                return false;
            }   
        }
    }
}
