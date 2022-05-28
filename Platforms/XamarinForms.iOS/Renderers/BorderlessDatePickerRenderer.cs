using System.ComponentModel;
using Xamarin.Forms.Platform.iOS;
using XamarinForms.Core.Helpers;


namespace XamarinForms.iOS.Renderers;

public sealed class BorderlessDatePickerRenderer : DatePickerRenderer
{
    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.OnElementPropertyChanged(sender, e);

        if (Control != null)
        {
            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;

            var picker = (UIDatePicker)Control.InputView;
            if (VersionHelper.IsEqualOrGreater(13, 4) && picker != null)
                picker.PreferredDatePickerStyle = UIDatePickerStyle.Wheels;
        }
    }
}

public sealed class BorderlessTimePickerRenderer : TimePickerRenderer
{
    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.OnElementPropertyChanged(sender, e);

        if (Control != null)
        {
            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;

            var picker = (UIDatePicker)Control.InputView;
            if (VersionHelper.IsEqualOrGreater(13, 4) && picker != null)
                picker.PreferredDatePickerStyle = UIDatePickerStyle.Wheels;
        }
    }
}