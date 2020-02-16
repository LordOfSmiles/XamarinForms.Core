using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace XamarinForms.Core.iOS.Renderers
{
    public sealed class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
                Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}