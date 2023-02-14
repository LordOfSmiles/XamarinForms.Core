namespace XamarinForms.Core.PlatformServices;

public interface IDroidFileSaver
{
    Task<bool> SaveToDownloadsAsync(string filePath);
}