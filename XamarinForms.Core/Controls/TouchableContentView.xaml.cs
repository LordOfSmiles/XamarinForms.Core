using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinForms.Core.Controls;


public partial class TouchableContentView
{
    public TouchableContentView()
    {
        InitializeComponent();
    }
    
    #region Bindable Properties

    #region PressedColor

    public static readonly BindableProperty PressedColorProperty = BindableProperty.Create(nameof(PressedColor),
        typeof(Color),
        typeof(TouchableContentView));

    public Color PressedColor
    {
        get => (Color)GetValue(PressedColorProperty);
        set => SetValue(PressedColorProperty, value);
    }

    #endregion

    #region NormalColor

    public static readonly BindableProperty NormalColorProperty = BindableProperty.Create(nameof(NormalColor),
        typeof(Color),
        typeof(TouchableContentView));

    public Color NormalColor
    {
        get => (Color)GetValue(NormalColorProperty);
        set => SetValue(NormalColorProperty, value);
    }

    #endregion

    #endregion
}