using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Controls
{
    public sealed class FlatButton:ContentView
    {
        #region Fields

        private readonly IosFlatButton _btnIos;
        private readonly AndroidFlatButton _btnDroid;
        
        #endregion
        
        public FlatButton()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    _btnIos=new IosFlatButton();
                    Content = _btnIos;
                    break;
                case Device.Android:
                    _btnDroid=new AndroidFlatButton();
                    Content = _btnDroid;
                    break;
            }
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

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    ctrl._btnIos.Text = newvalue?.ToString() ?? string.Empty;
                    break;
                case Device.Android:
                    ctrl._btnDroid.Text = newvalue?.ToString() ?? string.Empty;
                    break;
            }

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

            ctrl._btnIos.TextColor = (Color)newvalue;
        }

        #endregion

        #region Image

        public static readonly BindableProperty ImageProperty = BindableProperty.Create(nameof(Image), typeof(ImageSource), typeof(FlatButton), null, propertyChanged: OnImageChanged);

        public ImageSource Image
        {
            get => (ImageSource)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        private static void OnImageChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as FlatButton;
            if (ctrl == null)
                return;

            ctrl._btnIos.Image=newvalue as ImageSource;
        }

        #endregion
        
        #region BackImage
        
        public static readonly BindableProperty BackImageProperty = BindableProperty.Create(nameof(BackImage), typeof(ImageSource), typeof(FlatButton), null, propertyChanged: OnBackImageChanged);

        public ImageSource BackImage
        {
            get => (ImageSource)GetValue(BackImageProperty);
            set => SetValue(BackImageProperty, value);
        }

        private static void OnBackImageChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as FlatButton;
            if (ctrl == null)
                return;

            ctrl._btnIos.BackImage=newvalue as ImageSource;
        }
        
        #endregion

        #region Command

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(FlatButton), propertyChanged:OnCommandChanged);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        private static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as FlatButton;
            if (ctrl == null)
                return;


            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    ctrl._btnIos.Command = newValue as ICommand;
                    break;
                case Device.Android:
                    ctrl._btnDroid.Command = newValue as ICommand;
                    break;
            }
        }

        #endregion

        #region CommandParameter

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(FlatButton),propertyChanged:OnCommandParameterChanged);

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        private static void OnCommandParameterChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as FlatButton;
            if (ctrl == null)
                return;
            
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    ctrl._btnIos.CommandParameter = newValue;
                    break;
                case Device.Android:
                    ctrl._btnDroid.CommandParameter = newValue;
                    break;
            }
        }

        #endregion

        #endregion
    }
}