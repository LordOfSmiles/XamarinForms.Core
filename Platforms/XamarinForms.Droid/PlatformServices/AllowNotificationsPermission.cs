using XamarinForms.Core.Infrastructure.Permissions;

namespace XamarinForms.Droid.PlatformServices;

public sealed class AllowNotificationsPermission : Xamarin.Essentials.Permissions.BasePlatformPermission, IAllowNotificationsPermission
{
    public override (string androidPermission, bool isRuntime)[] RequiredPermissions => new[]
    {
        (Android.Manifest.Permission.PostNotifications, true)
    };
}