using Xamarin.CommunityToolkit.Extensions;

namespace XamarinForms.Core.Helpers;

public static class ColorHelper
{
    public static bool IsTransparent(this Color color) => color == Color.Transparent;

    public static bool IsColorsEquals(Color color1, Color color2)
    {
        return color1.ToHex() == color2.ToHex();
    }

    public static Color OnTheme(Color light, Color dark) => ThemeHelper.IsLightTheme
                                                                ? light
                                                                : dark;

    public static bool IsDarkForTheEye(this Color c)
    {
        return c.GetByteRed() * 0.299 + c.GetByteGreen() * 0.587 + c.GetByteBlue() * 0.114 <= 186.0;
    }

    public static bool IsLight(this Color c) => !IsDark(c);

    public static bool IsDark(this Color c)
    {
        return c.GetByteRed() + c.GetByteGreen() + c.GetByteBlue() <= 381;
    }

    public static Color CalculatePressedColor(Color backgroundColor, int koef = 20)
    {
        var alpha = backgroundColor.GetByteAlpha();
        var red = backgroundColor.GetByteRed();
        var green = backgroundColor.GetByteGreen();
        var blue = backgroundColor.GetByteBlue();

        var endColor = !backgroundColor.IsDark()
                           ? Color.FromRgba(red - koef, green - koef, blue - koef, alpha)
                           : Color.FromRgba(red + koef, green + koef, blue + koef, alpha);

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