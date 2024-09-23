namespace ExpensesApp.Abstractions;

public interface ISqliteSevice
{
    ISQLiteAsyncConnection CreatConnection();
    Task<bool> InitiTables();
}
