namespace XamarinForms.Core.PlatformServices;

public interface IExtendedDevicePlatformService
{
    bool IsDeviceWithSafeArea { get; }
    
    void GoToPowerSettings();

    bool IsIgnoredPowerOptimizations { get; }
}