using System;
using Android.Content;
using Android.Graphics;
using Xamarin.Forms.Platform.Android;
using XamarinForms.Core.Standard.Controls;

namespace XamarinForms.Core.Droid.Renderers
{
    public sealed class GradientViewRenderer : VisualElementRenderer<GradientView>
    {
        public GradientViewRenderer(Context context)
            : base(context)
        {
        }

        protected override void UpdateBackgroundColor()
        {
            SetBackgroundColor(Android.Graphics.Color.Transparent);
        }

        /// <summary>
        /// Called by draw to draw the child views.
        /// </summary>
        /// <param name = "canvas"> the canvas on which to draw the view</param>
        protected override void DispatchDraw(Canvas canvas)
        {
            var w = Width;
            var h = Height;

            Paint paint = new Paint();
            LinearGradient shader = new LinearGradient(0f, 0, 0, h, Element.StartColor.ToAndroid(), Element.EndColor.ToAndroid(), Shader.TileMode.Clamp);
            paint.SetShader(shader);
            canvas.DrawRect(0, 0, w, h, paint);
        }
    }
}
