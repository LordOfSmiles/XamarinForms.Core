using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
    public partial class MaterialFilledDatePicker
    {
        public MaterialFilledDatePicker()
        {
            InitializeComponent();
        }

        #region Bindable Properties
        
        #region SelectedDate
        
        public static readonly BindableProperty SelectedDateProperty = BindableProperty.Create(
            nameof(SelectedDate),
            typeof(DateTime?),
            typeof(MaterialFilledDatePicker),
            null,
            BindingMode.TwoWay,
            propertyChanged: OnSelectedDateChanged);

        public DateTime? SelectedDate
        {
            get => (DateTime?) GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        private static void OnSelectedDateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as MaterialFilledDatePicker;
            if (ctrl == null)
                return;

            var date = (DateTime?) newValue;

            ctrl.stkContent.IsVisible = date.HasValue;
            ctrl.lblBigPlaceholder.IsVisible = !date.HasValue;

            ctrl.lblContent.Text = date.HasValue
                ? date.Value.ToString("dd.MM.yyyy")
                : "";
        }
        
        #endregion
        
        #region Placeholder

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(MaterialFilledDatePicker),
            propertyChanged: OnPlaceholderChanged);

        public string Placeholder
        {
            get => (string) GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        private static void OnPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as MaterialFilledDatePicker;
            if (ctrl == null)
                return;

            var placeholder = newValue.ToString() ?? "";

            ctrl.lblBigPlaceholder.Text = placeholder;
            ctrl.lblSmallPlaceholder.Text = placeholder;
        }

        #endregion
        
        #region AccentColor

        public static readonly BindableProperty AccentColorProperty = BindableProperty.Create(nameof(AccentColor),
            typeof(Color),
            typeof(MaterialFilledDatePicker),
            Color.FromHex("#14000000"),
            propertyChanged: OnAccentColorChanged);

        public Color AccentColor
        {
            get => (Color) GetValue(AccentColorProperty);
            set => SetValue(AccentColorProperty, value);
        }

        private static void OnAccentColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as MaterialFilledDatePicker;
            if (ctrl == null)
                return;

            var accent = (Color) newValue;

            ctrl.lblSmallPlaceholder.TextColor = accent;
            ctrl.brdSeparator.Color = accent;
        }
        
        #endregion
        
        #region Command

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command),
            typeof(ICommand),
            typeof(MaterialFilledDatePicker),
            null);

        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        
        #endregion

        #endregion

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            Command?.Execute(null);
        }
    }
}