using System;
using Xamarin.Core.Infrastructure.Container;
using Xamarin.Core.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using XamarinForms.Core.Infrastructure.Navigation;
using XamarinForms.Core.ViewModels;

namespace XamarinForms.Core.Views
{
    public class ShellContentPage : ContentPage
    {
        protected override async void OnAppearing()
        {
            if (BindingContext is ViewModelBase vm)
            {
                try
                {
                    await vm.OnAppearingAsync(NavigationHelper.GetParametersByPageType(GetType()));
                }
                catch (Exception ex)
                {
                    var logger = FastContainer.TryResolve<ICrashlytics>();
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
}