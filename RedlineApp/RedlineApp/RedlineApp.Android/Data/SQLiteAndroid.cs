/*
    File name: SQLiteAndroid.cs
    Purpose:   Establish connection to local storage
               for SQLite on Android devices.
    Author:    Cody Sheridan
    Version:   1.0.0
*/

using RedlineApp.Data;
using RedlineApp.Droid.Data;
using SQLite;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteAndroid))]
namespace RedlineApp.Droid.Data
{
    class SQLiteAndroid : ISQLite
    {
        public SQLiteAsyncConnection GetConnection()
        {
            // Set path on device where database file will
            // be stored.
            string dbFileName = "RedlineAppDB.db3";
            string folderPath = System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(folderPath, dbFileName);

            // Return SQLite connection.
            return new SQLiteAsyncConnection(path);
        }
    }
}