using System.ComponentModel;

public class Record
{
    // 👇 2. 加上這行，這個欄位就不會出現在 DataGridView
    [Browsable(false)]
    public int Id { get; set; }

    // 👇 這樣寫，UI 就不會顯示 UserId，但程式照樣能運作
    [Browsable(false)]
    public int UserId { get; set; }

    // 👇 同理，把 BookId 也藏起來
    [Browsable(false)]
    public int BookId { get; set; }

    public string Date { get; set; }
    public string Item { get; set; }
    public double Amount { get; set; }
    public string Category { get; set; }
    public string Type { get; set; }

    public Record() { }

    // 建構子可以不用改，因為匯入時我們是直接設定屬性的
    public Record(string date, string item, double amount, string category, string type)
    {
        Date = date;
        Item = item;
        Amount = amount;
        Category = category;
        Type = type;
    }
}