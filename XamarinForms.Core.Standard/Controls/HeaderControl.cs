using Xamarin.Forms;
using XamarinForms.Core.Standard.Services;

namespace XamarinForms.Core.Standard.Controls
{
    public sealed class HeaderControl : ContentView
    {
        #region Fields

        private Label _lblHeader;
        private View _viewRoot;

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

        #region TextColor

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(HeaderControl), Color.Default, propertyChanged: OnTextColorChanged);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        private static void OnTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as HeaderControl;
            if (ctrl == null)
                return;

            ctrl._lblHeader.TextColor = (Color)newValue;
        }

        #endregion

        #region FontSize

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(HeaderControl), DeviceService.OnPlatform(13.0, 14.0), propertyChanged: OnFontSizeChanged);

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        private static void OnFontSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as HeaderControl;
            if (ctrl == null)
                return;

            ctrl._lblHeader.FontSize = (double)newValue;
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

            var contentView = ctrl._viewRoot as ContentView;
            if (contentView != null)
                contentView.Padding = (Thickness) newValue;
        }

        #endregion

        #endregion

        #region Private Methods

        private View GetAndroidControl()
        {
            _lblHeader = new Label()
            {
                VerticalOptions = LayoutOptions.Center,
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Accent
            };

            _viewRoot = this;
            return _lblHeader;
        }

        private View GetIosControl()
        {
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


            _viewRoot = cnv;
            return cnv;
        }

        #endregion
    }
}

