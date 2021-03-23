/*
    File name: Allergy.cs
    Purpose:   Class containing all registered 
               users' allergy information.
    Author:    Daniel Mansilla
    Version:   1.0.0
*/

using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace RedlineApp.Model
{
    public class Allergy
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string AllergyType { get; set; }

        [ForeignKey(typeof(UserAccount))]
        public int UserId { get; set; }

        [ManyToOne]
        public UserAccount UserAccount { get; set; }

    }
}

