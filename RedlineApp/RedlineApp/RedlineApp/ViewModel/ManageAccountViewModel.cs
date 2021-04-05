/*
   File name: ManageAccountViewModel.cs
   Purpose:   Provides data required by ManageAccount View.
   Author:    Cody Sheridan
   Version:   1.0.3
*/

using SQLite;
using Xamarin.Forms;
using RedlineApp.Model;
using RedlineApp.Persistence;
using System.Collections.Generic;
using System.Linq;
using SQLiteNetExtensions.Extensions;

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

        // Confirm password and password confirmation fields match.
        public bool ConfirmMatchingPassword(string form, string password, string confirmPass)
        {
            // Always return true if sending form is not the password form.
            if(form == "Update Password")
            {
                // Prevent null values breaking.
                if (password == null || confirmPass == null)
                {
                    return false;
                }
                else
                {
                    bool passwordMatch = confirmPass.Equals(password);
                    return passwordMatch;
                }
            }
            else
            {
                return true;
            }
        }

        // Confirm all required fields are filled.
        public bool AllFieldsFilled(string fieldOne, string fieldTwo)
        {
            if(!string.IsNullOrWhiteSpace(fieldOne) && !string.IsNullOrWhiteSpace(fieldTwo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string ConfirmUniqueCredentials(string username, string email)
        {
            var account = new ManageAccountViewModel();
            var activeUser = account._data.Where(x => x.ActiveUser).FirstOrDefault();
            var existingUser = _data.Where(x => x.Username == username && x.ActiveUser != true).FirstOrDefault();
            var existingEmail = _data.Where(x => x.Email == email && x.ActiveUser != true).FirstOrDefault();
            string response = "Account updated successfully!";

            if (existingUser != null && existingEmail == null)
            {
                activeUser.Email = email;
                response = "username taken";
                return response;
            }
            else if (existingUser == null && existingEmail != null)
            {
                response = "email taken";
                return response;
            }
            else if (existingUser != null && existingEmail != null)
            {
                response = "both taken";
                return response;
            }
            else
            {
                activeUser.Username = username;
                activeUser.Email = email;
                return response;
            }
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
                        returnMessage = ConfirmUniqueCredentials(fieldOneEntry, fieldTwoEntry);
                        if (returnMessage == "Account updated successfully!")
                        {
                            activeUser.Username = fieldOneEntry;
                            activeUser.Email = fieldTwoEntry;
                        }
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

        public bool DeleteUserAccount()
        {
            var account = new ManageAccountViewModel();
            var activeUser = account._data.Where(x => x.ActiveUser).FirstOrDefault();

            if (activeUser != null)
            {
                // Get all children records of active user.
                var activeUserRecord = _connection.GetWithChildren<UserAccount>(activeUser.Id);

                // Delete all records related to active user.
                _connection.Delete(activeUserRecord, recursive: true);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
