using System.ComponentModel;
using Xamarin.Forms;
using XamarinForms.Core.Standard.Infrastructure.Navigation;
using XamarinForms.Core.ViewModels;

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
