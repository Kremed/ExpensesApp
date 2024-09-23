namespace ExpensesApp.Views;

public partial class ExpenseMangmentView : ContentPage
{
    public ICategorySevice categorySevice;
    public IExpenseSevice expenseSevice;

    public ExpenseMangmentView(
        ICategorySevice categorySevice,
        IExpenseSevice expenseSevice)
    {
        InitializeComponent();

        this.expenseSevice = expenseSevice;

        this.categorySevice = categorySevice;

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var CategoriesList = await categorySevice.GetAllICategories();

            categoryPicer.ItemsSource = CategoriesList;

            categoryPicer.SelectedItem = CategoriesList.FirstOrDefault();
        });
    }

    public void ClearInbut()
    {
        TxtDescrbtion.Text = "";
        TxtTitle.Text = "";
        Amount.Text = "";
    }


    private async void BtnCreateExpeenses_Clicked(object sender, EventArgs e)
    {
        try
        {
            var item = categoryPicer.SelectedItem as Category;
            var selectedItemName = item?.Name;

            Expense expense = new()
            {
                Title = TxtTitle.Text,
                Descption = TxtDescrbtion.Text,
                Amount = Convert.ToDouble(Amount.Text),
                CategoryName = (categoryPicer.SelectedItem as Category)?.Name!,
                //Time = DateTime.Now,
            };

            var IsSucsseccfuly = await expenseSevice.CreateNewExpenseAsync(expense);

            if (IsSucsseccfuly)
            {
                await DisplayAlert("نجاح العملية", "تمت العملية وخلاص", "OK");
                ClearInbut();
            }
            //await Navigation.PopModalAsync();
            else
                await DisplayAlert("فشلت العملية", "لم تتم عملية الحفظ بنجاح, الرجاء تعبئة جميع الحقول والمحاولة مرة اخرئ", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("System Exception", ex.Message, "OK");
        }
    }
}