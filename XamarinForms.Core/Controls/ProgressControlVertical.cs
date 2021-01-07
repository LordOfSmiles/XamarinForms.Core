using System;
using Xamarin.Forms;

namespace XamarinForms.Core.Controls
{
    public sealed class ProgressControlVertical : Grid
    {
        #region Fields

        private readonly BoxView _bxActive;
        private readonly BoxView _bxNormal;

        #endregion

        public ProgressControlVertical()
        {
            RowSpacing = 0;
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions.Add(new RowDefinition());

            {
                _bxNormal = new BoxView();
                Children.Add(_bxNormal);
            }
            
            {
                _bxActive = new BoxView();
                SetRow(_bxActive, 1);
                Children.Add(_bxActive);
            }
            
            SizeChanged += CtrlRootOnSizeChanged;
        }

        #region Bindable Properties

        #region FromTopToBottom

        public static BindableProperty FromTopToBottomProperty = BindableProperty.Create(nameof(FromTopToBottom),
            typeof(bool),
            typeof(ProgressControlVertical),
            false,
            propertyChanged: OnFromTopToBottomChanged);

        public bool FromTopToBottom
        {
            get => (bool) GetValue(FromTopToBottomProperty);
            set => SetValue(FromTopToBottomProperty, value);
        }

        private static void OnFromTopToBottomChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            var ctrl = bindableObject as ProgressControlVertical;
            if (ctrl == null)
                return;

            ctrl.UpdateControl();
        }

        #endregion
        
        #region Percentvalue

        public static readonly BindableProperty PercentValueProperty = BindableProperty.Create(nameof(PercentValue), typeof(double), typeof(ProgressControlVertical), 0.0, propertyChanged: OnPercentValueChanged);

        public double PercentValue
        {
            get => (double)GetValue(PercentValueProperty);
            set => SetValue(PercentValueProperty, value);
        }

        private static void OnPercentValueChanged(BindableObject bo, object oldValue, object newValue)
        {
            var ctrl = bo as ProgressControlVertical;
            if (ctrl == null)
                return;

            ctrl.UpdateControl();
        }

        #endregion

        #region Bottom Color

        public static readonly BindableProperty ActiveColorProperty = BindableProperty.Create(nameof(ActiveColor), typeof(Color), typeof(ProgressControlVertical), Color.Red, propertyChanged: OnLeftColorChanged);

        public Color ActiveColor
        {
            get => (Color)GetValue(ActiveColorProperty);
            set => SetValue(ActiveColorProperty, value);
        }

        private static void OnLeftColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as ProgressControlVertical;
            if (ctrl == null)
                return;

            ctrl._bxActive.Color = (Color)newValue;
        }

        #endregion

        #region Top Color

        public static readonly BindableProperty NormalColorProperty = BindableProperty.Create(nameof(NormalColor), typeof(Color), typeof(ProgressControlVertical), Color.White, propertyChanged: OnRightColorChanged);

        public Color NormalColor
        {
            get => (Color)GetValue(NormalColorProperty);
            set => SetValue(NormalColorProperty, value);
        }

        private static void OnRightColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = bindable as ProgressControlVertical;
            if (ctrl == null)
                return;

            ctrl._bxNormal.Color = (Color)newValue;
        }


        #endregion

        #endregion

        #region Handlers

        private void CtrlRootOnSizeChanged(object sender, EventArgs eventArgs)
        {
            UpdateControl();
        }

        #endregion

        #region Private Mtehods

        private void UpdateControl()
        {
            var actualWidth = Height;

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
                filledValue = PercentValue * actualWidth;
            }

            if (filledValue < 0)
                filledValue = 0;

            if (filledValue > 1)
                filledValue = 1;

            if (FromTopToBottom)
            {
                RowDefinitions[0].Height = new GridLength(filledValue, GridUnitType.Absolute);
            }
            else
            {
                RowDefinitions[1].Height = new GridLength(filledValue, GridUnitType.Absolute);
            }
        }

        #endregion
    }
}

