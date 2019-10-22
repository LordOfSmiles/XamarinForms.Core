using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinForms.Core.Standard.Extensions;
using XamarinForms.Core.Standard.Services;

namespace XamarinForms.Core.Standard.Controls
{
    public sealed class FlatButtonWithFontImage : Grid
    {
        #region Fields
        
        private readonly Label _lbl;
        private readonly Label _lblGlyph;

        #endregion

        public FlatButtonWithFontImage()
        {
            ColumnDefinitions.Add(new ColumnDefinition() {Width = GridLength.Auto});
            ColumnDefinitions.Add(new ColumnDefinition());

            Padding = DeviceService.OnPlatform(new Thickness(16, 8, 8, 8), new Thickness(16, 12, 8, 12));
            ColumnSpacing = DeviceService.OnPlatform(16, 32);
            BackgroundColor = Color.White;

            _lblGlyph = new Label()
            {
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                LineBreakMode = LineBreakMode.NoWrap,
                TextColor = Color.Black
            };
            SetColumn(_lblGlyph, 0);
            Children.Add(_lblGlyph);

            _lbl = new Label()
            {
                TextColor = Color.Black,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Start
            };
            SetColumn(_lbl, 1);
            Children.Add(_lbl);

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
            var ctrl = bindable as FlatButtonWithFontImage;
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
            var ctrl = bindable as FlatButtonWithFontImage;
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
            var ctrl = bindable as FlatButtonWithFontImage;
            if (ctrl == null)
                return;

            ctrl._lbl.FontAttributes = (FontAttributes) newvalue;
        }

        
        #endregion

        #region Glyph

        public static readonly BindableProperty GlyphProperty = BindableProperty.Create(nameof(Glyph), typeof(string),
            typeof(FlatButtonWithFontImage),
            string.Empty,
            propertyChanged: OnGlyphChanged);

        public string Glyph
        {
            get => (string) GetValue(GlyphProperty);
            set => SetValue(GlyphProperty, value);
        }

        private static void OnGlyphChanged(BindableObject bo, object oldValue, object newValue)
        {
            var ctrl = bo as FlatButtonWithFontImage;
            if (ctrl == null)
                return;

            ctrl._lblGlyph.Text = newValue.ToString();
        }
        
        #endregion
        
        #region GlyphSize

        public static readonly BindableProperty GlyphSizeProperty = BindableProperty.Create(nameof(GlyphSize), 
            typeof(double),
            typeof(FlatButtonWithFontImage),
            14.0,
            propertyChanged: OnGlyphSizeChanged);

        public double GlyphSize
        {
            get => (double) GetValue(GlyphSizeProperty);
            set => SetValue(GlyphSizeProperty, value);
        }

        private static void OnGlyphSizeChanged(BindableObject bo, object oldValue, object newValue)
        {
            var ctrl = bo as FlatButtonWithFontImage;
            if (ctrl == null)
                return;

            ctrl._lblGlyph.FontSize = (double) newValue;
        }
        
        #endregion
        
        #region GlyphColor

        public static readonly BindableProperty GlyphColorProperty = BindableProperty.Create(nameof(GlyphColor),
            typeof(Color),
            typeof(FlatButtonWithImage),
            Color.Black,
            propertyChanged: OnGlyphColorChanged);

        public Color GlyphColor
        {
            get => (Color)GetValue(GlyphColorProperty);
            set => SetValue(GlyphColorProperty, value);
        }

        private static void OnGlyphColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as FlatButtonWithFontImage;
            if (ctrl == null)
                return;

            ctrl._lblGlyph.TextColor = (Color) newvalue;
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