using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinForms.Core.iOS.Renderers;
using XamarinForms.Core.Standard.Controls;

[assembly: ExportRenderer(typeof(CircleView), typeof(CircleViewRenderer))]
namespace XamarinForms.Core.iOS.Renderers
{
    public sealed class CircleViewRenderer : BoxRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
                return;

            Layer.MasksToBounds = true;
            Layer.CornerRadius = (float)((CircleView)Element).CornerRadius;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var circle = Element as CircleView;
            if (circle == null)
                return;

            if (e.PropertyName == CircleView.CornerRadiusProperty.PropertyName)
            {
                Layer.CornerRadius = (float)circle.CornerRadius;
            }
        }
    }
}
