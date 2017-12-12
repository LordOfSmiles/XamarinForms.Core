// ***********************************************************************
// Assembly         : XLabs.Forms
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="GradientContentView.cs" company="XLabs Team">
//     Copyright (c) XLabs Team. All rights reserved.
// </copyright>
// <summary>
//       This project is licensed under the Apache 2.0 license
//       https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/LICENSE
//       
//       XLabs is a open source project that aims to provide a powerfull and cross 
//       platform set of controls tailored to work with Xamarin Forms.
// </summary>
// ***********************************************************************
// 

using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Controls
{
    /// <summary>
    /// Enum GradientOrientation
    /// </summary>
    public enum GradientOrientation
    {
        /// <summary>
        /// The vertical
        /// </summary>
        Vertical,
        /// <summary>
        /// The horizontal
        /// </summary>
        Horizontal
    }

    /// <summary>
    /// ContentView that allows you to have a Gradient for
    /// the background. Let there be Gradients!
    /// </summary>
    public class GradientContentView : ContentView
    {
        #region Orientation

        public GradientOrientation Orientation
        {
            get => (GradientOrientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public static readonly BindableProperty OrientationProperty =BindableProperty.Create(nameof(Orientation),typeof(GradientOrientation),typeof(GradientContentView),GradientOrientation.Vertical);

        #endregion

        #region StartColor

        public Color StartColor
        {
            get => (Color)GetValue(StartColorProperty);
            set => SetValue(StartColorProperty, value);
        }

        public static readonly BindableProperty StartColorProperty = BindableProperty.Create(nameof(StartColor), typeof(Color), typeof(GradientContentView), Color.White);

        #endregion

        #region EndColor

        public Color EndColor
        {
            get => (Color)GetValue(EndColorProperty);
            set => SetValue(EndColorProperty, value);
        }

        public static readonly BindableProperty EndColorProperty = BindableProperty.Create(nameof(EndColor), typeof(Color), typeof(GradientContentView), Color.Black);

        #endregion
    }
}
