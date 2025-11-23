using System;
using System.Collections.Generic;
using System.IO;

namespace ExpenseManager
{
    /// <summary>
    /// FileManager：負責記帳資料的讀取與儲存（支援收入/支出）
    /// </summary>
    public static class FileManager
    {
        // === 儲存記帳資料至 CSV 檔案 ===
        public static void Save(List<Record> records, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (var r in records)
                {
                    // ✅ 五欄版本（含 Type）
                    sw.WriteLine($"{r.Date},{r.Item},{r.Amount},{r.Category},{r.Type}");
                }
            }
        }

        // === 從 CSV 檔案載入記帳資料 ===
        public static List<Record> Load(string path)
        {
            List<Record> records = new();

            if (!File.Exists(path))
                return records;

            foreach (var line in File.ReadAllLines(path))
            {
                var parts = line.Split(',');

                if (parts.Length < 5) continue; // 不是新版格式就跳過

                try
                {
                    records.Add(new Record(
                        parts[0],
                        parts[1],
                        double.Parse(parts[2]),
                        parts[3],
                        parts[4]
                    ));
                }
                catch
                {
                    continue; // 有問題的行就跳過
                }
            }

            return records;
        }

    }
}

