using System;
using System.Threading.Tasks;
using UserNotifications;
using Xamarin.Essentials;
using XamarinForms.Core.Helpers;
using XamarinForms.Core.Infrastructure.Permissions;

namespace XamarinForms.iOS.PlatformServices;

public sealed class AllowNotificationsPermission : IAllowNotificationsPermission
{
    public async Task<PermissionStatus> RequestAsync()
    {
        var (isAllowed, _) = await UNUserNotificationCenter.Current.RequestAuthorizationAsync(UNAuthorizationOptions.Alert
                                                                                              | UNAuthorizationOptions.Badge
                                                                                              | UNAuthorizationOptions.Sound)
                                                           .ConfigureAwait(false);

        return isAllowed
                   ? PermissionStatus.Granted
                   : PermissionStatus.Denied;
    }

    public async Task<PermissionStatus> CheckStatusAsync()
    {
        var isAllowed = true;

        if (VersionHelper.IsEqualOrGreater(10))
        {
            var settings = await UNUserNotificationCenter.Current.GetNotificationSettingsAsync()
                                                         .ConfigureAwait(false);

            isAllowed = settings.AlertSetting == UNNotificationSetting.Enabled;
        }

        return isAllowed
                   ? PermissionStatus.Granted
                   : PermissionStatus.Denied;
    }
}