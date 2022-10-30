using Android.App;
using Android.Content;
using Xamarin.Forms.Platform.Android;
using XamarinForms.Core.Controls;

namespace XamarinForms.Droid.Renderers;

public sealed class BorderlessDatePickerRenderer : DatePickerRenderer
{
    protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
    {
        base.OnElementChanged(e);

        if (e.OldElement == null)
        {
            Control.Background = null;

            var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
            layoutParams.SetMargins(0, 0, 0, 0);
            LayoutParameters = layoutParams;
            Control.LayoutParameters = layoutParams;
            Control.SetPadding(0, 0, 0, 0);
            SetPadding(0, 0, 0, 0);
        }
    }
    
    protected override DatePickerDialog CreateDatePickerDialog(int year, int month, int day)
    {
        var dialog = base.CreateDatePickerDialog(year, month, day);
        if (Element is ExtendedDatePicker extendedDatePicker)
        {
            dialog.SetButton((int)DialogButtonType.Positive, "Ok", (sender, args) =>
            {
                extendedDatePicker.Date = dialog.DatePicker.DateTime;
                extendedDatePicker.RaiseDoneEvent();
            });
        }

        return dialog;
    }

    public BorderlessDatePickerRenderer(Context context)
        : base(context)
    {
            
    }
}

public sealed class BorderlessTimePickerRenderer : TimePickerRenderer
{
    protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
    {
        base.OnElementChanged(e);

        if (e.OldElement == null)
        {
            Control.Background = null;

            var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
            layoutParams.SetMargins(0, 0, 0, 0);
            LayoutParameters = layoutParams;
            Control.LayoutParameters = layoutParams;
            Control.SetPadding(0, 0, 0, 0);
            SetPadding(0, 0, 0, 0);
        }
    }

    public BorderlessTimePickerRenderer(Context context)
        : base(context)
    {
    }
}