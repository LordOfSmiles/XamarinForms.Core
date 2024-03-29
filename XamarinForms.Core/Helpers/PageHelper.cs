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
            && DependencyService.Get<IIosDependencyService>().IsDeviceWithSafeArea)
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
                bottom += 34;
            }
        }

        return new Thickness(0, 16, 0, bottom);
    }

    public static Thickness GetScrollContentPadding(Xamarin.Forms.Page page = null)
    {
        var bottom = 64;

        if (DeviceHelper.IsIos
            && DependencyService.Get<IIosDependencyService>().IsDeviceWithSafeArea)
        {
            bottom += 16;
            
            page ??= Application.Current.MainPage switch
            {
                Shell shell       => shell.CurrentPage,
                TabbedPage tabbed => tabbed.CurrentPage,
                _                 => page
            };

            page ??= Application.Current.MainPage;

            if (!page.On<iOS>().UsingSafeArea())
            {
                bottom += 34;
            }
        }

        return new Thickness(0, 16, 0, bottom);
    }
}