using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using XamarinForms.Core.Infrastructure.Navigation;
using XamarinForms.Core.ViewModels;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace XamarinForms.Core.Pages;

public abstract class CustomTabbedPage : TabbedPage
{
    protected CustomTabbedPage()
        : base()
    {
        Shell.SetNavBarHasShadow(this, false);
        On<iOS>().SetTranslucencyMode(TranslucencyMode.Opaque);
        
        Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetIsSwipePagingEnabled(this, false);
        Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetToolbarPlacement(this, ToolbarPlacement.Bottom);
        Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetOffscreenPageLimit(this, 1);
    }

    protected override async void OnCurrentPageChanged()
    {
        var pages = Children.ToArray();
        foreach (var page in pages)
        {
            var tabbedViewModel = page.BindingContext as TabbedViewModelBase;
            if (tabbedViewModel != null)
            {
                tabbedViewModel.IsTabActive = page == CurrentPage;
                
                if (tabbedViewModel.IsTabActive)
                {
                    try
                    {
                        var parameters = NavigationHelper.Get(page.GetType().Name);
                        await tabbedViewModel.OnAppearingAsync(parameters);
                    }
                    catch
                    {
                        //
                    }
                }
            }
        }
        
        base.OnCurrentPageChanged();
    }
}