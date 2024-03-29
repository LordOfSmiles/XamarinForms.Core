using Xamarin.CommunityToolkit.Extensions;

namespace XamarinForms.Core.Helpers;

public static class ColorHelper
{
    public static bool IsTransparent(this Color color) => color == Color.Transparent;

    public static bool IsColorsEquals(Color color1, Color color2)
    {
        return color1.ToHex() == color2.ToHex();
    }

    public static Color OnColor(this Color color, Color onLight, Color onDark) => color.IsLight()
                                                                                      ? onLight
                                                                                      : onDark;

    public static Color OnTheme(Color light, Color dark) => ThemeHelper.IsLightTheme
                                                                ? light
                                                                : dark;

    public static bool IsLightForTheEye(this Color c) => !c.IsDarkForTheEye();

    public static bool IsDarkForTheEye(this Color c)
    {
        return c.GetByteRed() * 0.299 + c.GetByteGreen() * 0.587 + c.GetByteBlue() * 0.114 <= 186.0;
    }

    public static bool IsLight(this Color c) => !IsDark(c);

    public static bool IsDark(this Color c)
    {
        return c.GetByteRed() + c.GetByteGreen() + c.GetByteBlue() <= 381;
    }

    public static Color CalculatePressedColor(Color backgroundColor, int koef = 15)
    {
        var alpha = backgroundColor.GetByteAlpha();
        var red = backgroundColor.GetByteRed();
        var green = backgroundColor.GetByteGreen();
        var blue = backgroundColor.GetByteBlue();


        Color endColor;

        if (alpha == 255)
        {
            endColor = backgroundColor.IsLight()
                           ? Color.FromRgb(red - koef, green - koef, blue - koef)
                           : Color.FromRgb(red + koef, green + koef, blue + koef);
        }
        else
        {
            var color = new Color(red, green, blue);

            endColor = color.IsLight()
                           ? Color.FromRgba(red, green, blue, alpha - koef)
                           : Color.FromRgba(red, green, blue, alpha + koef);
        }

        return endColor;
    }

    public static Color FindParentRealColor(View view)
    {
        Color result;

        if (!view.BackgroundColor.IsTransparent()
            && !view.BackgroundColor.IsDefault)
        {
            result = view.BackgroundColor;
        }
        else if (view.Parent is View parent)
        {
            result = FindParentRealColor(parent);
        }
        else if (view.Parent is Page page)
        {
            result = page.BackgroundColor;
        }
        else
        {
            result = Color.Transparent;
        }

        return result;
    }
}