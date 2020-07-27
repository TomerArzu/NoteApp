using SQLite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.ViewModel.Helpers
{
    /// <summary>
    ///     Help us to do the functionallity in DB like insert, Update , Delete...
    /// </summary>
    public class DBHelper
    {
        public static string dbFile = Path.Combine(Environment.CurrentDirectory, "NoteDB.db3"); //path to where do we store the DB file

        public static bool Insert<T>(T Item)
        {
            bool result = false;

            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                result = conn.Insert(Item) > 0; // return number of rows that inserted inside of the table
                // if greater than 0 it's ok
            }
            return result;
        }

        public static bool Update<T>(T Item)
        {
            bool result = false;

            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                result = conn.Update(Item) > 0;
            }
            return result;
        }

        public static bool Delete<T>(T Item)
        {
            bool result = false;

            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                result = conn.Delete(Item) > 0;
            }
            return result;
        }
    }
}
