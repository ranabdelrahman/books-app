using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using SQLite;
using SQLiteNetExtensions;
using Xamarin.Forms;

namespace Bookends
{
    public class DB
    {
        private static string DBName = "log.db";
        public static SQLiteConnection conn;
        public static void OpenConnection()
        {
            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, DBName);
            conn = new SQLiteConnection(fname);
            conn.CreateTable<VolumeInfo>();  

        }
        public static void DeleteTableContents(string tableName)
        {
            int v = conn.Execute("DELETE FROM " + tableName);
        }

    }
}
