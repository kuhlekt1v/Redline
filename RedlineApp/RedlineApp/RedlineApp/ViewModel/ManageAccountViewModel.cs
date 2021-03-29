/*
   File name: ManageAccountViewModel.cs
   Purpose:   Provides data required by ManageAccount View.
   Author:    Cody Sheridan
   Version:   1.0.0
*/

using SQLite;
using Xamarin.Forms;
using RedlineApp.Model;
using RedlineApp.Persistence;
using System.Collections.Generic;

namespace RedlineApp.ViewModel
{
    public class ManageAccountViewModel
    {
        private SQLiteConnection _connection;
        private TableQuery<UserAccount> _data;

        public ManageAccountViewModel()
        {
            _connection = DependencyService.Get<ISQLiteInterface>().GetConnection();
            _connection.CreateTable<UserAccount>();
            _data = _connection.Table<UserAccount>();
        }

        // Return active user's first and last name as list.
        public static List<string> GetCurrentUserName()
        {
            var account = new ManageAccountViewModel();
            var activeUser = account._data.Where(x => x.ActiveUser).FirstOrDefault();

            var returnNames = new List<string>();
            returnNames.Add(activeUser.FirstName);
            returnNames.Add(activeUser.LastName);

            return returnNames;
        }

        // Return active user password twice to satisfy password and
        // password confirmation fields.
        public static List<string> GetCurrentUserPassword()
        {
            var account = new ManageAccountViewModel();
            var activeUser = account._data.Where(x => x.ActiveUser).FirstOrDefault();

            var existingPassword = new List<string>();
            existingPassword.Add(activeUser.Password);
            existingPassword.Add(activeUser.Password);

            return existingPassword;
        }

        // Return active user username and email address.
        public static List<string> GetCurrentUserAccount()
        {
            var account = new ManageAccountViewModel();
            var activeUser = account._data.Where(x => x.ActiveUser).FirstOrDefault();

            var returnDetails = new List<string>();
            returnDetails.Add(activeUser.Username);
            returnDetails.Add(activeUser.Email);

            return returnDetails;
        }
    }
}
