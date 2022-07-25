using Android.Content;
using Xamarin.Forms.Platform.Android;

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

    public BorderlessDatePickerRenderer(Context context)
        : base(context)
    {
            
    }
}