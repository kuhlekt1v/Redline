/*
    File name: ProfileDetails.cs
    Purpose:   Class containing all registered 
               users' profile information.
    Author:    Daniel Mansilla
    Version:   1.0.0
*/

using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace RedlineApp.Model
{
    public class ProfileDetails
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime DateOfBirth { get; set; }

        [MaxLength(3)]
        public string HeightFeet { get; set; }

        [MaxLength(3)]
        public string HeightInches { get; set; }

        [MaxLength(3)]
        public string Weight { get; set; }

        [MaxLength(10)]
        public string Sex { get; set; }

        [MaxLength(3)]
        public string BloodType { get; set; }

        [ForeignKey(typeof(UserAccount))]
        public int UserId { get; set; }

        [OneToOne]
        public UserAccount UserAccount { get; set; }

    }
}

