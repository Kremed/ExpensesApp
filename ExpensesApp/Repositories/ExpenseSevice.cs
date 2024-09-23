namespace ExpensesApp.Repositories;

public class ExpenseSevice : IExpenseSevice
{
    public ISQLiteAsyncConnection DbConnection;

    public ExpenseSevice(ISqliteSevice sqliteSevice)
    {
        DbConnection = sqliteSevice.CreatConnection();
    }

    public async Task<bool> CreateNewExpenseAsync(Expense expense)
    {
        try
        {
            await DbConnection.InsertAsync(expense);
           
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> DeleteExpenseByIdAsync(int ID)
    {
        try
        {
            var selectedExpense = await DbConnection
                               .Table<Expense>()
                               .FirstOrDefaultAsync(user => user.Id == ID);

            if (selectedExpense is null)
                return false;

            await DbConnection.DeleteAsync(selectedExpense);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<List<Expense>> GetAllIExpenses()
    {
        return await DbConnection.Table<Expense>().ToListAsync();

    }
}
