using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinForms.Core.Extensions
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
            var weakView = new WeakReference<View>(view);

            View frameRef;
            if (!weakView.TryGetTarget(out frameRef))
            {
                throw new ArgumentException("Can't target View");
            }

            Action<double> setColor = f => { frameRef.BackgroundColor = ComputeColor(startColor, toColor, f); };
            return setColor;
        }

        private static Color ComputeColor(Color startColor, Color endColor, double progress)
        {
            var r = startColor.R + (endColor.R - startColor.R) * progress;
            var g = startColor.G + (endColor.G - startColor.G) * progress;
            var b = startColor.B + (endColor.B - startColor.B) * progress;
            var a = startColor.A + (endColor.A - startColor.A) * progress;

            return Color.FromRgba(r, g, b, a);
        }
    }
}
