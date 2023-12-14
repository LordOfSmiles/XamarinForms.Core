using Xamarin.Core.Extensions;
using Xamarin.Core.Interfaces;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using XamarinForms.Core.Helpers;
using XamarinForms.Core.Infrastructure.Navigation;
using XamarinForms.Core.PlatformServices;
using XamarinForms.Core.ViewModels;
using NavigationPage = Xamarin.Forms.NavigationPage;

namespace XamarinForms.Core.Pages;

public abstract class ShellContentPage : ContentPage
{
    protected override void OnAppearing()
    {
        if (BindingContext is ViewModelBase vm)
        {
            if (vm is TabbedViewModelBase { IsActive: false })
            {
                return;
            }
            
            try
            {
                var parameters = NavigationHelper.Get(GetType().Name);
                vm.OnAppearingAsync(parameters).FireAndForget();
            }
            catch (Exception e)
            {
                //
            }
        }

        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        var vm = BindingContext as ViewModelBase;
        vm?.OnDisappearing();

        base.OnDisappearing();
    }

    protected override void OnParentSet()
    {
        if (Parent == null)
        {
            var vm = BindingContext as ViewModelBase;
            vm?.OnCleanup();
        }

        base.OnParentSet();
    }

    protected ShellContentPage()
    {
        On<iOS>().SetUseSafeArea(true);
        NavigationPage.SetBackButtonTitle(this, "");
        Shell.SetNavBarHasShadow(this, false);
    }
}