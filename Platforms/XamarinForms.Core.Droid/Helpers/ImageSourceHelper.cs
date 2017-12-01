﻿using System;
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
        public static async Task<Bitmap> GetBitmapAsync(ImageSource imageSource)
        {
            Bitmap result = null;

            IImageSourceHandler handler = null;

            if (imageSource is FileImageSource)
            {
                handler = new FileImageSourceHandler();
            }
            else if (imageSource is UriImageSource)
            {
                handler = new ImageLoaderSourceHandler();
            }
            else if (imageSource is StreamImageSource)
            {
                handler = new StreamImagesourceHandler();
            }

            if (handler != null)
            {
                result = await handler.LoadImageAsync(imageSource, null).ConfigureAwait(false);
            }

            return result;
        }
    }
}