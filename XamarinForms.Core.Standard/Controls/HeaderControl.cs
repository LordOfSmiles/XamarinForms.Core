using System;

using Xamarin.Forms;
using System.Reflection;

namespace XamarinForms.Core.Standard.Controls
{
    public sealed class HeaderControl : ContentView
    {
        #region Fields

        private Label _lblHeader;
        private View _viewRoot;

        private BoxView _bxTopBorder;
        private BoxView _bxBottomBorder;

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

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    ctrl._lblHeader.Text = newValue?.ToString() ?? "";
                    break;
                case Device.iOS:
                    ctrl._lblHeader.Text = newValue?.ToString().ToUpper() ?? "";
                    break;
            }
        }

        #endregion

        #region HeaderPadding

        public static readonly BindableProperty HeaderPaddingProperty = BindableProperty.Create(nameof(HeaderPadding), typeof(Thickness), typeof(HeaderControl), new Thickness(0), propertyChanged: OnHeaderPaddingChanged);

        public Thickness HeaderPadding
        {
            get => (Thickness)GetValue(HeaderPaddingProperty);
            set => SetValue(HeaderPaddingProperty, value);
        }

        private static void OnHeaderPaddingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as HeaderControl;
            if (ctrl == null)
                return;
            if (ctrl._viewRoot is ContentView)
                ((ContentView)ctrl._viewRoot).Padding = (Thickness)newValue;
        }

        #endregion

        #region IsTopBorderVisible

        public static readonly BindableProperty IsTopBorderVisibleProperty = BindableProperty.Create(nameof(IsTopBorderVisible), typeof(bool), typeof(HeaderControl), true, propertyChanged: OnIsTopBorderVisibleChanged);

        public bool IsTopBorderVisible
        {
            get
            {
                return (bool)GetValue(IsTopBorderVisibleProperty);
            }
            set => SetValue(IsTopBorderVisibleProperty, value);
        }

        private static void OnIsTopBorderVisibleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as HeaderControl;
            if (ctrl == null)
                return;

            if (ctrl._bxTopBorder == null)
                return;

            ctrl._bxTopBorder.IsVisible = (bool)newValue;
        }

        #endregion

        #region IsBottomBorderVisible

        public static readonly BindableProperty IsBottomBorderVisibleProperty = BindableProperty.Create(nameof(IsBottomBorderVisible), typeof(bool), typeof(HeaderControl), true, propertyChanged: OnIsBottomBorderVisibleChanged);

        public bool IsBottomBorderVisible
        {
            get
            {
                return (bool)GetValue(IsBottomBorderVisibleProperty);
            }
            set => SetValue(IsBottomBorderVisibleProperty, value);
        }

        private static void OnIsBottomBorderVisibleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as HeaderControl;
            if (ctrl == null)
                return;

            if (ctrl._bxBottomBorder == null)
                return;

            ctrl._bxBottomBorder.IsVisible = (bool)newValue;
        }

        #endregion

        #endregion

        #region Private Methods

        private View GetAndroidControl()
        {
            var stack = new StackLayout() { Spacing = 0 };

            _bxTopBorder = new BoxView()
            {
                HeightRequest = 1,
                Color = Color.Black
            };
            stack.Children.Add(_bxTopBorder);

            var cnv = new ContentView();
            _lblHeader = new Label()
            {
                VerticalOptions = LayoutOptions.Center,
                FontSize = 14,
                FontAttributes = FontAttributes.Bold
            };
            cnv.Content = _lblHeader;
            stack.Children.Add(cnv);

            _bxBottomBorder = new BoxView()
            {
                HeightRequest = 1,
                Color = Color.FromRgb(244, 244, 244)
            };
            stack.Children.Add(_bxBottomBorder);

            _viewRoot = cnv;
            return stack;
        }

        private View GetIosControl()
        {
            var stack = new StackLayout() { Spacing = 0 };

            _bxTopBorder = new BoxView()
            {
                HeightRequest = 1,
                Color = Color.FromRgb(215, 215, 215)
            };
            stack.Children.Add(_bxTopBorder);

            var cnv = new ContentView()
            {
                BackgroundColor = Color.FromRgb(238, 238, 238)
            };

            _lblHeader = new Label()
            {
                VerticalOptions = LayoutOptions.Center,
                FontSize = 13,
                TextColor = Color.FromRgb(110, 110, 110)
            };
            cnv.Content = _lblHeader;
            stack.Children.Add(cnv);

            _bxBottomBorder = new BoxView()
            {
                HeightRequest = 1,
                Color = Color.FromRgb(215, 215, 215)
            };
            stack.Children.Add(_bxBottomBorder);

            _viewRoot = cnv;
            return stack;
        }

        #endregion
    }
}

