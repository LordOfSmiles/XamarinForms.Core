using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinForms.Core.Controls;

namespace XamarinForms.Core.iOS.Renderers
{
    public sealed class ImageWithTintRenderer: ImageRenderer
    {
        private ImageWithTint _ctrl;
        private UIImage _image;

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement == null || Control == null)
            {
                return;
            }

            _ctrl = Element as ImageWithTint;
            _image = Control.Image?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            ChangeTintColor();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e?.PropertyName != nameof(ImageWithTint.TintColor) && e?.PropertyName != nameof(Image.Source) || Control == null)
            {
                return;
            }

            _image = Control.Image?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            ChangeTintColor();
        }

        private void ChangeTintColor()
        {
            if (_ctrl.TintColor.IsDefault || _image == null || Control == null)
            {
                return;
            }

            Control.TintColor = _ctrl.TintColor.ToUIColor();
            Control.Image = _image;
        }
    }
}