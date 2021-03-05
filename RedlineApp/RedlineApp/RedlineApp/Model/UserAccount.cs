/*
    File name: UserAccount.cs
    Purpose:   Class containing all registered 
               users' account information.
    Author:    Cody Sheridan
    Version:   1.0.0
*/

using SQLite;
using System;

namespace RedlineApp.Model
{
    class UserAccount
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(2)]
        public string MiddleInitial { get; set; }

        [MaxLength(255)]
        public string LastName { get; set; }

        [MaxLength(25), Unique]
        public string Username { get; set; }

        [MaxLength(25)]
        public string Password { get; set; }

        [MaxLength(255), Unique]
        public string Email { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
