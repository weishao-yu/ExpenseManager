using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace ExpenseManager
{
    public static class RecordService
    {
        // ✅ 修正點 1：參數名稱改成 r 和 targetBookId，對應下面的程式碼
        public static void AddMyRecord(Record r, int targetBookId)
        {
            if (!Session.IsLoggedIn) return;

            using var conn = new SQLiteConnection(Db.ConnStr);
            conn.Open();

            string sql = @"
        INSERT INTO Records (UserId, BookId, Date, Item, Amount, Category, Type)
        VALUES (@UserId, @BookId, @Date, @Item, @Amount, @Category, @Type)";

            using (var cmd = new SQLiteCommand(sql, conn))
            {
                // ✅ 這裡建議直接用 Session.UserId，確保一定是寫入當前登入者的帳戶
                // (這樣就算你的 Record 物件沒設 UserId 也不會錯)
                cmd.Parameters.AddWithValue("@UserId", Session.UserId);

                // ✅ 這裡用 targetBookId (對應上面傳進來的參數)
                cmd.Parameters.AddWithValue("@BookId", targetBookId);

                cmd.Parameters.AddWithValue("@Date", r.Date);
                cmd.Parameters.AddWithValue("@Item", r.Item);
                cmd.Parameters.AddWithValue("@Amount", r.Amount);
                // 修正：防止 Category 是 null 導致錯誤
                cmd.Parameters.AddWithValue("@Category", r.Category ?? "");
                cmd.Parameters.AddWithValue("@Type", r.Type);

                cmd.ExecuteNonQuery();
            }
        }

        // ... 底下的 GetMyRecords, DeleteRecord, UpdateRecord 維持原樣即可 ...
        // (為了版面整潔，底下你可以保留你原本寫好的程式碼，不用更動)

        // ✅ 取得目前登入者在指定帳本的紀錄
        public static List<Record> GetMyRecords(int bookId)
        {
            var list = new List<Record>();
            if (!Session.IsLoggedIn) return list;

            using var conn = new SQLiteConnection(Db.ConnStr);
            conn.Open();

            string sql = @"
SELECT Id, Date, Item, Amount, Category, Type
FROM Records
WHERE UserId = @uid AND BookId = @bid
ORDER BY Date DESC, Id DESC";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uid", Session.UserId);
            cmd.Parameters.AddWithValue("@bid", bookId);

            using var reader = cmd.ExecuteReader(); // 改名 reader 比較清楚
            while (reader.Read())
            {
                list.Add(new Record
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Date = reader["Date"].ToString(),
                    Item = reader["Item"].ToString(),
                    Amount = Convert.ToDouble(reader["Amount"]),
                    Category = reader["Category"]?.ToString() ?? "",
                    Type = reader["Type"].ToString()
                    // 注意：讀出來時通常不需要設 UserId/BookId，除非你要拿去做其他運算
                });
            }

            return list;
        }

        public static void DeleteRecord(int recordId)
        {
            if (!Session.IsLoggedIn) return;

            using var conn = new SQLiteConnection(Db.ConnStr);
            conn.Open();

            string sql = "DELETE FROM Records WHERE Id = @id AND UserId = @uid";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", recordId);
            cmd.Parameters.AddWithValue("@uid", Session.UserId);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateRecord(Record rec)
        {
            if (!Session.IsLoggedIn) return;

            using var conn = new SQLiteConnection(Db.ConnStr);
            conn.Open();

            string sql = @"
UPDATE Records
SET Date = @date, Item = @item, Amount = @amt, Category = @cat, Type = @type
WHERE Id = @id AND UserId = @uid";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@date", rec.Date);
            cmd.Parameters.AddWithValue("@item", rec.Item);
            cmd.Parameters.AddWithValue("@amt", rec.Amount);
            cmd.Parameters.AddWithValue("@cat", rec.Category ?? "");
            cmd.Parameters.AddWithValue("@type", rec.Type);
            cmd.Parameters.AddWithValue("@id", rec.Id);
            cmd.Parameters.AddWithValue("@uid", Session.UserId);

            cmd.ExecuteNonQuery();
        }

        // 取得指定帳本的所有紀錄 (匯出用)
        public static List<Record> GetRecordsByBook(int bookId)
        {
            var list = new List<Record>();
            // 這裡不需要檢查 Session.IsLoggedIn，因為可能是匯出功能在用
            // 但如果要嚴謹一點，可以加上檢查

            using var conn = new SQLiteConnection(Db.ConnStr);
            conn.Open();

            string sql = @"
SELECT Id, Date, Item, Amount, Category, Type
FROM Records
WHERE BookId = @bid
ORDER BY Date DESC, Id DESC";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@bid", bookId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Record
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Date = reader["Date"].ToString(),
                    Item = reader["Item"].ToString(),
                    Amount = Convert.ToDouble(reader["Amount"]),
                    Category = reader["Category"]?.ToString() ?? "",
                    Type = reader["Type"].ToString()
                });
            }
            return list;
        }
    }
}