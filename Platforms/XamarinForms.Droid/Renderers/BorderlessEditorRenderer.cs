using Android.Content;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace XamarinForms.Droid.Renderers
{
    public sealed class BorderlessEditorRenderer:EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }

        public BorderlessEditorRenderer(Context context)
            : base(context)
        {
            
        }
    }
}