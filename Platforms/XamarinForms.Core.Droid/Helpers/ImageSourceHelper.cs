using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace XamarinForms.Core.Droid.Helpers
{
    public static class ImageSourceHelper
    {
        public static async Task<Bitmap> GetBitmapAsync(ImageSource imageSource, Context context)
        {
            Bitmap result = null;

            IImageSourceHandler handler = null;

            if (imageSource is FileImageSource fileImageSource)
            {
                if (fileImageSource.File != null)
                    handler = new FileImageSourceHandler();
            }
            else if (imageSource is UriImageSource uriImageSource)
            {
                if (uriImageSource.Uri != null)
                    handler = new ImageLoaderSourceHandler();
            }
            else if (imageSource is StreamImageSource)
            {
                handler = new StreamImagesourceHandler();
            }

            if (handler != null)
            {
                result = await handler.LoadImageAsync(imageSource, context);
            }

            return result;
        }
    }
}