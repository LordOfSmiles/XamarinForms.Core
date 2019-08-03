using System;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinForms.Core.Standard.Extensions;
using XamarinForms.Core.Standard.Services;

namespace XamarinForms.Core.Standard.Controls
{
    public sealed class IosFlatButton : StackLayout
    {
        #region Fields

        private readonly View _viewContent;
        private readonly Label _lbl;
        private readonly Image _img;
        private readonly Image _imgArrow;

        #endregion

        public IosFlatButton()
        {
            Padding = DeviceService.OnPlatform(new Thickness(16, 8, 8, 8), new Thickness(16, 12, 8, 12));
            Orientation = StackOrientation.Horizontal;
            Spacing = 8;
            BackgroundColor = Color.Transparent;

            {
                _img = new Image()
                {
                    HeightRequest = DeviceService.OnPlatform(25, 24),
                    WidthRequest = DeviceService.OnPlatform(25, 24),
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 0, 8, 0),
                    IsVisible = false
                };
                Children.Add(_img);

                _lbl = new Label()
                {
                    TextColor = Color.Black,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };
                Children.Add(_lbl);


                if (Device.RuntimePlatform == Device.iOS)
                {
                    _imgArrow = new Image()
                    {
                        HeightRequest = 18,
                        WidthRequest = 18,
                        VerticalOptions = LayoutOptions.Center,
                        IsVisible = false
                    };
                    Children.Add(_imgArrow);
                }

                _viewContent = this;
            }


            {
                var gesture = new TapGestureRecognizer();
                gesture.Tapped += GestureOnTapped;
                GestureRecognizers.Add(gesture);
            }

        }

        #region Bindable Properties

