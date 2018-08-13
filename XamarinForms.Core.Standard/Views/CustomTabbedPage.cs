using Xamarin.Core.Standard.ViewModels;
using Xamarin.Forms;
using XamarinForms.Core.Standard.Infrastructure.Navigation;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;

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

        private void Page_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedItem) || e.PropertyName == nameof(CurrentPage))
            {
                var page = CurrentPage as Page;
                if (page != null)
                {
                    var vm = page.BindingContext as ViewModelBase;
                    if (vm != null)
                    {
                        vm.OnAppearingAsync(NavigationState.GetParametersByPageType(page.GetType()));
                    }
                }
            }
        }

        #endregion
    }
}
