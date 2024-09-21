
namespace ExpensesApp.Repositories;

public class SqliteSevice : ISqliteSevice
{
    public static ISQLiteAsyncConnection DbConnection = null!;

    public SqliteSevice()
    {
        Task.Run(async () =>
        {
            CreatConnection();
            await InitiTables();
        });
    }

    public ISQLiteAsyncConnection CreatConnection()
    {
        if (DbConnection is null)
        {
            DbConnection = new SQLiteAsyncConnection(Path.Combine(
                               FileSystem.AppDataDirectory, "ExpensesAppLocalDB.db3"),
                               SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite);
        }

        return DbConnection;

    }

    public async Task<bool> InitiTables()
    {
        try
        {
            await DbConnection.CreateTableAsync<UserTable>();

            await DbConnection.CreateTableAsync<Category>();

            await DbConnection.CreateTableAsync<Expense>();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

}
