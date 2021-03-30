/*
    File name: RegisterViewModel.cs
    Purpose:   Provides data required by RegisterPage View.
    Author:    Cody Sheridan
    Version:   1.0.1
*/

using RedlineApp.Behaviors;
using RedlineApp.Model;
using RedlineApp.Persistence;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            var existingEmail = data.Where(x => x.Email == userAccount.Email).FirstOrDefault();

            if (existingUser == null && existingEmail == null)
            {
                userAccount.ActiveUser = true;
                _connection.Insert(userAccount);
                return "New user added!";
            }
            else if(existingUser != null)
            {
                return "Username taken.";
            }
            else
            {
                return $"Account for {userAccount.Email} already exists.";
            }
        }

        // Confirm all required fields are filled.
        public bool AllFieldsFilled(object userAccount)
        {
            bool result = true;
            foreach (PropertyInfo pi in userAccount.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(userAccount);
                    if (string.IsNullOrEmpty(value))
                    {
                        result = false;
                        break;
                    }
                }    
            }
            return result;
        }

        // Confirm password and password confirmation fields match.
        public bool ConfirmMatchingPassword(string password, string confirmPass)
        {
            // Prevent null values breaking.
            if(password == null || confirmPass == null)
            {
                return false;
            }
            else
            {
                bool passwordMatch = confirmPass.Equals(password);
                return passwordMatch;
            }
        }

        // Display error message and focus on entry.
        public void DisplayPasswordError(Entry confirmPasswordEntry, Label passwordLabel)
        {
            confirmPasswordEntry.Focus();
            confirmPasswordEntry.Text = "";
            passwordLabel.Text = "Passwords don't match.";
        }
    }
}
