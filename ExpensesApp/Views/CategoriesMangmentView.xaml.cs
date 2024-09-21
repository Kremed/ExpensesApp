namespace ExpensesApp.Views;

public partial class CategoriesMangmentView : ContentPage
{
    public ISQLiteAsyncConnection Db = null!;
    //public SQLiteConnectionRepository sqliteConnectionService;

    public CategoriesMangmentView()
    {
        InitializeComponent();

        //  this.sqliteConnectionService = sqliteConnectionService;


        MainThread.BeginInvokeOnMainThread(async () =>
        {
            //var isSuccessful = await sqliteConnectionService.InitiDataBaseTabels();

            //if (isSuccessful is not true)
            //    await DisplayAlert("Issue in Initi Data Base", "لقد واجهنا مشكلة اثناء انشاء قاعدة البيانات المحلية.", "OK");

            //Db = sqliteConnectionService.CreateConnection();

            await GetAllCategories();
        });
    }

    private async Task GetAllCategories()
    {
        if (Db is not null)
        {
            CategoriesCollectionView.ItemsSource = await Db.Table<Category>().ToListAsync();
        }
    }

    private async void BtnCreateNewCategory_Clicked(object sender, EventArgs e)
    {
        try
        {
            Category category = new Category { Name = TxtCategoryName.Text.Trim() };

            await Db.InsertAsync(category);

            await GetAllCategories();

            await DisplayAlert("عملية ناجحة", "تمت أضافة التصنيف بنجاح", "OK");

            TxtCategoryName.Text = "";
        }
        catch (Exception ex)
        {
            await DisplayAlert("Exception", ex.Message, "OK");
        }
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            if (int.TryParse(e.Parameter!.ToString(), out var categoryID))
            {
                var selectedCategory = await Db.Table<Category>().FirstOrDefaultAsync(cat => cat.Id == categoryID);

                await Db.DeleteAsync(selectedCategory);

                await GetAllCategories();
            }
        }
        catch (Exception)
        {
        }
    }
}