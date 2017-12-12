using System;
using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Controls
{
    public partial class ProgressControl : ContentView
    {

        public ProgressControl()
        {
            InitializeComponent();
            SizeChanged += CtrlRootOnSizeChanged;
        }

        #region Bindable Properties

        #region Percentvalue

        public static readonly BindableProperty PercentValueProperty = BindableProperty.Create("PercentValue", typeof(double), typeof(ProgressControl), 0.0, propertyChanged: OnPercentValueChanged);

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

        public static readonly BindableProperty LeftColorProperty = BindableProperty.Create("LeftColor", typeof(Color), typeof(ProgressControl), Color.Red);

        public Color LeftColor
        {
            get => (Color)GetValue(LeftColorProperty);
            set => SetValue(LeftColorProperty, value);
        }

        #endregion

        #region Right Color

        public static readonly BindableProperty RightColorProperty = BindableProperty.Create("RightColor", typeof(Color), typeof(ProgressControl), Color.White);

        public Color RightColor
        {
            get => (Color)GetValue(RightColorProperty);
            set => SetValue(RightColorProperty, value);
        }

        #endregion

        #endregion

        #region Private Mtehods

        private void FillGridColumn()
        {
            var actualWidth = grdRoot.Width;

            double filledValue;

            if (PercentValue < 0)
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

            grdRoot.ColumnDefinitions[0].Width = new GridLength(filledValue, GridUnitType.Absolute);
        }

        #endregion

        #region Handlers

        private void CtrlRootOnSizeChanged(object sender, EventArgs eventArgs)
        {
            FillGridColumn();
        }

        #endregion
    }
}

