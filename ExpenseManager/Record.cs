namespace ExpenseManager
{
    public class Record
    {
        public string Date { get; set; }
        public string Item { get; set; }
        public double Amount { get; set; }
        public string Category { get; set; }
        public string Type { get; set; } // ✅ 收入或支出

        public Record(string date, string item, double amount, string category, string type)
        {
            Date = date;
            Item = item;
            Amount = amount;
            Category = category;
            Type = type;
        }
    }
}
