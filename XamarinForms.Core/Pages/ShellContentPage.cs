using Xamarin.Core.Infrastructure.Container;
using Xamarin.Core.Interfaces;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using XamarinForms.Core.Helpers;
using XamarinForms.Core.Infrastructure.Navigation;
using XamarinForms.Core.PlatformServices;
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

            var from = Shell.Current?.CurrentPage.GetType();
            if (from != null)
            {
                var analyticsService = FastContainer.TryResolve<IAnalyticsService>();
                analyticsService?.OnNavigation(from.Name, GetType().Name);
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

    #region Methods

    protected Thickness GetBottomButtonMargin()
    {
        var bottom = 16;

        if (DeviceHelper.IsIos
            && !On<iOS>().UsingSafeArea()
            && DependencyService.Get<IExtendedDevicePlatformService>().IsDeviceWithSafeArea)
        {
            bottom = 50;
        }

        return new Thickness(0, 0, 0, bottom);
    }

    #endregion
}