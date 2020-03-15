using System;
using Xamarin.Forms;
using XamarinForms.Core.Extensions;

namespace XamarinForms.Core.Controls
{
    public class ProgressControl : Grid
    {
        #region Fields

        private readonly BoxView _bxLeft;
        private readonly BoxView _bxRight;

        #endregion

        public ProgressControl()
        {
            ColumnSpacing = 0;
            ColumnDefinitions.Add(new ColumnDefinition());
            ColumnDefinitions.Add(new ColumnDefinition());
            
            _bxLeft = new BoxView();
            Children.Add(_bxLeft);


            _bxRight = new BoxView()
                .Col(1);
            Children.Add(_bxRight);
            
            SizeChanged += CtrlRootOnSizeChanged;
        }

        #region Bindable Properties

        #region Percentvalue

        public static readonly BindableProperty PercentValueProperty = BindableProperty.Create(nameof(PercentValue),
            typeof(double),
            typeof(ProgressControl),
            0.0,
            propertyChanged: OnPercentValueChanged);

        public double PercentValue
        {
            get => (double)GetValue(PercentValueProperty);
            set => SetValue(PercentValueProperty, value);
        }

        private static void OnPercentValueChanged(BindableObject bo, object oldValue, object newValue)
        {
            var ctrl = bo as ProgressControl;
            if (ctrl == null)
                return;

            ctrl.FillGridColumn();
        }

        #endregion

        #region Left Color

        public static readonly BindableProperty LeftColorProperty = BindableProperty.Create(nameof(LeftColor), typeof(Color), typeof(ProgressControl), Color.Red, propertyChanged: OnLeftColorChanged);

        public Color LeftColor
        {
            get => (Color)GetValue(LeftColorProperty);
            set => SetValue(LeftColorProperty, value);
        }

        private static void OnLeftColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as ProgressControl;
            if (ctrl == null)
                return;

            ctrl._bxLeft.Color = (Color)newValue;
        }

        #endregion

        #region Right Color

        public static readonly BindableProperty RightColorProperty = BindableProperty.Create(nameof(RightColor), typeof(Color), typeof(ProgressControl), Color.White, propertyChanged: OnRightColorChanged);

        public Color RightColor
        {
            get => (Color)GetValue(RightColorProperty);
            set => SetValue(RightColorProperty, value);
        }

        private static void OnRightColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as ProgressControl;
            if (ctrl == null)
                return;

            ctrl._bxRight.Color = (Color)newValue;
        }


        #endregion

        #endregion

        #region Handlers

        private void CtrlRootOnSizeChanged(object sender, EventArgs eventArgs)
        {
            FillGridColumn();
        }

        #endregion

        #region Private Mtehods

        private void FillGridColumn()
        {
            if (Width.Equals(-1))
                return;
            
            var actualWidth = Width;

            double filledValue;

            if (PercentValue <= 0)
            {
                filledValue = 0.0;
            }
            else if (PercentValue >= 1)
            {
                filledValue = actualWidth;
            }
            else
            {
                filledValue = (PercentValue * actualWidth);
            }

            if (filledValue < 0)
                filledValue = 0;

            if (filledValue > actualWidth)
                filledValue = actualWidth;

            ColumnDefinitions[0].Width = new GridLength(filledValue, GridUnitType.Absolute);
        }

        #endregion
    }
}

