using System.ComponentModel;
using Xamarin.Core.ViewModels;
using Xamarin.Forms;
using XamarinForms.Core.Standard.Infrastructure.Navigation;

namespace XamarinForms.Core.Standard.Views
{
    public abstract class CustomTabbedPage : TabbedPage
    {
        protected override void OnPropertyChanged(string propertyName = null)
        {
            if (propertyName == nameof(SelectedItem) || propertyName == nameof(CurrentPage))
            {
                var vm = CurrentPage?.BindingContext as ViewModelBase;
                if (vm != null)
                {
                    vm.OnAppearingAsync(NavigationState.GetParametersByPageType(CurrentPage.GetType()));
                }
            }

            base.OnPropertyChanged(propertyName);
        }
    }
}
