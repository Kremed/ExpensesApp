namespace ExpensesApp.Views;

public partial class CategoriesMangmentView : ContentPage
{
    
    public ICategorySevice CategorySevice;
    public CategoriesMangmentView( ICategorySevice CategorySevice)
    {
        InitializeComponent();

        this.CategorySevice = CategorySevice;

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await GetAllCategories();
        });
    }

    private async Task GetAllCategories()
    {
        var items = await CategorySevice.GetAllICategories();
       
        CategoriesCollectionView.ItemsSource = items;
    }

    private async void BtnCreateNewCategory_Clicked(object sender, EventArgs e)
    {
        try
        {
            Category category = new Category { Name = TxtCategoryName.Text.Trim() };

            var isDoneSuccssefuly = await CategorySevice.CreateNewCategoryAsync(category);

            if (isDoneSuccssefuly )
            {
                await GetAllCategories();

                await DisplayAlert("عملية ناجحة", "تمت أضافة التصنيف بنجاح", "OK");

                TxtCategoryName.Text = "";
            }
            else
            {
                await DisplayAlert("عملية فاشلة", "لم تتم عملية حفظ التصنيف, الرجاء أعادة المحاولة مرة اخرئ", "OK");
            }
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
                var IsDeleted = await CategorySevice.DeleteCategoryByIdAsync(categoryID);

                if (IsDeleted)
                {
                    await GetAllCategories();
                }
                else
                {
                    await DisplayAlert("عملية فاشلة", "لم يتم حذف التصنيف , الرجاء أعادة المحاولة مرة اخرئ", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Exception", ex.Message, "OK");
        }
    }
}