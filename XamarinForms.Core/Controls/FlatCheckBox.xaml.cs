using System;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.Forms;
using TouchEffect = Xamarin.CommunityToolkit.Effects.TouchEffect;

namespace XamarinForms.Core.Controls
{
    public partial class FlatCheckBox
    {
        public FlatCheckBox()
        {
            InitializeComponent();

            img.IsVisible = Icon != null;

            UpdateColors();
            TouchEffect.SetCommand(this, TapCommand);
        }

        #region Commands

        private ICommand TapCommand => new Command(OnTap);

        private void OnTap()
        {
            IsChecked = !IsChecked;
        }

        #endregion

        #region Bindable Properties

        #region Icon

        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon),
            typeof(ImageSource),
            typeof(FlatCheckBox),
            null,
            propertyChanged: OnIconChanged);

        public ImageSource Icon
        {
            get => (ImageSource) GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        private static void OnIconChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (FlatCheckBox) bindable;

            var imageSource = (ImageSource) newValue;

            ctrl.img.IsVisible = imageSource != null;
            ctrl.img.Source = imageSource;
        }

        #endregion

        #region Text

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
            typeof(string),
            typeof(FlatCheckBox));

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        #endregion

        #region IsChecked

        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked),
            typeof(bool),
            typeof(FlatCheckBox),
            false,
            BindingMode.TwoWay,
            propertyChanged: OnIsCheckedChanged);

        public bool IsChecked
        {
            get => (bool) GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        private static void OnIsCheckedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (FlatCheckBox) bindable;
            ctrl.UpdateColors();

            ctrl.CheckedEvent?.Invoke(ctrl, EventArgs.Empty);
        }

        #endregion

        #region TextTransform

        public static readonly BindableProperty TextTransformProperty = BindableProperty.Create(nameof(TextTransform),
            typeof(TextTransform),
            typeof(FlatCheckBox),
            TextTransform.None);

        public TextTransform TextTransform
        {
            get => (TextTransform) GetValue(TextTransformProperty);
            set => SetValue(TextTransformProperty, value);
        }

        #endregion

        #region RippleColor

        public static readonly BindableProperty RippleColorProperty = BindableProperty.Create(nameof(RippleColor),
            typeof(Color),
            typeof(FlatCheckBox),
            Color.LightGray);

        public Color RippleColor
        {
            get => (Color) GetValue(RippleColorProperty);
            set => SetValue(RippleColorProperty, value);
        }

        #endregion

        #region SelectedColor

        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor),
            typeof(Color),
            typeof(FlatCheckBox),
            Color.Red,
            propertyChanged: OnSelectedColorChanged);

        public Color SelectedColor
        {
            get => (Color) GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        private static void OnSelectedColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (FlatCheckBox) bindable;
            ctrl?.UpdateColors();
        }

        #endregion

        #region SelectedTextColor

        public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create(nameof(SelectedTextColor),
            typeof(Color),
            typeof(FlatCheckBox),
            Color.White,
            propertyChanged: OnSelectedTextColorChanged);

        public Color SelectedTextColor
        {
            get => (Color) GetValue(SelectedTextColorProperty);
            set => SetValue(SelectedTextColorProperty, value);
        }

        private static void OnSelectedTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (FlatCheckBox) bindable;
            ctrl?.UpdateColors();
        }

        #endregion

        #region UnselectedColor

        public static readonly BindableProperty UnselectedColorProperty = BindableProperty.Create(nameof(UnselectedColor),
            typeof(Color),
            typeof(FlatCheckBox),
            Color.Default,
            propertyChanged: OnUnselectedColorChanged);

        public Color UnselectedColor
        {
            get => (Color) GetValue(UnselectedColorProperty);
            set => SetValue(UnselectedColorProperty, value);
        }

        private static void OnUnselectedColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (FlatCheckBox) bindable;
            ctrl?.UpdateColors();
        }

        #endregion

        #region UnselectedTextColor

        public static readonly BindableProperty UnselectedTextColorProperty = BindableProperty.Create(nameof(UnselectedTextColor),
            typeof(Color),
            typeof(FlatCheckBox),
            Color.Black,
            propertyChanged: OnUnselectedTextColorChanged);

        public Color UnselectedTextColor
        {
            get => (Color) GetValue(UnselectedTextColorProperty);
            set => SetValue(UnselectedTextColorProperty, value);
        }

        private static void OnUnselectedTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (FlatCheckBox) bindable;
            ctrl?.UpdateColors();
        }

        #endregion

        #endregion

        #region Private Methods

        private void UpdateColors()
        {
            if (ctrlRoot == null || img == null || lbl == null)
                return;

            if (IsChecked)
            {
                ctrlRoot.BackgroundColor = SelectedColor;
                IconTintColorEffect.SetTintColor(img, SelectedTextColor);
                lbl.TextColor = SelectedTextColor;
            }
            else
            {
                ctrlRoot.BackgroundColor = UnselectedColor;
                IconTintColorEffect.SetTintColor(img, UnselectedTextColor);
                lbl.TextColor = UnselectedTextColor;
            }
        }

        #endregion
        
        #region Events

        public event EventHandler CheckedEvent;

        #endregion
    }
}