using System;
using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Views
{
    public class CustomTabbedPage : TabbedPage
    {
        #region Bindable Properties

        #region TintColor

        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(CustomTabbedPage), Color.Default);

        public Color TintColor{
            get => (Color)GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }

        #endregion

        #endregion
    }
}
