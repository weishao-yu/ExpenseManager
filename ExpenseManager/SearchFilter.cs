using System;

namespace ExpenseManager
{
    public class SearchFilter
    {
        public DateTime DateFrom { get; set; } = DateTime.MinValue;
        public DateTime DateTo { get; set; } = DateTime.MaxValue;

        public string Category { get; set; } = "";
        public string Keyword { get; set; } = "";

        public double? MinAmount { get; set; }
        public double? MaxAmount { get; set; }

        public string Type { get; set; } = "";

        // ⭐ 關鍵：明確告訴主頁是不是 Reset
        public bool IsReset { get; set; } = false;
    }
}
