namespace XamarinForms.Core.Helpers;

public static class FontHelper
{
    /// <summary>
    /// iOS - 17
    /// Android - 16
    /// </summary>
    public static readonly double CaptionFontSize = DeviceHelper.OnPlatform(17, 16);

    /// <summary>
    /// iOS - 15
    /// Android - 14
    /// </summary>
    public static readonly double PrimaryTextFontSize = DeviceHelper.OnPlatform(15, 14);

    /// <summary>
    /// iOS - 13
    /// Android - 14
    /// </summary>
    public static readonly double SecondaryTextFontSize = DeviceHelper.OnPlatform(13, 14);

    /// <summary>
    /// iOS - 11
    /// Android - 12
    /// </summary>
    public static readonly double SmallestTextFontSize = DeviceHelper.OnPlatform(11, 12);

    public static readonly double ButtonTextSize = 16;
    public static readonly double TextInputSize = DeviceHelper.OnPlatform(17, 16);
}