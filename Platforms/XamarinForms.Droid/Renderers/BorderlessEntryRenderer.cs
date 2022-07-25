using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace XamarinForms.Droid.Renderers;

public sealed class BorderlessEntryRenderer : EntryRenderer
{
    public BorderlessEntryRenderer(Context context) : base(context)
    {
    }

    protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
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
}