using Xamarin.Forms;
using XamarinForms.Core.Infrastructure.Navigation;
using XamarinForms.Core.ViewModels;

namespace XamarinForms.Core.Views
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
                    vm.OnAppearingAsync(NavigationHelper.GetParametersByPageType(CurrentPage.GetType()));
                }
            }

            base.OnPropertyChanged(propertyName);
        }
    }
}
