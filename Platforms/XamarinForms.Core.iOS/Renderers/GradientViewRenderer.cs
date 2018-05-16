using System;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using Xamarin.Forms.Platform.iOS;
using XamarinForms.Core.Standard.Controls;

namespace XamarinForms.Core.iOS.Renderers
{
    public sealed class GradientViewRenderer : VisualElementRenderer<GradientView>
    {
        public override void LayoutSubviews()
        {
            foreach (var layer in Layer.Sublayers?.Where(layer => layer is CAGradientLayer))
            {
                layer.Frame = Bounds;
            }
            base.LayoutSubviews();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<GradientView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var gradient = new CAGradientLayer();
                gradient.Colors = new CGColor[]
                {
                    Element.StartColor.ToCGColor(),
                    Element.EndColor.ToCGColor()
                };
                var layer = Layer.Sublayers?.LastOrDefault();
                if (layer == null)
                    Layer.AddSublayer(gradient);
                else
                    Layer.InsertSublayerBelow(gradient, layer);
            }
        }
    }
}
