using Xamarin.Essentials;

namespace XamarinForms.Core.Infrastructure.Permissions;

public interface IAllowNotificationsPermission
{
    Task<PermissionStatus> CheckStatusAsync();
    Task<PermissionStatus> RequestAsync();
}