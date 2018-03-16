using System;

using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Controls
{
    public sealed class PlaceholderControl : ContentView
    {
        #region Fields

        private Label _lblHeader;

        #endregion

        public PlaceholderControl()
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

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(PlaceholderControl), string.Empty, propertyChanged: OnTextChanged);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void OnTextChanged(BindableObject bo, object oldValue, object newValue)
        {
            var ctrl = bo as PlaceholderControl;
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
            var grd = new Grid()
            {
                RowSpacing = 0,
                BackgroundColor = Color.White
            };
            grd.RowDefinitions.Add(new RowDefinition() { Height = 0.5 });
            grd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
            grd.RowDefinitions.Add(new RowDefinition() { Height = 0.5 });

            var topBorder = new BoxView()
            {
                Color = Color.FromRgb(238, 238, 238)
            };
            grd.Children.Add(topBorder);

            _lblHeader = new Label()
            {
                VerticalOptions = LayoutOptions.Center,
                FontSize = 12,
                TextColor = Color.Black,
                Margin = new Thickness(16, 8, 16, 8)
            };
            Grid.SetRow(_lblHeader, 1);
            grd.Children.Add(_lblHeader);

            var bottomBorder = new BoxView()
            {
                Color = Color.FromRgb(238, 238, 238)
            };
            Grid.SetRow(bottomBorder, 2);
            grd.Children.Add(bottomBorder);

            return grd;
        }

        #endregion
    }
}

