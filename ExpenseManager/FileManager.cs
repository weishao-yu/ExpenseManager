using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; // 記得加這個，才能用 Skip

namespace ExpenseManager
{
    public static class FileManager
    {
        // 配合 CsvExportService 的格式讀取
        public static List<Record> Load(string path)
        {
            var list = new List<Record>();

            if (!File.Exists(path)) return list;

            var lines = File.ReadAllLines(path);

            // 1. 檢查檔案是否有內容
            if (lines.Length == 0) return list;

            // 2. 使用 Skip(1) 跳過第一行標題 (Date,Type,Category...)
            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                // 簡單切割 (假設內容沒有額外逗號)
                var parts = line.Split(',');

                // 確保至少有 5 個欄位
                if (parts.Length < 5) continue;

                try
                {
                    // 3. 去除可能存在的引號 (因為 CsvExportService 有加 Esc)
                    string date = Clean(parts[0]);
                    string type = Clean(parts[1]);
                    string category = Clean(parts[2]);
                    string item = Clean(parts[3]);
                    string amountStr = Clean(parts[4]);

                    // 4. 正確對應欄位順序
                    // 匯出順序: Date, Type, Category, Item, Amount
                    var r = new Record
                    {
                        Date = date,
                        Type = type,
                        Category = category,
                        Item = item,
                        Amount = double.Parse(amountStr), // 這裡現在是對應到正確的數字欄位了

                        // 雖然這裡沒設 Id, UserId, BookId，
                        // 但在 FormBookManager 匯入迴圈裡我們會手動覆蓋，所以沒關係
                    };

                    list.Add(r);
                }
                catch
                {
                    // 這裡如果解析失敗會跳過該行
                    // System.Diagnostics.Debug.WriteLine("解析失敗: " + line);
                }
            }

            return list;
        }

        // 輔助方法：去除 CSV 的引號
        private static string Clean(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            // 去除前後空白，並把雙引號 "" 變回 "，再去掉頭尾的 "
            return input.Trim().Replace("\"\"", "\"").Trim('"');
        }
    }
}