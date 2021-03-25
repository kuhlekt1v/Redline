 /*
    File name: LoginViewModel.cs
    Purpose:   Provides data required by LoginPage View.
    Author:    Cody Sheridan
    Version:   1.0.3
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
        UserAccount userAccount;

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
                //MarkActiveUser(userName, userAccount);
                return true;
            }
            else
            {
                return false;
            }
        }

        // In development - Email password to registered account password
        // if provided email is a valid account. 
        public string PasswordReminder(string userEmail)
        {
            var user = _connection.Table<UserAccount>()
                .Where(x => x.Email == userEmail).FirstOrDefault();

            if (user != null)
            {
                var password = _connection.Table<UserAccount>()
                    .Where(x => x.Email == userEmail)
                    .FirstOrDefault()?
                    .Password;

                return $"The password for {userEmail} is {password}";
            }
            else
            {
                return "No user exists with the provided email.";
            }
        }

        //// Mark logged in user as active user.
        //private void MarkActiveUser(string userName, UserAccount userAccount)
        //{
        //    this.userAccount = userAccount;
        //    var user = _connection.Table<UserAccount>()
        //        .Where(x => x.Username == userName).FirstOrDefault();

        //    userAccount.ActiveUser = true;
        //    _connection.Update(userAccount);
        //}

    }
}
