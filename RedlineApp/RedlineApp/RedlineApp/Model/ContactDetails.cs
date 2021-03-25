/*
    File name: ContactDetails.cs
    Purpose:   Class containing all registered 
               users' contact information.
    Author:    Daniel Mansilla
    Version:   1.0.0
*/

using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace RedlineApp.Model
{
    public class ContactDetails
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(14)]
        public string PhoneNumber { get; set; }

        [MaxLength(50)]
        public string Address { get; set; }

        [MaxLength(14)]
        public string EmergencyContactNumber { get; set; }

        [MaxLength(25), Unique]
        public string EmergencyContactName { get; set; }

        [ForeignKey(typeof(UserAccount))]
        public int UserId { get; set; }

        [OneToOne]
        public UserAccount UserAccount { get; set; }

    }
}

