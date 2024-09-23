namespace ExpensesApp.Views;
public partial class MainPageView : ContentPage
{
    public IConnectivity connectivityService;
    public IServiceProvider serviceProvider;

    public IExpenseSevice expenseSevice;

    public MainPageView(

        IExpenseSevice expenseSevice,
        IConnectivity connectivityService,
        IServiceProvider serviceProvider)
    {
        InitializeComponent();

        this.serviceProvider = serviceProvider;

        this.connectivityService = connectivityService;

        this.expenseSevice = expenseSevice;

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            CheckInternetConniction();

            await SetStartData();

        });
    }


    protected async override void OnAppearing()
    {
        base.OnAppearing();

        CheckInternetConniction();

        await SetStartData();
    }

    public async Task SetStartData()
    {
        try
        {
            var allExpenses = await expenseSevice.GetAllIExpenses();

            ExpensesCollection.ItemsSource = allExpenses.OrderByDescending(exp => exp.Time).Take(5);

            LblDailyExpenses.Text = allExpenses
                                    .Where(Expens => Expens.Time.Date == DateTime.Now.Date)
                                    .Sum(Expens => Expens.Amount)
                                    .ToString() + " LYD";

            LblMonthlyExpenses.Text = allExpenses
                                      .Where(Expens => Expens.Time.Month == DateTime.Now.Month && Expens.Time.Year == DateTime.Now.Year)
                                      .Sum(Expens => Expens.Amount)
                                      .ToString() + " LYD";

            LblYearlyExpenses.Text = allExpenses
                                      .Where(Expens => Expens.Time.Year == DateTime.Now.Year)
                                      .Sum(Expens => Expens.Amount)
                                      .ToString() + " LYD";
        }
        catch
        {
        }
    }

    private async void CheckInternetConniction()
    {
        if (connectivityService.NetworkAccess != NetworkAccess.Internet)
        {
            await DisplayAlert("Network Access", $"انت غير متصل بشبكة الانترنت الان نوع الاتصال الحالي هو : {connectivityService.NetworkAccess}", "OK");
            return;
        }
    }

    private async void BtnCheckConnection_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (connectivityService.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Network Access", $"انت غير متصل بشبكة الانترنت الان نوع الاتصال الحالي هو : {connectivityService.NetworkAccess}", "OK");
            }
            else
            {
                await DisplayAlert("Network Access", "انت متصل بشبكة الانترنت الان", "OK");

                var profiles = connectivityService.ConnectionProfiles;

                string profilesString = "";

                foreach (var profile in profiles)
                    profilesString += $"{Environment.NewLine}{profile.ToString()}";

                await DisplayAlert("Profiles", $"انت متصل بالانترنت عبر: {profilesString}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Exception", ex.Message, "OK");
        }
    }

    private async void BtnGoToCategoriesView_Clicked(object sender, EventArgs e)
    {
        try
        {
            var page = serviceProvider.GetRequiredService<CategoriesMangmentView>();

            await Navigation.PushModalAsync(page);
        }
        catch (Exception)
        {
        }
    }

    private async void BtnGoExpensesView_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(ExpenseMangmentView)}");

        //var page = serviceProvider.GetRequiredService<ExpenseMangmentView>();
        //
        //await Navigation.PushModalAsync(page);
    }
}