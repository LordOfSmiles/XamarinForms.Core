using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Controls
{
    public sealed class SegmentedControlView : View
    {
        public event EventHandler<int> OnSegmentSelected;
        
        public SegmentedControlView()
        {
            
        }

        #region Bindable properties
        
        #region Children

        public static readonly BindableProperty ChildrenProperty = BindableProperty.Create(nameof(Children),
            typeof(IList<string>),
            typeof(SegmentedControlView),
            default(IList<string>));
        
        public IList<string> Children
        {
            get => (IList<string>) GetValue(ChildrenProperty);
            set => SetValue(ChildrenProperty, value);
        }

        #endregion

        #region TintColor

        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor),
            typeof(Color),
            typeof(SegmentedControlView), 
            Color.Blue);

        public Color TintColor
        {
            get => (Color)GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }

        #endregion
        
        #region SelectedTextColor
        
        public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create(nameof(SelectedTextColor),
            typeof(Color),
            typeof(SegmentedControlView),
            Color.White);

        public Color SelectedTextColor
        {
            get => (Color)GetValue(SelectedTextColorProperty);
            set => SetValue(SelectedTextColorProperty, value);
        }
        
        #endregion
        
        #region DisabledColor
        
        public static readonly BindableProperty DisabledColorProperty = BindableProperty.Create(nameof(DisabledColor), typeof(Color), typeof(SegmentedControlView), Color.Gray);

        public Color DisabledColor
        {
            get => (Color)GetValue(DisabledColorProperty);
            set => SetValue(DisabledColorProperty, value);
        }
        
        #endregion
        
        #region SelectedSegment
        
        public static readonly BindableProperty SelectedSegmentProperty = BindableProperty.Create(nameof(SelectedSegment), 
            typeof(int),
            typeof(SegmentedControlView),
            0);

        public int SelectedSegment
        {
            get => (int)GetValue(SelectedSegmentProperty);
            set => SetValue(SelectedSegmentProperty, value);
        }
        
        #endregion

        #endregion
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void RaiseSelectionChanged()
        {
            OnSegmentSelected?.Invoke(this, SelectedSegment);
        }
    }
}
