
using Android.App;
using Android.Content;
using Android.Util;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinForms.Core.Droid.Renderers;
using XamarinForms.Core.Standard.Controls;

namespace XamarinForms.Core.Droid.Renderers
{
    public sealed class FlatButtonRenderer : ButtonRenderer
    {
        public FlatButtonRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
        }
    }
}

