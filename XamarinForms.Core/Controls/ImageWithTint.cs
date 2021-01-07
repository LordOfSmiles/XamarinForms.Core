using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
    public sealed class ImageWithTint:Image
    {
        /// <summary>
        /// Backing field for the bindable property <see cref="TintColor"/>.
        /// </summary>
        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), 
            typeof(Color),
            typeof(ImageWithTint),
            Color.Default);

        /// <summary>
        /// Gets or sets the tint color of the image icon.
        /// </summary>
        public Color TintColor
        {
            get => (Color)GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }
    }
}