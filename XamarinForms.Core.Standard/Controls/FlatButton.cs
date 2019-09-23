using System;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinForms.Core.Standard.Extensions;
using XamarinForms.Core.Standard.Services;

namespace XamarinForms.Core.Standard.Controls
{
    public sealed class FlatButton : ContentView
    {
        #region Fields
        
        private readonly Label _lbl;

        #endregion

        public FlatButton()
        {
            Padding = DeviceService.OnPlatform(new Thickness(8), new Thickness(8, 12));
            BackgroundColor = Color.White;

            _lbl = new Label()
            {
                TextColor = Color.Black,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            Content = _lbl;

            var gesture = new TapGestureRecognizer();
            gesture.Tapped += GestureOnTapped;
            GestureRecognizers.Add(gesture);
        }

        #region Bindable Properties

        #region Text

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(FlatButton), string.Empty, propertyChanged: OnTextChanded);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void OnTextChanded(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as FlatButton;
            if (ctrl == null)
                return;

            ctrl._lbl.Text = newvalue?.ToString() ?? string.Empty;
        }

        #endregion

        #region TextColor

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(FlatButton), Color.Black, propertyChanged: OnTextColorChanged);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        private static void OnTextColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as FlatButton;
            if (ctrl == null)
                return;

            ctrl._lbl.TextColor = (Color)newvalue;
        }

        #endregion
        
        #region FontAttributes

        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes),
            typeof(FontAttributes),
            typeof(FlatButton),
            FontAttributes.None,
            propertyChanged: OnFontAttributesChanged);
        
        public FontAttributes FontAttributes
        {
            get => (FontAttributes) GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }
        
        private static void OnFontAttributesChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as FlatButton;
            if (ctrl == null)
                return;

            ctrl._lbl.FontAttributes = (FontAttributes) newvalue;
        }

        
        #endregion

        #region Command

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(FlatButton));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        #endregion

        #region CommandParameter

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(FlatButton));

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
            
            await this.ColorTo(endColor, 100);
            Command?.Execute(CommandParameter);
            await this.ColorTo(startColor, 100);
        }

        #endregion
    }
}