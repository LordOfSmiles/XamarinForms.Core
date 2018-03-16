using System;

using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Controls
{
    public sealed class HeaderControl : ContentView
    {
        #region Fields

        private Label _lblHeader;

        #endregion

        public HeaderControl()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    Content = GetIosControl();
                    break;
                case Device.Android:
                    Content = GetAndroidControl();
                    break;
            }
        }

        #region Bindable proeprties

        #region Text

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(HeaderControl), string.Empty, propertyChanged: OnTextChanged);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void OnTextChanged(BindableObject bo, object oldValue, object newValue)
        {
            var ctrl = bo as HeaderControl;
            if (ctrl == null)
                return;

            ctrl._lblHeader.Text = newValue?.ToString().ToUpper() ?? "";
        }

        #endregion

        #endregion

        #region Private Methods

        private View GetAndroidControl()
        {
            return new ContentView();
        }

        private View GetIosControl()
        {
            var grd = new ContentView()
            {
                BackgroundColor = Color.FromRgb(240, 239, 244)
            };

            _lblHeader = new Label()
            {
                VerticalOptions = LayoutOptions.Center,
                FontSize = 12,
                TextColor = Color.FromRgb(110, 110, 110),
                Margin = new Thickness(16, 8, 4, 8)
            };
            grd.Content = _lblHeader;

            return grd;
        }

        #endregion
    }
}

