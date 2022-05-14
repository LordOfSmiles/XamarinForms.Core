using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("XamarinForms.iOS.Effects")]
[assembly: ExportEffect(typeof(XamarinForms.iOS.Effects.PickerWithoutBorderEffect), "PickerWithoutBorderEffect")]
namespace XamarinForms.iOS.Effects;

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