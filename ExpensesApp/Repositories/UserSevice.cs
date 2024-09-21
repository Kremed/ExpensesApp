namespace ExpensesApp.Repositories;

public class UserSevice : IUserSevice
{
    public ISQLiteAsyncConnection DbConnection;

    public UserSevice(ISqliteSevice sqliteSevice)
    {
        DbConnection = sqliteSevice.CreatConnection();
    }
    public async Task<bool> CreateNewUseAsync(UserTable user)
    {
        try
        {
            await DbConnection.InsertAsync(user);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> DeleteUserByIdAsync(int ID)
    {
        try
        {
            var selectedUser = await DbConnection
                               .Table<UserTable>()
                               .FirstOrDefaultAsync(user => user.ID == ID);

            if (selectedUser is null)
                return false;

            await DbConnection.DeleteAsync(selectedUser);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<List<UserTable>> GetAllUsers()
    {
        return await DbConnection
                             .Table<UserTable>()
                             .ToListAsync();

    }
}
