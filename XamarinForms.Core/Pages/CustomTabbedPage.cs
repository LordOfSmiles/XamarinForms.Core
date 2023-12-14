using Xamarin.Core.Extensions;
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
    {
        Shell.SetNavBarHasShadow(this, false);
        On<iOS>().SetTranslucencyMode(TranslucencyMode.Opaque);
        
        Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetIsSwipePagingEnabled(this, false);
        Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetToolbarPlacement(this, ToolbarPlacement.Bottom);
        Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetOffscreenPageLimit(this, 1);
    }

    protected override void OnCurrentPageChanged()
    {
        var pages = Children.ToArray();
        
        foreach (var page in pages)
        {
            if (page.BindingContext is TabbedViewModelBase tabbedViewModel)
            {
                tabbedViewModel.IsActive = page == CurrentPage;
                
                // if (tabbedViewModel.IsTabActive)
                // {
                //     try
                //     {
                //         var parameters = NavigationHelper.Get(page.GetType().Name);
                //         //tabbedViewModel.OnAppearingAsync(parameters).FireAndForget();
                //     }
                //     catch
                //     {
                //         //
                //     }
                // }
            }
        }
        
        base.OnCurrentPageChanged();
    }
}