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

        //Views =>
        builder.Services.AddTransient<CategoriesMangmentView>();
        builder.Services.AddSingleton<MainPageView>();

        return builder.Build();
    }
}
