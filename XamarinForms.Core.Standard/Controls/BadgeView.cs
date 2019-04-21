using System.Security.Cryptography;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinForms.Core.Standard.Services;

namespace XamarinForms.Core.Standard.Controls
{
    public sealed class BadgeView:ContentView
    {
        #region Fields

        private readonly BoxView _badge;
        private readonly Label _lbl;
        
        #endregion

        public BadgeView()
        {
            var grd = new Grid()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            _badge = new BoxView()
            {
                CornerRadius = 11,
                HeightRequest = 22,
                MinimumWidthRequest = 22,
            };
            grd.Children.Add(_badge);

            _lbl = new Label()
            {
                Margin = new Thickness(4, 0),
                FontSize = DeviceService.OnPlatform(15, 14),
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                LineBreakMode = LineBreakMode.NoWrap,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            grd.Children.Add(_lbl);

            Content = grd;
        }

        #region Bindable Properties
        
        #region BadgeColor

        public static readonly BindableProperty BadgeColorProperty = BindableProperty.Create(nameof(BadgeColor), typeof(Color), typeof(BadgeView), Color.Transparent, propertyChanged: OnBadgeColorChanged);

        public Color BadgeColor
        {
            get => (Color) GetValue(BadgeColorProperty);
            set => SetValue(BadgeColorProperty, value);
        }

        private static void OnBadgeColorChanged(BindableObject bo, object oldValue, object newValue)
        {
            var ctrl = bo as BadgeView;
            if (ctrl == null)
                return;

            ctrl._badge.Color = (Color) newValue;
        }
        

        #endregion

        #region Text

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(BadgeView), string.Empty, propertyChanged: OnTextChanged);

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void OnTextChanged(BindableObject bo, object oldValue, object newValue)
        {
            var ctrl = bo as BadgeView;
            if (ctrl == null)
                return;

            if (newValue != null)
            {
                ctrl._lbl.Text = newValue.ToString();
            }
            else
            {
                ctrl._lbl.Text = string.Empty;
            }
        }

        #endregion
        
        #region TextColor

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(BadgeView), Color.Default, propertyChanged: OnTextColorChanged);

        public Color TextColor
        {
            get => (Color) GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        private static void OnTextColorChanged(BindableObject bo, object oldValue, object newValue)
        {
            var ctrl = bo as BadgeView;
            if (ctrl == null)
                return;

            ctrl._lbl.TextColor = (Color) newValue;
        }

        #endregion

        #endregion
    }
}