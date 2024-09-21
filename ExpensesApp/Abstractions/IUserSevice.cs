namespace ExpensesApp.Abstractions;

public interface IUserSevice
{
    Task<List<UserTable>> GetAllUsers();
    Task<bool> CreateNewUseAsync(UserTable user);
    Task<bool> DeleteUserByIdAsync(int ID);
}
