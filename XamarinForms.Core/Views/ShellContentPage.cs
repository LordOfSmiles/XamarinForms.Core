using System;
using Xamarin.Core.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using XamarinForms.Core.Infrastructure.Navigation;
using XamarinForms.Core.ViewModels;

namespace XamarinForms.Core.Views
{
    public abstract class ShellContentPage:ContentPage
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
                catch
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

        protected ShellContentPage()
        {
            On<iOS>().SetUseSafeArea(true);
        }
    }
}