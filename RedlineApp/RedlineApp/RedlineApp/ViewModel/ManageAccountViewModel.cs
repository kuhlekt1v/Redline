/*
   File name: ManageAccountViewModel.cs
   Purpose:   Provides data required by ManageAccount View.
   Author:    Cody Sheridan
   Version:   1.0.1
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

        // Update user account record in database.
        public string UpdateDatabaseRecord(string updateBtnText, string fieldOneEntry, string fieldTwoEntry)
        {
            var account = new ManageAccountViewModel();
            var activeUser = account._data.Where(x => x.ActiveUser).FirstOrDefault();
            string returnMessage = "Something went wrong.";
            // Ensure only record for active user are updated.
            if (activeUser.ActiveUser == true)
            {
                switch (updateBtnText)
                {
                    case "Update Profile":
                        activeUser.FirstName = fieldOneEntry;
                        activeUser.LastName = fieldTwoEntry;
                        returnMessage = "Profile updated successfully!";
                        break;
                    case "Update Password":
                        activeUser.Password = fieldOneEntry;
                        returnMessage = "Password updated successfully!";
                        break;
                    case "Update Account":
                        activeUser.Username = fieldOneEntry;
                        activeUser.Email = fieldTwoEntry;
                        returnMessage = "Account updated successfully!";
                        break;
                }

                // Update record in db.
                _connection.Update(activeUser);
                return returnMessage;
            }
            else
            {
                return returnMessage;
            }
        }
    }
}
