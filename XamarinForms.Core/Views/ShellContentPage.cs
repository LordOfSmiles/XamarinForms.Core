
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForms.Core.Standard.Infrastructure.Navigation;
using XamarinForms.Core.ViewModels;

namespace XamarinForms.Core.Views
{
    public abstract class ShellContentPage:ContentPage
    {
        protected override void OnAppearing()
        {
            var vm = BindingContext as ViewModelBase;
            if (vm != null)
            {
                try
                {
                    Task.Run(async () => await vm.OnAppearingAsync(NavigationState.GetParametersByPageType(GetType())));
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
    }
}