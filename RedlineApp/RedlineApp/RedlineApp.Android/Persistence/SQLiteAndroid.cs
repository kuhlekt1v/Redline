/*
    File name: SQLiteAndroid.cs
    Purpose:   Establish connection to local storage
               for SQLite on Android devices.
    Author:    Cody Sheridan
    Version:   1.0.1
*/

using RedlineApp.Persistence;
using RedlineApp.Droid.Persistence;
using SQLite;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteAndroid))]
namespace RedlineApp.Droid.Persistence
{
    class SQLiteAndroid : ISQLite
    {
        public SQLiteAsyncConnection GetConnection()
        {
            // Set path on device where database file will
            // be stored.
            var documentsPath = System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "RedlineAppDB.db3");

            // Return SQLite connection.
            return new SQLiteAsyncConnection(path);
        }
    }
}