using System;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace XamarinForms.Core.Standard.Extensions
{
    public static class ViewExtensions
    {
        private static string ColorToHandle = "ColorTo";

        public static void CancelColorAnimations(View view)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            view.AbortAnimation(ColorToHandle);
        }

        public static Task<bool> ColorTo(this View view, Color toColor, uint length = 250, Easing easing = null)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));
            if (easing == null)
                easing = Easing.Linear;

            var tcs = new TaskCompletionSource<bool>();
            var startColor = view.BackgroundColor;

            new Animation(AnimateColorCallback(view, startColor, toColor), 0, 1, easing)
                .Commit(view, ColorToHandle, 16, length, finished: (f, a) => tcs.SetResult(a));

            return tcs.Task;
        }

        private static Action<double> AnimateColorCallback(View view, Color startColor, Color toColor)
        {
            Func<double, Color> computeColor = progress =>
            {
                var r = startColor.R + (toColor.R - startColor.R) * progress;
                var g = startColor.G + (toColor.G - startColor.G) * progress;
                var b = startColor.B + (toColor.B - startColor.B) * progress;
                var a = startColor.A + (toColor.A - startColor.A) * progress;

                return Color.FromRgba(r, g, b, a);
            };

            var weakView = new WeakReference<View>(view);

            View frameRef;
            if (!weakView.TryGetTarget(out frameRef))
            {
                throw new ArgumentException("Can't target View");
            }

            Action<double> setColor = f =>
            {
                frameRef.BackgroundColor = computeColor(f);
            };
            return setColor;
        }
    }
}
