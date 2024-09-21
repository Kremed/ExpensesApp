

namespace ExpensesApp.Views;
public partial class MainPageView : ContentPage
{
    public IConnectivity connectivityService;

    public IServiceProvider serviceProvider;
    public IUserSevice userSevice;

    public MainPageView(
        IUserSevice userSevice,
        IConnectivity connectivityService,
        IServiceProvider serviceProvider)
    {
        InitializeComponent();

        this.serviceProvider = serviceProvider;

        this.connectivityService = connectivityService;

        this.userSevice = userSevice;



        MainThread.BeginInvokeOnMainThread(async () =>
        {
            CheckInternetConniction();
        });
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
            var user = new UserTable
            {
                UserEamil = "Moatasemkremed@gmail.com",
                UserName = "Moatasem kremed",
                UserPhone = "0021892447464",
                UserPassword = "Aa21892447464"
            };
           
           
           var ffff = await userSevice.CreateNewUseAsync(user);









            //await Navigation.PushModalAsync(new CategoriesMangmentView());
            var page = serviceProvider.GetRequiredService<CategoriesMangmentView>();

            await Navigation.PushModalAsync(page);
        }
        catch (Exception)
        {
        }
    }
}