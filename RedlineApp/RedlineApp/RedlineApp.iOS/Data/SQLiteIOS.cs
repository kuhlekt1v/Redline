/*
    File name: SQLiteIOS.cs
    Purpose:   Establish connection to local storage
               for SQLite on iOS devices.
    Author:    Cody Sheridan
    Version:   1.0.0
*/

using RedlineApp.Data;
using RedlineApp.iOS.Data;
using SQLite;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteIOS))]
namespace RedlineApp.iOS.Data
{
    class SQLiteIOS : ISQLite
    {
        public SQLiteAsyncConnection GetConnection()
        {
            // Set path on device where database file will
            // be stored.
            string dbFileName = "RedlineAppDB.db3";
            string folderPath = Path.Combine(System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Personal), "..", "Library");
            string path = Path.Combine(folderPath, dbFileName);
            
            // Return SQLite connection.
            return new SQLiteAsyncConnection(path);
        }

    }
}