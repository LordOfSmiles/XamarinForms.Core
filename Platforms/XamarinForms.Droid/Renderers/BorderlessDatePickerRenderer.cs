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
            dialog.SetButton((int)DialogButtonType.Positive,
                             "Ok",
                             (_, _) =>
                             {
                                 extendedDatePicker.Date = dialog.DatePicker.DateTime;
                                 extendedDatePicker.RaiseDoneEvent();
                             });
        }

        return dialog;
    }

    public BorderlessDatePickerRenderer(Context context)
        : base(context)
    { }
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

    protected override TimePickerDialog CreateTimePickerDialog(int hours, int minutes)
    {
        var dialog = base.CreateTimePickerDialog(hours, minutes);

        if (Element is ExtendedTimePicker extendedTimePicker)
        {
            dialog.DismissEvent += (_, _) =>
            {
                extendedTimePicker.RaiseDoneEvent();
            };

            dialog.CancelEvent += (_, _) =>
            {
                extendedTimePicker.RaiseCancelEvent();
            };
        }

        return dialog;
    }

    public BorderlessTimePickerRenderer(Context context)
        : base(context)
    { }
}