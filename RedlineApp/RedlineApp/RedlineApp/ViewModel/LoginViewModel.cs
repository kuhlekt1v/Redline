/*
   File name: LoginViewModel.cs
   Purpose:   Provides data required by LoginPage View.
   Author:    Cody Sheridan
   Version:   1.0.4
*/

using System;
using SQLite;
using Xamarin.Forms;
using System.Net.Mail;
using RedlineApp.Model;
using RedlineApp.Persistence;

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

        // Confirm case insensitive Username and case sensitive Password entry match a registered user account.
        public bool ValidateUserLogin(string userName, string password)
        {

            var data = _connection.Table<UserAccount>();
            var userAccount = data.Where(x => x.Username.ToLower() == userName.ToLower() && x.Password == password).FirstOrDefault();

            if (userAccount != null)
            {
                // Update as active user.
                userAccount.ActiveUser = true;
                _connection.Update(userAccount);
                return true;
            }
            else
            {
                return false;
            }
        }

        // Email password to account's registered email address.
        public string SendPasswordReminder(string userEmail)
        {
            var user = _connection.Table<UserAccount>()
                .Where(x => x.Email == userEmail).FirstOrDefault();
            string recipient = $"{user.FirstName} {user.LastName}";
            string result;

            try
            {
                // Initialize empty email and establish connection
                // with Google SMTP server. 
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                // Compose email recipient from Redline.
                mail.From = new MailAddress("redlinemedicalsystem@gmail.com");
                mail.To.Add(userEmail);
                mail.Subject = "Password Reminder";
                mail.Body = $"{recipient}, Your password is {user.Password}.\n If you didn't request a password reminder, please change your password immediately.";

                // Pass server details and security information.
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                // DEMONSTRATION ONLY - CLEAR TEXT PASSWORD NOT SAFE FOR PRODUCTION!
                SmtpServer.Credentials = new System.Net.NetworkCredential("redlinemedicalsystem@gmail.com", "RedL1nx2783");

                SmtpServer.Send(mail);

                result =  $"Password reminder sent successfully to {user.Email}.";
                return result;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return result;
            }
        }
    }
}
