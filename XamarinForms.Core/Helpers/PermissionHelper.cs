using Xamarin.Essentials;

namespace XamarinForms.Core.Helpers;

public static class PermissionHelper
{
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