using Xamarin.Forms;

namespace XamarinForms.Core.Controls.Entry
{
    public class FloatingEntry : Grid
    {
        private readonly Label _label;
        private readonly Xamarin.Forms.Entry _entry;

        public delegate void ChangedEventHandler(object sender, TextChangedEventArgs e);
        public event ChangedEventHandler EntryTextChanged;

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create<FloatingEntry, string>(p => p.Placeholder, string.Empty, propertyChanged:
                (bindable, value, newValue) =>
                {
                    var b = bindable as FloatingEntry;
                    b._label.Text = newValue;
                    b._entry.Placeholder = newValue;
                });

        public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create<FloatingEntry, Color>(p => p.PlaceholderColor, Color.Black, propertyChanged:
                (bindable, value, newValue) =>
                {
                    var b = bindable as FloatingEntry;
                    b._label.TextColor = newValue;
                });

        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create<FloatingEntry, bool>(p => p.IsPassword, false, propertyChanged:
                (bindable, value, newValue) =>
                {
                    var b = bindable as FloatingEntry;
                    b._entry.IsPassword = newValue;
                });

        public static readonly BindableProperty EntryTextProperty =
            BindableProperty.Create<FloatingEntry, string>(p => p.EntryText, string.Empty, propertyChanged:
                (bindable, value, newValue) =>
                {
                    var b = bindable as FloatingEntry;
                    b._entry.Text = newValue;

                    if (newValue != null)
                        b._label.IsVisible = newValue.Length > 0;
                });



        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }
        public Color PlaceholderColor
        {
            get { return (Color)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }

        public bool IsPassword
        {
            get { return (bool)GetValue(IsPasswordProperty); }
            set { SetValue(IsPasswordProperty, value); }
        }

        public string EntryText
        {
            get { return (string)GetValue(EntryTextProperty); }
            set { SetValue(EntryTextProperty, value); }
        }


        public FloatingEntry()
        {
            Padding = 0;
            RowSpacing = 0;

            var height = 40;
            var topLineFondSize = 14;

            if (Device.RuntimePlatform == Device.iOS)
            {
                Padding = new Thickness(10, 4, 10, 0);

                RowSpacing = 2;
                height = 22;
                topLineFondSize = 12;
            }


            RowDefinitions = new RowDefinitionCollection()
            {
                new RowDefinition() { Height = GridLength.Auto},
                new RowDefinition() { Height = GridLength.Auto },
            };

            _label = new Label()
            {
                TextColor = Color.Gray,
                IsVisible = false,
                FontSize = topLineFondSize
            };

            var s = new ContentView();
            _entry = new Xamarin.Forms.Entry()
            {
                BackgroundColor = Color.Transparent,
                FontSize = 16,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = height
            };

            s.Content = _entry;
            Grid.SetRow(s, 1);

            Children.Add(_label);
            Children.Add(s);


            _entry.TextChanged += (sender, args) =>
            {
                EntryText = args.NewTextValue;
                if (EntryTextChanged != null)
                    EntryTextChanged(this, args);
            };
        }
    }
}
