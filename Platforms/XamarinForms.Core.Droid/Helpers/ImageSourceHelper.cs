using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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

            if (imageSource is FileImageSource)
            {
                if (((FileImageSource)imageSource).File != null)
                    handler = new FileImageSourceHandler();
            }
            else if (imageSource is UriImageSource)
            {
                if (((UriImageSource)imageSource).Uri != null)
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