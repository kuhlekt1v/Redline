/*
    File name: RegisterViewModel.cs
    Purpose:   Provides data required by RegisterPage View.
    Author:    Cody Sheridan
    Version:   1.0.0
*/

using RedlineApp.Model;
using RedlineApp.Persistence;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace RedlineApp.ViewModel
{
    class RegisterViewModel
    {
        private SQLiteConnection _connection;

        // Create connection to UserAccount ISQLite table.
        public RegisterViewModel()
        {
            _connection = DependencyService.Get<ISQLiteInterface>().GetConnection();
            _connection.CreateTable<UserAccount>();
        }


        // Add new user to database.
        public string AddNewUser(UserAccount userAccount)
        {
            var data = _connection.Table<UserAccount>();
            var existingUser = data.Where(x => x.Username == userAccount.Username).FirstOrDefault();

            if (existingUser == null)
            {
                _connection.Insert(userAccount);
                return "New user added!";
            }
            else
            {
                return "User already exists.";
            }

        }


        // Check to ensure user doesn't exist already.


        // Movie these to an account management type page.


        // Return list of all users registered in database.
        private IEnumerable<UserAccount> GetUserAccounts()
        {
            return (from u in _connection.Table<UserAccount>()
                    select u).ToList();
        }

        private UserAccount GetSpecificUser(int id)
        {
            return _connection.Table<UserAccount>().FirstOrDefault(u => u.Id == id);
        }

    }
}
