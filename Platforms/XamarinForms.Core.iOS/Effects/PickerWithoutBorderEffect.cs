using Xamarin.Forms.Platform.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: ResolutionGroupName("XamarinForms.Core.iOS.Effects")]
[assembly: ExportEffect(typeof(XamarinForms.Core.iOS.Effects.PickerWithoutBorderEffect), "PickerWithoutBorderEffect")]
namespace XamarinForms.Core.iOS.Effects
{
    public sealed class PickerWithoutBorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var uiTextView = Control as UITextField;
            if (uiTextView != null)
            {
                uiTextView.Layer.BorderColor = UIColor.Clear.CGColor;
            }
        }

        protected override void OnDetached()
        {

        }
    }
}
