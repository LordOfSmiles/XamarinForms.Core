using System.ComponentModel;
using Xamarin.Core.Standard.ViewModels;
using Xamarin.Forms;
using XamarinForms.Core.Standard.Infrastructure.Navigation;

namespace XamarinForms.Core.Standard.Views
{
    public abstract class CustomTabbedPage : TabbedPage
    {
        protected CustomTabbedPage()
        {
            Current = this;
        }
        
        public static TabbedPage Current { get; private set; }

        #region Bindable Properties

        #region TintColor

        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(CustomTabbedPage), Color.Default);

        public Color TintColor
        {
            get => (Color)GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }

        #endregion

        #endregion

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
