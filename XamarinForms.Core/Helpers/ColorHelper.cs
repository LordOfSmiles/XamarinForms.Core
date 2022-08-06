using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace XamarinForms.Core.Helpers;

public static class ColorHelper
{
    public static Color GetTextColorForBackground(Color background, Color textColorForLightBackground, Color textColorForDarkBackground)
    {
        return IsLightColor(background)
            ? textColorForDarkBackground
            : textColorForLightBackground;
    }

    public static bool IsLightColor(Color background)
    {
        var r = background.GetByteRed();
        var g = background.GetByteGreen();
        var b = background.GetByteBlue();

        var luminosity = ((r * 0.299) + (g * 0.587) + (b * 0.114)) / 255;

        return background.Luminosity >= 0.5;
    }
}