using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinForms.Core.iOS.Renderers;
using XamarinForms.Core.Standard.Views;


namespace XamarinForms.Core.iOS.Renderers
{
    public sealed class CustomTabbedPageRenderer : TabbedRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
                return;
            

            var page = Element as CustomTabbedPage;
            if (page == null)
                return;

            TabBar.TintColor = page.TintColor.ToUIColor();
        }
    }
}
