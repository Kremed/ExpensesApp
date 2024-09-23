namespace ExpensesApp.Abstractions;

public interface ICategorySevice
{
    Task<List<Category>> GetAllICategories();
    Task<bool> CreateNewCategoryAsync(Category category);
    Task<bool> DeleteCategoryByIdAsync(int ID);
}
