using System;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinForms.Core.Standard.Extensions;
using XamarinForms.Core.Standard.Services;

namespace XamarinForms.Core.Standard.Controls
{
    public sealed class FlatButtonWithImage : Grid
    {
        #region Fields
        
        private readonly Label _lbl;
        private readonly Image _img;
        private readonly Image _imgArrow;

        #endregion

        public FlatButtonWithImage()
        {
            ColumnDefinitions.Add(new ColumnDefinition() {Width = DeviceHelper.OnPlatform(25, 24)});
            ColumnDefinitions.Add(new ColumnDefinition());

            Padding = DeviceHelper.OnPlatform(new Thickness(16, 8, 8, 8), new Thickness(16, 12, 8, 12));
            ColumnSpacing = DeviceHelper.OnPlatform(16, 32);
            BackgroundColor = Color.White;

            _img = new Image()
            {
                HeightRequest = DeviceHelper.OnPlatform(25, 24),
                WidthRequest = DeviceHelper.OnPlatform(25, 24),
                VerticalOptions = LayoutOptions.Center
            };
            SetColumn(_img, 0);
            Children.Add(_img);

            _lbl = new Label()
            {
                TextColor = Color.Black,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Start
            };
            SetColumn(_lbl, 1);
            Children.Add(_lbl);
            
            if (Device.RuntimePlatform == Device.iOS)
            {
                ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = 18
                });
                _imgArrow = new Image()
                {
                    HeightRequest = 18,
                    WidthRequest = 18,
                    VerticalOptions = LayoutOptions.Center,
                    IsVisible = false
                };
                SetColumn(_imgArrow, 2);
                Children.Add(_imgArrow);
            }
            
            {
                var gesture = new TapGestureRecognizer();
                gesture.Tapped += GestureOnTapped;
                GestureRecognizers.Add(gesture);
            }
        }

        #region Bindable Properties

        #region Text

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(FlatButtonWithImage), string.Empty, propertyChanged: OnTextChanded);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void OnTextChanded(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as FlatButtonWithImage;
            if (ctrl == null)
                return;

            ctrl._lbl.Text = newvalue?.ToString() ?? string.Empty;
        }

        #endregion

        #region TextColor

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(FlatButtonWithImage), Color.Black, propertyChanged: OnTextColorChanged);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        private static void OnTextColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as FlatButtonWithImage;
            if (ctrl == null)
                return;

            ctrl._lbl.TextColor = (Color)newvalue;
        }

        #endregion
        
        #region FontAttributes

        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes),
            typeof(FontAttributes),
            typeof(FlatButtonWithImage),
            FontAttributes.None,
            propertyChanged: OnFontAttributesChanged);
        
        public FontAttributes FontAttributes
        {
            get => (FontAttributes) GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }
        
        private static void OnFontAttributesChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as FlatButtonWithImage;
            if (ctrl == null)
                return;

            ctrl._lbl.FontAttributes = (FontAttributes) newvalue;
        }

        
        #endregion

        #region Image

        public static readonly BindableProperty ImageProperty = BindableProperty.Create(nameof(Image), typeof(ImageSource), typeof(FlatButtonWithImage), null, propertyChanged: OnImageChanged);

        public ImageSource Image
        {
            get => (ImageSource)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        private static void OnImageChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as FlatButtonWithImage;
            if (ctrl == null)
                return;

            var image = newvalue as ImageSource;
            if (image != null)
            {
                ctrl._img.Source = image;
            }
        }

        #endregion

        #region BackImage

        public static readonly BindableProperty BackImageProperty = BindableProperty.Create(nameof(BackImage), typeof(ImageSource), typeof(FlatButtonWithImage), null, propertyChanged: OnBackImageChanged);

        public ImageSource BackImage
        {
            get => (ImageSource)GetValue(BackImageProperty);
            set => SetValue(BackImageProperty, value);
        }

        private static void OnBackImageChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (Device.RuntimePlatform != Device.iOS)
                return;

            var ctrl = bindable as FlatButtonWithImage;
            if (ctrl == null)
                return;

            var image = newvalue as ImageSource;
            if (image != null)
            {
                ctrl.ColumnDefinitions[2].Width = 18;
                ctrl._imgArrow.Source = image;
                ctrl._imgArrow.IsVisible = true;
            }
            else
            {
                ctrl.ColumnDefinitions[2].Width = 0;
                ctrl._imgArrow.Source = null;
                ctrl._imgArrow.IsVisible = false;
            }
        }

        #endregion

        #region Command

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(FlatButtonWithImage));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        #endregion

        #region CommandParameter

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(FlatButtonWithImage));

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        #endregion

        #endregion

        #region Handlers

        private async void GestureOnTapped(object sender, EventArgs e)
        {
            if (BackgroundColor.Equals(Color.Transparent) || BackgroundColor.IsDefault)
            {
                Command?.Execute(CommandParameter);
            }
            else
            {
                var startColor = BackgroundColor;
                var endColor = startColor.MultiplyAlpha(0.7);

                await this.ColorTo(endColor, 100);
                Command?.Execute(CommandParameter);
                await this.ColorTo(startColor, 100);
            }
        }

        #endregion
    }
}