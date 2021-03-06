﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FormsMvvm2019
{
    public static class Constants
    {
        /// <summary>
        /// MessagingCenter 引数
        /// </summary>
        public const string MessageCoordinator = "Coordinator";

        public const string DatabaseFilename = "AppSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
