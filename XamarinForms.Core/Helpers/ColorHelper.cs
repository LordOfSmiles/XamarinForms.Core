using Xamarin.CommunityToolkit.Extensions;

namespace XamarinForms.Core.Helpers;

public static class ColorHelper
{
    public static bool IsColorsEquals(Color color1, Color color2)
    {
        return color1.ToHex() == color2.ToHex();
    }

    public static Color GetColorByTheme(Color lightColor, Color darkColor)
    {
        return ThemeHelper.IsDarkTheme
                   ? darkColor
                   : lightColor;
    }

    public static Color GetTextColorForBackground(Color background, Color textColorForLightBackground, Color textColorForDarkBackground)
    {
        return !IsDark(background)
                   ? textColorForDarkBackground
                   : textColorForLightBackground;
    }

    public static bool IsDarkForTheEye(this Color c)
    {
        return c.GetByteRed() * 0.299 + c.GetByteGreen() * 0.587 + c.GetByteBlue() * 0.114 <= 186.0;
    }

    public static bool IsDark(this Color c)
    {
        return c.GetByteRed() + c.GetByteGreen() + c.GetByteBlue() <= 381;
    }

    public static Color CalculatePressedColor(Color backgroundColor, int koef = 20)
    {
        var red = backgroundColor.GetByteRed();
        var green = backgroundColor.GetByteGreen();
        var blue = backgroundColor.GetByteBlue();

        var endColor = !backgroundColor.IsDark()
                           ? Color.FromRgb(red - koef, green - koef, blue - koef)
                           : Color.FromRgb(red + koef, green + koef, blue + koef);

        return endColor;
    }
}