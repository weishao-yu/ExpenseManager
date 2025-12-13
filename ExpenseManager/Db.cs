using System;
using System.Data.SQLite;
using System.IO;

namespace ExpenseManager
{
    public static class Db
    {
        public static string DbPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.db");

        public static string ConnStr =>
            $"Data Source={DbPath};Version=3;";

        public static void Init()
        {
            if (!File.Exists(DbPath))
                SQLiteConnection.CreateFile(DbPath);

            using var conn = new SQLiteConnection(ConnStr);
            conn.Open();

            string sql = @"
CREATE TABLE IF NOT EXISTS Users (
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  Username TEXT UNIQUE NOT NULL,
  PasswordHash TEXT NOT NULL
);";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.ExecuteNonQuery();

            string createRecords = @"
CREATE TABLE IF NOT EXISTS Records (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER NOT NULL,
    BookId INTEGER NOT NULL,
    Date TEXT NOT NULL,
    Item TEXT NOT NULL,
    Amount REAL NOT NULL,
    Category TEXT,
    Type TEXT NOT NULL
);";


            using var cmd2 = new SQLiteCommand(createRecords, conn);
            cmd2.ExecuteNonQuery();
            string createBooks = @"
CREATE TABLE IF NOT EXISTS Books (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER NOT NULL,
    Name TEXT NOT NULL
);";

            using var cmdBooks = new SQLiteCommand(createBooks, conn);
            cmdBooks.ExecuteNonQuery();


        }
    }
}
