using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinForms.Core.Droid.Renderers;
using XamarinForms.Core.Standard.Controls;

[assembly: ExportRenderer(typeof(CircleView), typeof(CircleViewRenderer))]
namespace XamarinForms.Core.Droid.Renderers
{
    public class CircleViewRenderer : BoxRenderer
    {
        private float _cornerRadius;
        private RectF _bounds;
        private Path _path;

        public CircleViewRenderer(Context context)
            : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
            {
                return;
            }
            var element = (CircleView)Element;

            _cornerRadius = TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)element.CornerRadius, Context.Resources.DisplayMetrics);

        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            if ((w != oldw && h != oldh) || _bounds == null)
            {
                _bounds = new RectF(0, 0, w, h);
            }

            _path = new Path();
            _path.Reset();
            _path.AddRoundRect(_bounds, _cornerRadius, _cornerRadius, Path.Direction.Cw);
            _path.Close();
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Save();
            canvas.ClipPath(_path);
            base.Draw(canvas);
            //canvas.DrawCircle(_bounds.Width() / 2, _bounds.Height() / 2, _cornerRadius, new Paint(PaintFlags.AntiAlias));
            canvas.Restore();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var circle = Element as CircleView;
            if (circle == null)
                return;

            if (e.PropertyName == CircleView.CornerRadiusProperty.PropertyName)
            {
                _cornerRadius = TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)circle.CornerRadius, Context.Resources.DisplayMetrics);
            }
        }
    }
}
