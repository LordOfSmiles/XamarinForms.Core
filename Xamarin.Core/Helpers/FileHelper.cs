using System.IO;

namespace Xamarin.Core.Helpers;

public static class FileHelper
{
    public static Task<string> StringFromStreamAsync(Stream fileStream)
    {
        using var sr = new StreamReader(fileStream);
        return sr.ReadToEndAsync();
    }

    public static async Task<byte[]> GetByteArrayFromStreamAsync(Stream inputStream)
    {
        byte[] result;

        if (inputStream is MemoryStream ms)
        {
            result = ms.ToArray();
        }
        else
        {
            using var memoryStream = new MemoryStream();
            await inputStream.CopyToAsync(memoryStream).ConfigureAwait(false);
            result = memoryStream.ToArray();
        }

        return result;
    }
}