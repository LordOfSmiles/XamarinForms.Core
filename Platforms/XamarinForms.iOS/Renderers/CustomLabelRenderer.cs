using System;
using Xamarin.Forms.Platform.iOS;
using XamarinForms.Core.Controls.Renderers;

namespace XamarinForms.iOS.Renderers;

public sealed class CustomLabelRenderer : LabelRenderer
{
    protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
    {
        base.OnElementChanged(e);

        if (Control != null
            && Element is CustomLabel customLabel)
        {
            var font = customLabel.FontWeight switch
            {
                FontWeightTypeEnum.Light   => UIFont.SystemFontOfSize((nfloat)Element.FontSize, UIFontWeight.Light),
                FontWeightTypeEnum.Regular => UIFont.SystemFontOfSize((nfloat)Element.FontSize, UIFontWeight.Regular),
                FontWeightTypeEnum.Medium  => UIFont.SystemFontOfSize((nfloat)Element.FontSize, UIFontWeight.Medium),
                FontWeightTypeEnum.Bold    => UIFont.SystemFontOfSize((nfloat)Element.FontSize, UIFontWeight.Bold),
                _                      => throw new ArgumentOutOfRangeException()
            };

            Control.Font = font;
        }
    }
}