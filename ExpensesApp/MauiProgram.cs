global using SQLite;
global using ExpensesApp.Views;
global using ExpensesApp.Repositories;
global using ExpensesApp.Abstractions;
global using ExpensesApp.Models.SqliteTabels;

namespace ExpensesApp;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
        .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });

        //sqlite-net-pcl
        //SQLitePCLRaw.bundle_green


        //A-01) IOC Container : Inversion of Control Container => 

        //Services =>
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

        builder.Services.AddSingleton<ISqliteSevice, SqliteSevice>();

        builder.Services.AddSingleton<IUserSevice, UserSevice>();

        builder.Services.AddSingleton<IExpenseSevice, ExpenseSevice>();

        builder.Services.AddSingleton<ICategorySevice, CategorySevice>();

        //Views =>
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<MainPageView>();
        builder.Services.AddTransient<ExpenseMangmentView>();
        builder.Services.AddTransient<CategoriesMangmentView>();

        return builder.Build();
    }
}
