using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using XamarinForms.Core.Infrastructure.Navigation;
using XamarinForms.Core.ViewModels;

namespace XamarinForms.Core.Pages;

public class ShellContentPage : ContentPage
{
    protected override async void OnAppearing()
    {
        if (BindingContext is ViewModelBase vm)
        {
            if (vm is TabbedViewModelBase { IsTabActive: false })
            {
                return;
            }

            try
            {
                var parameters = NavigationHelper.Get(GetType().Name);
                await vm.OnAppearingAsync(parameters);
            }
            catch
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
    }
}