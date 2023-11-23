namespace XamarinForms.Core.PlatformServices;

public interface IDroidDependencyService
{
    bool CanUseExactsAlarms { get; }
    void GoToPowerSettings();
    bool IsInPowerWhiteList { get; }
    
    Task<bool> SaveToDownloadsAsync(string filePath);
}