        #region Text

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(IosFlatButton), string.Empty, propertyChanged: OnTextChanded);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void OnTextChanded(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as IosFlatButton;
            if (ctrl == null)
                return;

            ctrl._lbl.Text = newvalue?.ToString() ?? string.Empty;
        }

        #endregion

        #region TextColor

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(IosFlatButton), Color.Black, propertyChanged: OnTextColorChanged);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        private static void OnTextColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as IosFlatButton;
            if (ctrl == null)
                return;

            ctrl._lbl.TextColor = (Color)newvalue;
        }

        #endregion
        
        #region FontAttributes

        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes),
            typeof(FontAttributes),
            typeof(IosFlatButton),
            FontAttributes.None,
            propertyChanged: OnFontAttributesChanged);
        
        public FontAttributes FontAttributes
        {
            get => (FontAttributes) GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }
        
        private static void OnFontAttributesChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as IosFlatButton;
            if (ctrl == null)
                return;

            ctrl._lbl.FontAttributes = (Xamarin.Forms.FontAttributes) newvalue;
        }

        
        #endregion

        #region Image

        public static readonly BindableProperty ImageProperty = BindableProperty.Create(nameof(Image), typeof(ImageSource), typeof(IosFlatButton), null, propertyChanged: OnImageChanged);

        public ImageSource Image
        {
            get => (ImageSource)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        private static void OnImageChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as IosFlatButton;
            if (ctrl == null)
                return;

            var image = newvalue as ImageSource;
            if (image != null)
            {
                ctrl._img.Source = image;
                ctrl._img.IsVisible = true;
                ctrl._lbl.HorizontalOptions=LayoutOptions.FillAndExpand;
            }
            else
            {
                ctrl._img.Source = null;
                ctrl._img.IsVisible = false;
                ctrl._lbl.HorizontalOptions=LayoutOptions.CenterAndExpand;
            }
        }

        #endregion

        #region BackImage

        public static readonly BindableProperty BackImageProperty = BindableProperty.Create(nameof(BackImage), typeof(ImageSource), typeof(IosFlatButton), null, propertyChanged: OnBackImageChanged);

        public ImageSource BackImage
        {
            get => (ImageSource)GetValue(BackImageProperty);
            set => SetValue(BackImageProperty, value);
        }

        private static void OnBackImageChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (Device.RuntimePlatform != Device.iOS)
                return;

            var ctrl = bindable as IosFlatButton;
            if (ctrl == null)
                return;

            var image = newvalue as ImageSource;
            if (image != null)
            {
                ctrl._imgArrow.Source = image;
                ctrl._imgArrow.IsVisible = true;
            }
            else
            {
                ctrl._imgArrow.Source = null;
                ctrl._imgArrow.IsVisible = false;
            }
        }

        #endregion

        #region Command

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(IosFlatButton));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        #endregion

        #region CommandParameter

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(IosFlatButton));

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
            var startColor = BackgroundColor;
            var endColor = startColor.MultiplyAlpha(0.7);
            
            await _viewContent.ColorTo(endColor, 100);
            Command?.Execute(CommandParameter);
            await _viewContent.ColorTo(startColor, 100);
        }

        #endregion
    }

    public sealed class AwesomeButton : StackLayout
    {
        #region Fields

        private readonly View _viewContent;
        private readonly Label _lblText;
        private readonly Label _lblAwesomeIcon;
        private readonly Image _imgDisclosure;

        #endregion

        public AwesomeButton()
        {
            Padding = DeviceService.OnPlatform(new Thickness(16, 8, 8, 8), new Thickness(16, 12, 8, 12));
            Orientation = StackOrientation.Horizontal;
            Spacing = 8;
            BackgroundColor = Color.Transparent;

            {
                _lblAwesomeIcon = new Label()
                {
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 0, 8, 0),
                    IsVisible = false,
                    LineBreakMode = LineBreakMode.NoWrap,
                    FontSize = DeviceService.OnPlatform(20, 20),
                    TextColor = Color.Black
                };
                Children.Add(_lblAwesomeIcon);

                _lblText = new Label()
                {
                    TextColor = Color.Black,
                    FontSize = DeviceService.OnPlatform(16, 15),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                Children.Add(_lblText);

                if (Device.RuntimePlatform == Device.iOS)
                {
                    _imgDisclosure = new Image()
                    {
                        HeightRequest = 18,
                        WidthRequest = 18,
                        VerticalOptions = LayoutOptions.Center,
                        IsVisible = false
                    };
                    Children.Add(_imgDisclosure);
                }

                _viewContent = this;
            }


            {
                var gesture = new TapGestureRecognizer();
                gesture.Tapped += GestureOnTapped;
                GestureRecognizers.Add(gesture);
            }

        }

        #region Bindable Properties

        #region Text

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(AwesomeButton), string.Empty, propertyChanged: OnTextChanged);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newvalue)
        {
            var ctrl = bindable as AwesomeButton;
            if (ctrl == null)
                return;

            ctrl._lblText.Text = newvalue?.ToString() ?? string.Empty;
        }

        #endregion

        #region TextColor

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(AwesomeButton), Color.Black, propertyChanged: OnTextColorChanged);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        private static void OnTextColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as AwesomeButton;
            if (ctrl == null)
                return;

            ctrl._lblText.TextColor = (Color)newvalue;
        }

        #endregion

        #region AwesomeIcon

        public static readonly BindableProperty AwesomeIconProperty = BindableProperty.Create(nameof(AwesomeIcon), typeof(string), typeof(AwesomeButton), string.Empty, propertyChanged: OnAwesomeIconChanged);

        public string AwesomeIcon
        {
            get => (string)GetValue(AwesomeIconProperty);
            set => SetValue(AwesomeIconProperty, value);
        }

        private static void OnAwesomeIconChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as AwesomeButton;
            if (ctrl == null)
                return;

            if (newvalue != null)
            {
                ctrl._lblAwesomeIcon.Text = newvalue.ToString();
                ctrl._lblAwesomeIcon.IsVisible = true;
            }
            else
            {
                ctrl._lblAwesomeIcon.Text = "";
                ctrl._lblAwesomeIcon.IsVisible = false;
            }
        }

        #endregion

        #region AwesomeIconFontSize

        public static readonly BindableProperty AwesomeIconFontSizeProperty = BindableProperty.Create(nameof(AwesomeIconFontSize), typeof(double), typeof(AwesomeButton), 20.0, propertyChanged: OnAwesomeIconFontSizeChanged);

        public double AwesomeIconFontSize
        {
            get => (double)GetValue(AwesomeIconFontSizeProperty);
            set => SetValue(AwesomeIconFontSizeProperty, value);
        }

        private static void OnAwesomeIconFontSizeChanged(BindableObject bo, object oldValue, object newValue)
        {
            var ctrl = bo as AwesomeButton;
            if (ctrl == null)
                return;

            ctrl._lblAwesomeIcon.FontSize = (double)newValue;
        }

        #endregion

        #region ImageColor

        public static readonly BindableProperty AwesomeIconColorProperty = BindableProperty.Create(nameof(AwesomeIconColor), typeof(Color), typeof(AwesomeButton), Color.Black, propertyChanged: OnAwesomeIconColorChanged);

        public Color AwesomeIconColor
        {
            get => (Color)GetValue(AwesomeIconColorProperty);
            set => SetValue(AwesomeIconColorProperty, value);
        }

        private static void OnAwesomeIconColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as AwesomeButton;
            if (ctrl == null)
                return;

            ctrl._lblAwesomeIcon.TextColor = (Color)newvalue;
        }

        #endregion

        #region ImageFontFamily

        public static readonly BindableProperty AwesomeIconFontFamilyProperty = BindableProperty.Create(nameof(AwesomeIconFontFamily), typeof(string), typeof(AwesomeButton), string.Empty, propertyChanged: OnImageFontFamilyChanged);

        public string AwesomeIconFontFamily
        {
            get => (string)GetValue(AwesomeIconFontFamilyProperty);
            set => SetValue(AwesomeIconFontFamilyProperty, value);
        }

        private static void OnImageFontFamilyChanged(BindableObject bo, object oldValue, object newValue)
        {
            var ctrl = bo as AwesomeButton;
            if (ctrl == null)
                return;

            ctrl._lblAwesomeIcon.FontFamily = newValue.ToString();
        }

        #endregion

        #region BackImage

        public static readonly BindableProperty BackImageProperty = BindableProperty.Create(nameof(BackImage), typeof(ImageSource), typeof(AwesomeButton), null, propertyChanged: OnBackImageChanged);

        public ImageSource BackImage
        {
            get => (ImageSource)GetValue(BackImageProperty);
            set => SetValue(BackImageProperty, value);
        }

        private static void OnBackImageChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (Device.RuntimePlatform != Device.iOS)
                return;

            var ctrl = bindable as AwesomeButton;
            if (ctrl == null)
                return;

            var image = newvalue as ImageSource;
            if (image != null)
            {
                ctrl._imgDisclosure.Source = image;
                ctrl._imgDisclosure.IsVisible = true;
            }
            else
            {
                ctrl._imgDisclosure.Source = null;
                ctrl._imgDisclosure.IsVisible = false;
            }
        }

        #endregion

        #region Command

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(AwesomeButton));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        #endregion

        #region CommandParameter

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(AwesomeButton));

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
            await _viewContent.ColorTo(Color.FromHex("#D7D7D7"), 100);
            Command?.Execute(CommandParameter);
            await _viewContent.ColorTo(Color.White, 100);
        }

        #endregion
    }
}