using System;
using System.Data.SQLite;

namespace ExpenseManager
{
    public static class AuthService
    {
        public static bool Register(string username, string password, out string msg)
        {
            msg = "";

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                msg = "帳號或密碼不可為空";
                return false;
            }

            string hash = BCrypt.Net.BCrypt.HashPassword(password);

            try
            {
                using var conn = new SQLiteConnection(Db.ConnStr);
                conn.Open();

                string sql = "INSERT INTO Users (Username, PasswordHash) VALUES (@u, @p)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@u", username.Trim());
                cmd.Parameters.AddWithValue("@p", hash);
                cmd.ExecuteNonQuery();

                msg = "註冊成功";
                return true;
            }
            catch (SQLiteException ex)
            {
                msg = ex.Message.Contains("UNIQUE") ? "帳號已存在" : ex.Message;
                return false;
            }
        }

        public static bool Login(string username, string password, out string msg)
        {
            msg = "";

            using var conn = new SQLiteConnection(Db.ConnStr);
            conn.Open();

            string sql = "SELECT Id, PasswordHash FROM Users WHERE Username = @u";
            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@u", username.Trim());

            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                msg = "帳號不存在";
                return false;
            }

            string hash = reader["PasswordHash"].ToString() ?? "";

            if (!BCrypt.Net.BCrypt.Verify(password, hash))
            {
                msg = "密碼錯誤";
                return false;
            }

            Session.UserId = Convert.ToInt32(reader["Id"]);
            Session.Username = username.Trim();
            msg = "登入成功";
            return true;
        }
    }
}
