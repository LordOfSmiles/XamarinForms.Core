using Xamarin.Forms;
using XamarinForms.Core.Standard.Services;

namespace XamarinForms.Core.Controls
{
    public sealed class SwitchSettingsContainer : Grid
    {
        #region Fields

        private readonly Label _lbl;
        private readonly Switch _switcher;
        private readonly Label _lblDetail;

        #endregion

        public SwitchSettingsContainer()
        {
            BackgroundColor = Color.Transparent;
            RowSpacing = 0;

            ColumnDefinitions.Add(new ColumnDefinition());
            ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = GridLength.Auto
            });
            
            RowDefinitions.Add(new RowDefinition(){ Height = GridLength.Auto});
            RowDefinitions.Add(new RowDefinition(){ Height = GridLength.Auto});

            Padding = DeviceHelper.OnPlatform(new Thickness(16, 4), new Thickness(16, 8));

            _lbl = new Label()
            {
                LineBreakMode = LineBreakMode.TailTruncation,
                TextColor = Color.Black,
                VerticalTextAlignment = TextAlignment.Center
            };
            Children.Add(_lbl);

            _switcher = new Switch()
            {
                VerticalOptions = LayoutOptions.Center
            };
            _switcher.Toggled += (sender, args) => IsToggled = args.Value;
            SetColumn(_switcher, 1);
            Children.Add(_switcher);

            _lblDetail = new Label()
            {
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                IsVisible = false,
                TextColor = Color.FromHex("#6E6E6E"),
                LineBreakMode = LineBreakMode.WordWrap,
                Margin = new Thickness(0,4,0,0)
            };
            SetRow(_lblDetail, 1);
            SetColumn(_lblDetail,0);
            SetColumnSpan(_lblDetail,2);
            Children.Add(_lblDetail);
        }

        #region Bindable properties

        #region Text

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
            typeof(string),
            typeof(SwitchSettingsContainer),
            string.Empty,
            propertyChanged: OnTextChanged);

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as SwitchSettingsContainer;
            if (ctrl == null)
                return;

            ctrl._lbl.Text = newValue?.ToString() ?? string.Empty;
        }

        #endregion

        #region OnColor

        public static readonly BindableProperty OnColorProperty = BindableProperty.Create(nameof(OnColor),
            typeof(Color),
            typeof(SwitchSettingsContainer),
            Color.Default,
            propertyChanged: OnColorChanged);

        public Color OnColor
        {
            get => (Color) GetValue(OnColorProperty);
            set => SetValue(OnColorProperty, value);
        }

        private static void OnColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as SwitchSettingsContainer;
            if (ctrl == null)
                return;

            ctrl._switcher.OnColor = (Color) newValue;
        }

        #endregion

        #region IsToggled

        public static readonly BindableProperty IsToggledProperty = BindableProperty.Create(nameof(IsToggled),
            typeof(bool),
            typeof(SwitchSettingsContainer),
            false,
            BindingMode.TwoWay,
            propertyChanged: OnIsToggledChanged);

        public bool IsToggled
        {
            get => (bool) GetValue(IsToggledProperty);
            set => SetValue(IsToggledProperty, value);
        }

        private static void OnIsToggledChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as SwitchSettingsContainer;
            if (ctrl == null)
                return;

            ctrl._switcher.IsToggled = (bool) newValue;
        }

        #endregion
        
        #region Detail

        public static readonly BindableProperty DetailProperty = BindableProperty.Create(nameof(Detail),
            typeof(string),
            typeof(SwitchSettingsContainer),
            null,
            propertyChanged: OnDetailChanged);

        public string Detail
        {
            get => (string) GetValue(DetailProperty);
            set => SetValue(DetailProperty, value);
        }

        private static void OnDetailChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as SwitchSettingsContainer;
            if (ctrl == null)
                return;

            var stringInput = newValue?.ToString() ?? string.Empty;
            if (!string.IsNullOrEmpty(stringInput))
            {
                ctrl._lblDetail.IsVisible = true;
                ctrl._lblDetail.Text = stringInput;
            }
            else
            {
                ctrl._lblDetail.IsVisible = false;
            }
        }

        #endregion

        #endregion
    }
}