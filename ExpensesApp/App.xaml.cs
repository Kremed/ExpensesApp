namespace ExpensesApp;

public partial class App : Application
{
    //IServiceProvider يتم إنشاؤه تلقائيًا بواسطة إطار .NET 
    //IoC ولا يحتاج إلى تسجيله صراحةً في حاوية .
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        
        var page = serviceProvider.GetRequiredService<MainPageView>();

        MainPage = page;
    }
}
