namespace ExpenseManager
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // 方便除錯用（可選）
        public override string ToString() => Name;
    }
}
