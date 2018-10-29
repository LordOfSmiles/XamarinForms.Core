using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Controls
{
    public class SegmentedControlView : View, IViewContainer<SegmentedControlOption>
    {
        public event EventHandler<SegmentSelectEventArgs> OnSegmentSelected;
        public IList<SegmentedControlOption> Children { get; set; }

        public static readonly BindableProperty TintColorProperty = BindableProperty.Create("TintColor", typeof(Color), typeof(SegmentedControlView), Color.Blue);

        public Color TintColor
        {
            get => (Color)GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }

        public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create("SelectedTextColor", typeof(Color), typeof(SegmentedControlView), Color.White);

        public Color SelectedTextColor
        {
            get => (Color)GetValue(SelectedTextColorProperty);
            set => SetValue(SelectedTextColorProperty, value);
        }

        public static readonly BindableProperty DisabledColorProperty = BindableProperty.Create("DisabledColor", typeof(Color), typeof(SegmentedControlView), Color.Gray);

        public Color DisabledColor
        {
            get => (Color)GetValue(DisabledColorProperty);
            set => SetValue(DisabledColorProperty, value);
        }


        public static readonly BindableProperty SelectedSegmentProperty = BindableProperty.Create("SelectedSegment", typeof(int), typeof(SegmentedControlView), 0);

        public int SelectedSegment
        {
            get => (int)GetValue(SelectedSegmentProperty);
            set => SetValue(SelectedSegmentProperty, value);
        }

        public SegmentedControlView()
        {
            Children = new List<SegmentedControlOption>();
        }


        [EditorBrowsable(EditorBrowsableState.Never)]
        public void RaiseSelectionChanged()
        {
            OnSegmentSelected?.Invoke(this, new SegmentSelectEventArgs { NewValue = this.SelectedSegment });
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            foreach (var segment in Children)
            {
                segment.BindingContext = BindingContext;
            }
        }
    }

    public class SegmentedControlOption : View
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(SegmentedControlOption), string.Empty);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }

    public class SegmentSelectEventArgs : EventArgs
    {
        public int NewValue { get; set; }
    }
}
