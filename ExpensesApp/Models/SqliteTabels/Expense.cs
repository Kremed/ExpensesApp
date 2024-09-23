namespace ExpensesApp.Models.SqliteTabels;

public class Expense
{
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Descption { get; set; }
    public double Amount { get; set; }
    public DateTime Time { get; set; } = DateTime.Now;
    public string CategoryName { get; set; }
}
