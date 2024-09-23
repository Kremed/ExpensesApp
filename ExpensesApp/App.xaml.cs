


namespace ExpensesApp;

public partial class App : Application
{
    //IServiceProvider يتم إنشاؤه تلقائيًا بواسطة إطار .NET 
    //IoC ولا يحتاج إلى تسجيله صراحةً في حاوية .
    //IServiceProvider serviceProvider
    public App()
    {
        InitializeComponent();
        
       // var page = serviceProvider.GetRequiredService<MainPageView>();

        MainPage = new AppShell();
    }

}
