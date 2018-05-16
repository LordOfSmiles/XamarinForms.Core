using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Controls
{
    public sealed class GradientView : View
    {
        #region StartColor

        public Color StartColor
        {
            get => (Color)GetValue(StartColorProperty);
            set => SetValue(StartColorProperty, value);
        }

        public static readonly BindableProperty StartColorProperty = BindableProperty.Create(nameof(StartColor), typeof(Color), typeof(GradientView), Color.White);

        #endregion

        #region EndColor

        public Color EndColor
        {
            get => (Color)GetValue(EndColorProperty);
            set => SetValue(EndColorProperty, value);
        }

        public static readonly BindableProperty EndColorProperty = BindableProperty.Create(nameof(EndColor), typeof(Color), typeof(GradientView), Color.Black);

        #endregion
    }
}
