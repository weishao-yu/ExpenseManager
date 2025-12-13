using System.Collections.Generic;
using System.Data.SQLite;

namespace ExpenseManager
{
    public static class BookService
    {
        // 取得目前使用者的所有帳本
        public static List<Book> GetMyBooks()
        {
            var list = new List<Book>();

            using var conn = new SQLiteConnection(Db.ConnStr);
            conn.Open();

            string sql = "SELECT Id, Name FROM Books WHERE UserId = @uid";
            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uid", Session.UserId);

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new Book
                {
                    Id = r.GetInt32(0),
                    Name = r.GetString(1)
                });
            }

            return list;
        }


        // 確保至少有一個帳本
        public static int EnsureDefaultBook()
        {
            var books = GetMyBooks();
            if (books.Count > 0)
                return books[0].Id;

            using var conn = new SQLiteConnection(Db.ConnStr);
            conn.Open();

            string sql = "INSERT INTO Books (UserId, Name) VALUES (@uid, '生活')";
            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uid", Session.UserId);
            cmd.ExecuteNonQuery();

            return (int)conn.LastInsertRowId;
        }
        public static int AddBook(string name)
        {
            using var conn = new SQLiteConnection(Db.ConnStr);
            conn.Open();

            string sql = "INSERT INTO Books (UserId, Name) VALUES (@uid, @name)";
            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uid", Session.UserId);
            cmd.Parameters.AddWithValue("@name", name.Trim());
            cmd.ExecuteNonQuery();

            return (int)conn.LastInsertRowId;
        }
        public static void RenameBook(int bookId, string newName)
        {
            using var conn = new SQLiteConnection(Db.ConnStr);
            conn.Open();

            string sql = "UPDATE Books SET Name = @name WHERE Id = @id AND UserId = @uid";
            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", newName.Trim());
            cmd.Parameters.AddWithValue("@id", bookId);
            cmd.Parameters.AddWithValue("@uid", Session.UserId);
            cmd.ExecuteNonQuery();
        }
        public static void DeleteBook(int bookId)
        {
            using var conn = new SQLiteConnection(Db.ConnStr);
            conn.Open();

            using var tx = conn.BeginTransaction();

            // 先刪該帳本底下的紀錄
            string delRecords = "DELETE FROM Records WHERE BookId = @bid AND UserId = @uid";
            using (var cmd1 = new SQLiteCommand(delRecords, conn, tx))
            {
                cmd1.Parameters.AddWithValue("@bid", bookId);
                cmd1.Parameters.AddWithValue("@uid", Session.UserId);
                cmd1.ExecuteNonQuery();
            }

            // 再刪帳本
            string delBook = "DELETE FROM Books WHERE Id = @bid AND UserId = @uid";
            using (var cmd2 = new SQLiteCommand(delBook, conn, tx))
            {
                cmd2.Parameters.AddWithValue("@bid", bookId);
                cmd2.Parameters.AddWithValue("@uid", Session.UserId);
                cmd2.ExecuteNonQuery();
            }

            tx.Commit();
        }



    }
}
