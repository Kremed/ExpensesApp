namespace ExpensesApp.Repositories;

public class CategorySevice : ICategorySevice
{
    public ISQLiteAsyncConnection DbConnection;

    public CategorySevice(ISqliteSevice sqliteSevice)
    {
        DbConnection = sqliteSevice.CreatConnection();
    }

    public async Task<bool> CreateNewCategoryAsync(Category category)
    {
        try
        {
            await DbConnection.InsertAsync(category);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> DeleteCategoryByIdAsync(int ID)
    {
        try
        {
            var selectedCategory = await DbConnection
                               .Table<Category>()
                               .FirstOrDefaultAsync(user => user.Id == ID);

            if (selectedCategory is null)
                return false;

            await DbConnection.DeleteAsync(selectedCategory);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<List<Category>> GetAllICategories()
    {
        return await DbConnection.Table<Category>().ToListAsync();

    }

   
}
