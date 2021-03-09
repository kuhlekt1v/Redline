/*
    File name: ISQLite.cs
    Purpose:   Initializes connection to SQLite using
               paths provided in platform Data folders.
    Author:    Cody Sheridan
    Version:   1.0.
*/

using SQLite;

namespace RedlineApp.Persistence
{
    public interface ISQLiteInterface
    {
        SQLiteConnection GetConnection();
    }
}
