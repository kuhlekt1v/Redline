/*
    File name: UserAccount.cs
    Purpose:   Class containing all registered 
               users' account information.
    Author:    Cody Sheridan
    Version:   1.0.0
*/

using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace RedlineApp.Model
{
    public class UserAccount
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50), NotNull]
        public string FirstName { get; set; }

        [MaxLength(255), NotNull]
        public string LastName { get; set; }

        [MaxLength(25), NotNull, Unique]
        public string Username { get; set; }

        [MaxLength(25), NotNull]
        public string Password { get; set; }

        [MaxLength(255), NotNull, Unique]
        public string Email { get; set; }

        public bool ActiveUser { get; set; } = false;

        public DateTime RegistrationDate { get; set; }

        [OneToOne]
        public List<ContactDetails> ContactDetails { get; set; }

        [OneToOne]
        public List<ProfileDetails> ProfileDetails { get; set; }

        [OneToMany]
        public List<Allergy> Allergy { get; set; }

        [OneToMany]
        public List<Prescription> Prescription { get; set; }

        [OneToMany]
        public List<Precondition> Precondition { get; set; }


    }
}
