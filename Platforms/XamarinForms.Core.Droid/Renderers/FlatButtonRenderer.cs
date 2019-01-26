
using Android.App;
using Android.Content;
using Android.Util;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinForms.Core.Droid.Renderers;
using XamarinForms.Core.Standard.Controls;

[assembly: ExportRenderer(typeof(AndroidFlatButton), typeof(FlatButtonRenderer))]

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

