using System.IO;
using System.Threading.Tasks;

namespace XamarinForms.Core.Standard.Helpers
{
    public static class ImageHelper
    {
        public static async Task<byte[]> ImageToBytesAsync(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                await input.CopyToAsync(ms).ConfigureAwait(false);
                return ms.ToArray();
            }
        }

        public static Stream BytesToStream(byte[] image)
        {
            return new MemoryStream(image);
        }
    }
}

