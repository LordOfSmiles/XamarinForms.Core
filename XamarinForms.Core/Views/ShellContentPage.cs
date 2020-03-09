using Xamarin.Core.ViewModels;
using Xamarin.Forms;
using XamarinForms.Core.Standard.Infrastructure.Navigation;

namespace XamarinForms.Core.Views
{
    public abstract class ShellContentPage:ContentPage
    {
        protected override void OnAppearing()
        {
            var vm = BindingContext as ViewModelBase;
            vm?.OnAppearingAsync(NavigationState.GetParametersByPageType(GetType()));

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