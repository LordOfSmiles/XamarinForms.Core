using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
    /// <summary>
    /// Class SegmentedControlView.
    /// </summary>
    public class SegmentedControlView : ContentView
    {
        /// <summary>
        /// The selected item property
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create("SelectedItem", typeof(int), typeof(SegmentedControlView), default(int));
        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>The selected item.</value>
        public int SelectedItem
        {
            get
            {
                return (int)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        /// <summary>
        /// The segments itens property
        /// </summary>
		public static readonly BindableProperty SegmentsItemsProperty = BindableProperty.Create("SegmentsItems", typeof(string), typeof(SegmentedControlView), default(string), BindingMode.TwoWay);
        /// <summary>
        /// Gets or sets the segments itens.
        /// </summary>
        /// <value>The segments itens.</value>
        public string SegmentsItems
        {
            get
            {
                return (string)GetValue(SegmentsItemsProperty);
            }
            set
            {
                SetValue(SegmentsItemsProperty, value);
            }
        }

        /// <summary>
        /// The tint color property
        /// </summary>
        public static readonly BindableProperty TintColorProperty = BindableProperty.Create("TintColor", typeof(Color), typeof(SegmentedControlView), Color.Blue);

        /// <summary>
        /// Gets or sets the color of the tint.
        /// </summary>
        /// <value>The color of the tint.</value>
        public Color TintColor
        {
            get
            {
                return (Color)GetValue(TintColorProperty);
            }
            set
            {
                SetValue(TintColorProperty, value);
            }
        }
    }
}
