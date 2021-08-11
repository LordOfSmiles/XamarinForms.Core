using System.Collections;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls.Pickers
{
    public partial class ContentPickerControl
    {
        public ContentPickerControl()
        {
            InitializeComponent();

            var gesture = new TapGestureRecognizer()
            {
                Command = OpenPickerCommand
            };
            GestureRecognizers.Add(gesture);

            UpdateControl();
        }

        #region Commands

        protected override Task OnOpenPicker()
        {
            picker.Focus();

            return Task.CompletedTask;
        }

        #endregion

        #region Bindable Properties

        #region PressedColor

        public static readonly BindableProperty PressedColorProperty = BindableProperty.Create(nameof(PressedColor),
            typeof(Color),
            typeof(ContentPickerControl),
            Color.Accent);

        public Color PressedColor
        {
            get => (Color)GetValue(PressedColorProperty);
            set => SetValue(PressedColorProperty, value);
        }
        
        #endregion
        
        #region SelectedIndex

        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex),
            typeof(int),
            typeof(ContentPickerControl),
            -1,
            propertyChanged: SelectedIndexChanged);

        public int SelectedIndex
        {
            get => (int) GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        private static void SelectedIndexChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (ContentPickerControl) bindable;

            ctrl.UpdateControl();
        }

        #endregion

        #region SelectedItem

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem),
            typeof(object),
            typeof(ContentPickerControl),
            propertyChanged: SelectedItemChanged);

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        private static void SelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (ContentPickerControl) bindable;

            ctrl.UpdateControl();
        }

        #endregion

        #region ItemsSource

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource),
            typeof(IList),
            typeof(ContentPickerControl),
            propertyChanged: OnItemsSourceChanged);

        public IList ItemsSource
        {
            get => (IList) GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (ContentPickerControl) bindable;
            
            ctrl.picker.ItemDisplayBinding = null;
            ctrl.picker.ItemDisplayBinding = ctrl.ItemDisplayBinding;
        }

        #endregion

        #region ItemDisplayBinding

        public static readonly BindableProperty ItemDisplayBindingProperty = BindableProperty.Create(nameof(ItemDisplayBinding),
            typeof(BindingBase),
            typeof(ContentPickerControl),
            new Binding("Name"),
            propertyChanged: OnItemDisplayBindingChanged);

        public BindingBase ItemDisplayBinding
        {
            get => (BindingBase) GetValue(ItemDisplayBindingProperty);
            set => SetValue(ItemDisplayBindingProperty, value);
        }

        private static void OnItemDisplayBindingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (ContentPickerControl) bindable;

            ctrl.picker.ItemDisplayBinding = (BindingBase) newValue;
        }

        #endregion

        #endregion

        protected override void UpdateControl()
        {
            cnvContent.IsVisible = SelectedIndex != -1;
            cnvPlaceholder.IsVisible = SelectedIndex == -1;
        }
    }
}