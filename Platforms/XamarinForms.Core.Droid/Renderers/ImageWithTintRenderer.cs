using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using AndroidX.Core.Graphics.Drawable;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinForms.Core.Controls;

namespace XamarinForms.Core.Droid.Renderers
{
    public sealed class ImageWithTintRenderer: ImageRenderer
    {
        private ImageWithTint _materialIcon;

        public ImageWithTintRenderer(Context context) 
            : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                return;
            }

            if (e?.NewElement == null)
            {
                return;
            }

            _materialIcon = Element as ImageWithTint;
            UpdateDrawable();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null)
            {
                return;
            }

            if (e?.PropertyName == nameof(ImageWithTint.TintColor) || e?.PropertyName == nameof(Image.Source))
            {
                UpdateDrawable();
            }
        }

        private void UpdateDrawable()
        {
            using var drawable = GetDrawableCopy(Control.Drawable);
            ChangeTintColor(drawable);
        }

        private void ChangeTintColor(Drawable drawable)
        {
            if (_materialIcon.TintColor.IsDefault || drawable == null)
            {
                return;
            }

            var tintColor = _materialIcon.TintColor.ToAndroid();
            DrawableCompat.SetTint(drawable, tintColor);
            Control.SetImageDrawable(drawable);
        }

        private static Drawable GetDrawableCopy(Drawable drawable)
        {
            return drawable?.GetConstantState()?.NewDrawable().Mutate();
        }
    }
}