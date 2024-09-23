using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Platform.Compatibility;

namespace ExpensesApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(MainPageView), typeof(MainPageView));
        Routing.RegisterRoute(nameof(CategoriesMangmentView), typeof(CategoriesMangmentView));
        Routing.RegisterRoute(nameof(ExpenseMangmentView), typeof(ExpenseMangmentView));
        

        //Routing.RegisterRoute("MainPageView", typeof(MainPageView));
    }


    //fixing App Shell FlowDiriction Issue github [https://github.com/dotnet/maui/pull/23473#issuecomment-2309531487]
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        try
        {
#if ANDROID
            var platformView = Handler!.PlatformView;
            var shellFlyoutRenderer = (ShellFlyoutRenderer)platformView!;
            shellFlyoutRenderer!.LayoutDirection = Android.Views.LayoutDirection.Rtl;
#elif IOS || MACCATALYST
            FlowDirection = FlowDirection.RightToLeft;
#endif
        }
        catch
        {
        }
    }
}