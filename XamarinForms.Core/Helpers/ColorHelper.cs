using Xamarin.Forms;

namespace XamarinForms.Core.Helpers;

public static class ColorHelper
{
    public static Color GetTextColorForBackground(Color backgroundColor, Color darkTextColor, Color lightTextColor)
    {
        return IsLightBackground(backgroundColor)
            ? darkTextColor
            : lightTextColor;
    }

    public static bool IsLightBackground(Color backgroundColor)
    {
        var r = backgroundColor.R;
        var g = backgroundColor.G;
        var b = backgroundColor.B;

        var yiq = ((r * 299) + (g * 587) + (b * 114)) / 1000;

        return yiq >= 0.5;
    }
}