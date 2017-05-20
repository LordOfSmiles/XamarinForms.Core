using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls.RadioButton
{
    public class BindableRadioGroup : StackLayout
    {


        public BindableRadioGroup()
        {
            Items = new ObservableCollection<CustomRadioButton>();
            Orientation = GroupOrientation;
        }

        #region Bindable Properties

        #region Orienatation

        public static readonly BindableProperty GroupOrientationProperty = BindableProperty.Create("GroupOrientation", typeof(StackOrientation), typeof(BindableRadioGroup), StackOrientation.Horizontal, propertyChanged: OnGroupOrientationChanged);

        public StackOrientation GroupOrientation
        {
            get { return (StackOrientation)GetValue(GroupOrientationProperty); }
            set { SetValue(GroupOrientationProperty, value); }
        }

        private static void OnGroupOrientationChanged(BindableObject bo, object oldValue, object newValue)
        {
            var ctrl = bo as BindableRadioGroup;
            if (ctrl == null)
                return;

            ctrl.Orientation = (StackOrientation)newValue;
        }

        #endregion

        #region ItemsSource

        public static BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource", typeof(IList), typeof(BindableRadioGroup), default(IList), propertyChanged: OnItemsSourceChanged);

        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as BindableRadioGroup;
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
                var button = new CustomRadioButton
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
                ctrl.Children.OfType<CustomRadioButton>().First().Checked = true;
            }
        }

        #endregion

        #region SelectedIndex

        public static BindableProperty SelectedIndexProperty =
            BindableProperty.Create("SelectedIndex", typeof(int), typeof(BindableRadioGroup), default(int), BindingMode.TwoWay, propertyChanged: OnSelectedIndexChanged);

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        private static void OnSelectedIndexChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ctrl = bindable as BindableRadioGroup;
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

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create("TextColor", typeof(Color), typeof(BindableRadioGroup), Color.Black);

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        #endregion

        #region FontSize

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create("FontSize", typeof(double), typeof(BindableRadioGroup), -1.0);

        public double FontSize
        {
            get
            {
                return (double)GetValue(FontSizeProperty);
            }
            set
            {
                SetValue(FontSizeProperty, value);
            }
        }


        #endregion

        #region FontName

        public static readonly BindableProperty FontNameProperty =
            BindableProperty.Create("FontName", typeof(string), typeof(BindableRadioGroup), string.Empty);


        public string FontName
        {
            get
            {
                return (string)GetValue(FontNameProperty);
            }
            set
            {
                SetValue(FontNameProperty, value);
            }
        }

        #endregion

        #endregion

        public ObservableCollection<CustomRadioButton> Items;

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


            var selectedItem = sender as CustomRadioButton;

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
