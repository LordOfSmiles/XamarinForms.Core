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
            PropertyChanged += Page_PropertyChanged;
        }

        ~CustomTabbedPage()
        {
            PropertyChanged -= Page_PropertyChanged;
        }

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

        #region Handlers

        private void Page_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedItem) || e.PropertyName == nameof(CurrentPage))
            {
                var vm = CurrentPage?.BindingContext as ViewModelBase;
                if (vm != null)
                {
                    vm.OnAppearingAsync(NavigationState.GetParametersByPageType(CurrentPage.GetType()));
                }
            }
        }

        #endregion
    }
}
