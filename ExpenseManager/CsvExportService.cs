using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExpenseManager
{
    public static class CsvExportService
    {
        public static void Export(string path, List<Record> records)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Date,Type,Category,Item,Amount");

            foreach (var r in records)
            {
                sb.AppendLine(
                    $"{Esc(r.Date)}," +
                    $"{Esc(r.Type)}," +
                    $"{Esc(r.Category)}," +
                    $"{Esc(r.Item)}," +
                    $"{r.Amount}"
                );
            }

            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
        }

        private static string Esc(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            if (s.Contains(",") || s.Contains("\""))
                return $"\"{s.Replace("\"", "\"\"")}\"";
            return s;
        }
    }
}
