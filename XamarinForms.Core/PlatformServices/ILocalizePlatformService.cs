using System.Globalization;

namespace XamarinForms.Core.PlatformServices;

public interface ILocalizePlatformService
{
    CultureInfo GetCurrentCultureInfo();
    void SetLocale(CultureInfo ci);
}