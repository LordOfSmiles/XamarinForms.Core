namespace XamarinForms.Core.PlatformServices;

public interface IDroidDependencyService
{
    bool CanUseExactsAlarms { get; }
    void GoToExactAlarmSettings();
    
    bool IsInPowerWhiteList { get; }
    void GoToPowerSettings();
    
    Task<bool> SaveToDownloadsAsync(string pathToData);

    Task<bool> SaveToDownloadsAsync(byte[] data, string fileName);
}