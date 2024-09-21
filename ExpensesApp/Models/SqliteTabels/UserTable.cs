namespace ExpensesApp.Models.SqliteTabels;

//[Table(name: "UserTable")]
public class UserTable
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string UserName { get; set; }
    public string UserEamil { get; set; }
    public string UserPhone { get; set; }
    public string UserPassword { get; set; }
}
