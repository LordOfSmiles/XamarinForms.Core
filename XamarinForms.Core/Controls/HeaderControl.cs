using Xamarin.Forms;
using XamarinForms.Core.Standard.Helpers;
using XamarinForms.Core.Standard.Services;

namespace XamarinForms.Core.Standard.Controls
{
    public sealed class HeaderControl : Label
    {
        public HeaderControl()
        {
            //BackgroundColor = DeviceHelper.OnPlatform(Color.FromHex("#EEEEEE"), Color.FromHex("#EDEDED"));
            Padding = DeviceHelper.OnPlatform(new Thickness(16, 6, 8, 6), new Thickness(16, 8, 8, 8));
            VerticalOptions = LayoutOptions.Center;
            FontSize = FontHelper.LabelSmall;
            FontAttributes = DeviceHelper.OnPlatform(FontAttributes.None, FontAttributes.Bold);
            TextColor = DeviceHelper.OnPlatform(Color.FromRgb(110, 110, 110), Color.Accent);
        }

        #region Bindable proeprties

        #region TextInput
        
        public static readonly BindableProperty TextInputProperty = BindableProperty.Create(nameof(TextInput), 
            typeof(string),
            typeof(HeaderControl),
            string.Empty, 
            propertyChanged: OnTextInputChanged);
        
        public string TextInput
        {
            get => (string)GetValue(TextInputProperty);
            set => SetValue(TextInputProperty, value);
        }
        
        private static void OnTextInputChanged(BindableObject bo, object oldValue, object newValue)
        {
            var ctrl = bo as HeaderControl;
            if (ctrl == null)
                return;
        
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    ctrl.Text = newValue?.ToString() ?? "";
                    break;
                case Device.iOS:
                    ctrl.Text = newValue?.ToString().ToUpper() ?? "";
                    break;
            }
        }
        
        #endregion
        
        #endregion
    }
}

