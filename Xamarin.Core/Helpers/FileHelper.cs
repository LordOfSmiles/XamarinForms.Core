using System.IO;

namespace Xamarin.Core.Helpers;

public static class FileHelper
{
    public static Task<string> GetStringFromStreamAsync(Stream fileStream)
    {
        using var sr = new StreamReader(fileStream);
        return sr.ReadToEndAsync();
    }
}