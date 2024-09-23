namespace ExpensesApp.Abstractions;

public interface IExpenseSevice
{
    Task<List<Expense>> GetAllIExpenses();
    Task<bool> CreateNewExpenseAsync(Expense expense);
    Task<bool> DeleteExpenseByIdAsync(int ID);
}
