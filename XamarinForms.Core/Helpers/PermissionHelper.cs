using Xamarin.Essentials;
using XamarinForms.Core.Infrastructure.Permissions;

namespace XamarinForms.Core.Helpers;

public static class PermissionHelper
{
    public static async Task<bool> CheckAndRequestNotificationsAsync()
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

    public static async Task<bool> CheckNotificationsAsync(IAllowNotificationsPermission permission)
    {
        bool hasPermission;

        if (DeviceHelper.IsIos
            || (DeviceHelper.IsAndroid && VersionHelper.IsEqualOrGreater(13)))
        {
            var status = await permission.CheckStatusAsync();
            
            hasPermission = status == PermissionStatus.Granted;
        }
        else
        {
            hasPermission = true;
        }

        return hasPermission;
    }

    public static async Task<bool> CheckAndRequestReadStorageAsync()
    {
        bool hasPermission;

        if (DeviceHelper.IsAndroid
            && VersionHelper.IsEqualOrGreater(13))
        {
            hasPermission = true;
        }
        else
        {
            hasPermission = await CheckAndRequestAsync(new Permissions.StorageRead());
        }

        return hasPermission;
    }

    public static async Task<bool> CheckAndRequestWriteStorageAsync()
    {
        bool hasPermission;

        if (DeviceHelper.IsAndroid
            && VersionHelper.IsEqualOrGreater(11))
        {
            hasPermission = true;
        }
        else
        {
            hasPermission = await CheckAndRequestAsync(new Permissions.StorageWrite());
        }

        return hasPermission;
    }
    
    public static async Task<bool> CheckWriteStorageAsync()
    {
        bool hasPermission;

        if (DeviceHelper.IsAndroid
            && VersionHelper.IsEqualOrGreater(11))
        {
            hasPermission = true;
        }
        else
        {
            hasPermission = await CheckAsync(new Permissions.StorageWrite());
        }

        return hasPermission;
    }

    public static async Task<bool> CheckAndRequestAsync<T>(T permission)
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
    
    public static async Task<bool> CheckAsync<T>(T permission)
        where T : Permissions.BasePermission
    {
        var status = await permission.CheckStatusAsync();

        var result = status == PermissionStatus.Granted;

        return result;
    }
}