namespace XamarinForms.Core.PlatformServices;

public interface IAdmobRewardedPlatformService
{
    Task<bool> ShowAsync(string adUnitId);
}