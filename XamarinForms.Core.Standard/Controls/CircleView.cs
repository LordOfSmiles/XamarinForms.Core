using System;
using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Controls
{
    public sealed class CircleView : BoxView
    {
        #region Bindable Proeprties

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(CircleView), 0.0);

        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        #endregion
    }
}
