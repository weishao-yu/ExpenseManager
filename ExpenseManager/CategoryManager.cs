using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ExpenseManager
{
    public static class CategoryManager
    {
        private static readonly string filePath = "categories.json";

        // 主資料結構：支出 / 收入 各自有分類清單
        public static Dictionary<string, List<string>> Categories { get; private set; } = new()
        {
            { "支出", new List<string> { "餐飲", "交通", "娛樂", "購物", "生活", "醫療", "其他" } },
            { "收入", new List<string> { "薪資", "獎金", "紅包", "投資", "退款", "其他" } }
        };

        // === 載入分類 ===
        public static void Load()
        {
            if (!File.Exists(filePath))
            {
                Save(); // 沒檔案就建立預設檔
                return;
            }

            try
            {
                string json = File.ReadAllText(filePath);
                var data = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);
                if (data != null)
                    Categories = data;
            }
            catch
            {
                // 若檔案損壞則回預設值
                Categories = new()
                {
                    { "支出", new List<string> { "餐飲", "交通", "娛樂", "購物", "生活", "醫療", "其他" } },
                    { "收入", new List<string> { "薪資", "獎金", "紅包", "投資", "退款", "其他" } }
                };
                Save();
            }
        }

        // === 儲存分類 ===
        public static void Save()
        {
            var json = JsonSerializer.Serialize(Categories, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        // === 新增分類 ===
        public static void Add(string type, string category)
        {
            // 強制去空白
            category = category.Trim();
            if (string.IsNullOrEmpty(category)) return;

            if (!Categories.ContainsKey(type))
                Categories[type] = new List<string>();

            // 忽略大小寫比較 (如果你希望 "food" 和 "Food" 視為同一個)
            // 但中文通常沒差，用 Contains 即可
            if (!Categories[type].Contains(category))
            {
                Categories[type].Add(category);
                Save();
            }
        }

        // === 刪除分類 ===
        public static void Remove(string type, string category)
        {
            if (Categories.ContainsKey(type) && Categories[type].Contains(category))
            {
                Categories[type].Remove(category);
                Save();
            }
        }
    }
}
