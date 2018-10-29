﻿using System;
using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Controls.RadioButton
{
    public sealed class RadioButtonView : View
    {
        #region Bindable Properties

        #region Checked

        public static readonly BindableProperty CheckedProperty = BindableProperty.Create(nameof(Checked), typeof(bool), typeof(RadioButtonView), false);

        public bool Checked
        {
            get => (bool)GetValue(CheckedProperty);
            set
            {
                SetValue(CheckedProperty, value);

                var eventHandler = CheckedChanged;
                eventHandler?.Invoke(this, value);
            }
        }

        #endregion

        #region Text

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(RadioButtonView), string.Empty);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        #endregion

        #region TextColor

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(RadioButtonView), Color.Default);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        #endregion

        #region FontSize

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(RadioButtonView), -1.0);

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        #endregion

        #region FontName

        public static readonly BindableProperty FontNameProperty = BindableProperty.Create("FontName", typeof(string), typeof(RadioButtonView), string.Empty);

        public string FontName
        {
            get => (string)GetValue(FontNameProperty);
            set => SetValue(FontNameProperty, value);
        }

        #endregion

        #endregion

        #region Events

        public EventHandler<bool> CheckedChanged;

        #endregion


#pragma warning disable CS0108 // Member hides inherited member; missing new keyword 
        /// <summary> 
        /// Gets or sets the identifier. 
        /// </summary> 
        /// <value>The identifier.</value> 
        public int Id { get; set; }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword 
    }

}
