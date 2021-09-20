using System;
using Xamarin.Core.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using XamarinForms.Core.Infrastructure.Navigation;
using XamarinForms.Core.ViewModels;
using FlyoutPage = Xamarin.Forms.FlyoutPage;

namespace XamarinForms.Core.Views
{
    public class ShellContentPage : ContentPage
    {
        protected override async void OnAppearing()
        {
            var vm = BindingContext as ViewModelBase;

            if (vm != null)
            {
                try
                {
                    await vm.OnAppearingAsync(NavigationHelper.GetParametersByPageType(GetType()));
                }
                catch (Exception ex)
                {
                    var logger = DependencyService.Get<ICrashlyticsService>();
                    logger?.TrackError(ex);
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

        public ShellContentPage()
        {
            On<iOS>().SetUseSafeArea(true);
        }
    }

    public abstract class ShellFlyoutPage : FlyoutPage
    {
        protected override async void OnAppearing()
        {
            var vm = BindingContext as ViewModelBase;

            if (vm != null)
            {
                try
                {
                    await vm.OnAppearingAsync(NavigationHelper.GetParametersByPageType(GetType()));
                }
                catch (Exception ex)
                {
                    var logger = DependencyService.Get<ICrashlyticsService>();
                    logger?.TrackError(ex);
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

        protected ShellFlyoutPage()
        {
            On<iOS>().SetUseSafeArea(true);
        }
    }
}