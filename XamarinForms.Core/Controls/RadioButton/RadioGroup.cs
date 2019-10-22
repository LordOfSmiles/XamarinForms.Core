using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Controls.RadioButton
{
    public sealed class RadioGroup : StackLayout
    {
        public RadioGroup()
        {
            Items = new ObservableCollection<RadioButtonView>();
            Orientation = GroupOrientation;
        }

        #region Bindable Properties

        #region Orienatation

        public static readonly BindableProperty GroupOrientationProperty = BindableProperty.Create(nameof(GroupOrientation), typeof(StackOrientation), typeof(RadioGroup), StackOrientation.Horizontal, propertyChanged: OnGroupOrientationChanged);

        public StackOrientation GroupOrientation
        {
            get => (StackOrientation)GetValue(GroupOrientationProperty);
            set => SetValue(GroupOrientationProperty, value);
        }

        private static void OnGroupOrientationChanged(BindableObject bo, object oldValue, object newValue)
        {
            var ctrl = bo as RadioGroup;
            if (ctrl == null)
                return;

            ctrl.Orientation = (StackOrientation)newValue;
        }

        #endregion

        #region ItemsSource

        public static BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(RadioGroup), default(IList), propertyChanged: OnItemsSourceChanged);

        public IList ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as RadioGroup;
            if (ctrl == null)
                return;

            foreach (var item in ctrl.Items)
            {
                    item.CheckedChanged -= ctrl.OnCheckedChanged;
            }

            ctrl.Children.Clear();

            var radIndex = 0;

            foreach (var item in ctrl.ItemsSource)
            {
                var button = new RadioButtonView
                {
                    Text = item.ToString(),
                    Id = radIndex++,
                    TextColor = ctrl.TextColor,
                    FontSize = Device.GetNamedSize(NamedSize.Small, ctrl),
                    FontName = ctrl.FontName
                };

                button.CheckedChanged += ctrl.OnCheckedChanged;

                ctrl.Items.Add(button);
                ctrl.Children.Add(button);
            }

            if (ctrl.Children.Any())
            {
                ctrl.Children.OfType<RadioButtonView>().First().Checked = true;
            }
        }

        #endregion

        #region SelectedIndex

        public static BindableProperty SelectedIndexProperty =
            BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(RadioGroup), default(int), BindingMode.TwoWay, propertyChanged: OnSelectedIndexChanged);

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        private static void OnSelectedIndexChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as RadioGroup;
            if (ctrl == null)
                return;

            var value = (int)newvalue;

            if (value == -1)
                return;

            foreach (var button in ctrl.Items.Where(button => button.Id == ctrl.SelectedIndex))
            {
                button.Checked = true;
            }
        }

        #endregion

        #region TextColor

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(RadioGroup), Color.Black);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        #endregion

        #region FontSize

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(RadioGroup), -1.0);

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }


        #endregion

        #region FontName

        public static readonly BindableProperty FontNameProperty =
            BindableProperty.Create("FontName", typeof(string), typeof(RadioGroup), string.Empty);


        public string FontName
        {
            get => (string)GetValue(FontNameProperty);
            set => SetValue(FontNameProperty, value);
        }

        #endregion

        #endregion

        public ObservableCollection<RadioButtonView> Items;

        #region Events

        public event EventHandler<int> CheckedChanged;

        #endregion

        #region Handlers

        private void OnCheckedChanged(object sender, bool e)
        {
            if (e == false)
            {
                return;
            }

            var selectedItem = sender as RadioButtonView;

            if (selectedItem == null)
            {
                return;
            }

            foreach (var item in Items)
            {
                if (!selectedItem.Id.Equals(item.Id))
                {
                    item.Checked = false;
                }
                else
                {
                    SelectedIndex = selectedItem.Id;
                    if (CheckedChanged != null)
                    {
                        CheckedChanged.Invoke(sender, item.Id);
                    }
                }
            }
        }

        #endregion
    }

}
