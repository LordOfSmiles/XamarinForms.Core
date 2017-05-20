using System;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
    public sealed class ShapeView : BoxView
    {
        #region Bindable Proeprties

        #region ShapeType

        public static readonly BindableProperty ShapeTypeProperty = BindableProperty.Create("ShapeType", typeof(ShapeType), typeof(ShapeView), ShapeType.Box);

        public ShapeType ShapeType
        {
            get { return (ShapeType)GetValue(ShapeTypeProperty); }
            set { SetValue(ShapeTypeProperty, value); }
        }

        #endregion

        #region StrokeColor

        public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create("StrokeColor", typeof(Color), typeof(ShapeView), Color.Default);

        public Color StrokeColor
        {
            get { return (Color)GetValue(StrokeColorProperty); }
            set { SetValue(StrokeColorProperty, value); }
        }

        #endregion

        #region StrokeWidth

        public static readonly BindableProperty StrokeWidthProperty = BindableProperty.Create("StrokeWidth", typeof(float), typeof(ShapeView), 1f);

        public float StrokeWidth
        {
            get { return (float)GetValue(StrokeWidthProperty); }
            set { SetValue(StrokeWidthProperty, value); }
        }

        #endregion

        #region IndicatorPercentage

        public static readonly BindableProperty IndicatorPercentageProperty = BindableProperty.Create("IndicatorPercentage", typeof(float), typeof(ShapeView), 0f);

        public float IndicatorPercentage
        {
            get { return (float)GetValue(IndicatorPercentageProperty); }
            set
            {
                if (ShapeType != ShapeType.CircleIndicator)
                    throw new ArgumentException("Can only specify this property with CircleIndicator");

                SetValue(IndicatorPercentageProperty, value);
            }
        }

        #endregion

        #region CornerRadius

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create("CornerRadius", typeof(float), typeof(ShapeView), 0f);

        public float CornerRadius
        {
            get { return (float)GetValue(CornerRadiusProperty); }
            set
            {
                if (ShapeType != ShapeType.Box)
                    throw new ArgumentException("Can only specify this property with Box");
                SetValue(CornerRadiusProperty, value);
            }
        }

        #endregion

        #region Padding

        public static readonly BindableProperty PaddingProperty = BindableProperty.Create("Padding", typeof(Thickness), typeof(ShapeView), default(Thickness));

        public Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }

        #endregion

        #endregion
    }


    public enum ShapeType
    {
        Box,
        Circle,
        CircleIndicator
    }

}
