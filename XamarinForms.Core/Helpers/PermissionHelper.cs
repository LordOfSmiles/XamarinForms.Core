using Xamarin.Essentials;
using XamarinForms.Core.Infrastructure.Permissions;

namespace XamarinForms.Core.Helpers;

public static class PermissionHelper
{
    public static async Task<bool> IsNotificationsAllowedAsync()
    {
        bool hasPermission;

        if (DeviceHelper.IsIos
            || (DeviceHelper.IsAndroid && VersionHelper.IsEqualOrGreater(13)))
        {
            var permission = DependencyService.Get<IAllowNotificationsPermission>();
            var status = await permission.CheckStatusAsync();
            hasPermission = status == PermissionStatus.Granted;
        }
        else
        {
            hasPermission = true;
        }

        return hasPermission;
    }
    
    public static async Task<bool> RequestNotificationsPermissionAsync()
    {
        bool hasPermission;

        if (DeviceHelper.IsIos
            || (DeviceHelper.IsAndroid && VersionHelper.IsEqualOrGreater(13)))
        {
            var permission = DependencyService.Get<IAllowNotificationsPermission>();
            var status = await permission.CheckStatusAsync();
            if (status != PermissionStatus.Granted)
            {
                status = await permission.RequestAsync();
                hasPermission = status == PermissionStatus.Granted;
            }
            else
            {
                hasPermission = true;
            }
        }
        else
        {
            hasPermission = true;
        }

        return hasPermission;
    }

    public static async Task<bool> CanReadStorageAsync()
    {
        bool hasPermission;

        if (DeviceHelper.IsAndroid
            && VersionHelper.IsEqualOrGreater(13))
        {
            hasPermission = true;
        }
        else
        {
            hasPermission = await CheckAndRequestPermission(new Permissions.StorageRead());
        }

        return hasPermission;
    }

    public static async Task<bool> CanWriteStorageAsync()
    {
        bool hasPermission;
        if (DeviceHelper.IsAndroid
            && VersionHelper.IsEqualOrGreater(11))
        {
            hasPermission = true;
        }
        else
        {
            hasPermission = await CheckAndRequestPermission(new Permissions.StorageWrite());
        }

        return hasPermission;
    }

    public static async Task<bool> CheckAndRequestPermission<T>(T permission)
        where T : Permissions.BasePermission
    {
        bool result;

        var status = await permission.CheckStatusAsync();

        if (status != PermissionStatus.Granted)
        {
            if (permission.ShouldShowRationale())
            {
                // Prompt the user with additional information as to why the permission is needed
            }

            status = await permission.RequestAsync();
            result = status == PermissionStatus.Granted;
        }
        else
        {
            result = true;
        }

        return result;
    }
}