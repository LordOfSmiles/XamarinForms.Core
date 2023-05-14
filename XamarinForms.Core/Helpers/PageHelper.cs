using Xamarin.Forms.PlatformConfiguration;
using XamarinForms.Core.PlatformServices;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Application = Xamarin.Forms.Application;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace XamarinForms.Core.Helpers;

public static class PageHelper
{
    public static Thickness GetBottomButtonMargin(Xamarin.Forms.Page page = null)
    {
        var bottom = 16;

        if (DeviceHelper.IsIos
            && DependencyService.Get<IExtendedDevicePlatformService>().IsDeviceWithSafeArea)
        {
            page ??= Application.Current.MainPage switch
            {
                Shell shell       => shell.CurrentPage,
                TabbedPage tabbed => tabbed.CurrentPage,
                _                 => page
            };

            page ??= Application.Current.MainPage;

            if (!page.On<iOS>().UsingSafeArea())
            {
                bottom = 50;
            }
        }

        return new Thickness(0, 0, 0, bottom);
    }
}