/*
    File name: SQLiteIOS.cs
    Purpose:   Establish connection to local storage
               for SQLite on iOS devices.
    Author:    Cody Sheridan
    Version:   1.0.2
*/

using RedlineApp.Persistence;
using RedlineApp.iOS.Persistence;
using SQLite;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteIOS))]
namespace RedlineApp.iOS.Persistence
{ 
    class SQLiteIOS : ISQLiteInterface
    {
        public SQLiteConnection GetConnection()
        {
            // Set path on device where database file will
            // be stored.
            string documentsPath = Path.Combine(System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Personal), "..", "Library");
            string path = Path.Combine(documentsPath, "RedlineAppDB.db3");
            
            // Return SQLite connection.
            return new SQLiteConnection(path);
        }

    }
}