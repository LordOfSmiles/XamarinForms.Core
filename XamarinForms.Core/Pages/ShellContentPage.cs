using System.Linq;
using Xamarin.Core.Infrastructure.Container;
using Xamarin.Core.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using XamarinForms.Core.Infrastructure.Navigation;
using XamarinForms.Core.ViewModels;

namespace XamarinForms.Core.Views;

public class ShellContentPage : ContentPage
{
    protected override async void OnAppearing()
    {
        if (BindingContext is ViewModelBase vm)
        {
            try
            {
                var parameters = NavigationHelper.GetParametersByPageType(GetType());
                if (!parameters.Any())
                {
                    parameters = NavigationHelper.Get(GetType().Name);
                }

                await vm.OnAppearingAsync(parameters);
            }
            catch (Exception ex)
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

    protected ShellContentPage()
    {
        On<iOS>().SetUseSafeArea(true);
    }
}