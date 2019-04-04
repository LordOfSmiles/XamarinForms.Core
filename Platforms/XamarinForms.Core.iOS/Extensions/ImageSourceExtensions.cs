using System.Threading.Tasks;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace XamarinForms.Core.iOS.Extensions
{
	public static class ImageSourceExtensions
	{
		public static IImageSourceHandler GetHandler(this ImageSource source)
		{
			IImageSourceHandler returnValue = null;

			switch (source)
			{
				case UriImageSource _:
					returnValue = new ImageLoaderSourceHandler();
					break;
				case FileImageSource _:
					returnValue = new FileImageSourceHandler();
					break;
				case StreamImageSource _:
					returnValue = new StreamImagesourceHandler();
					break;
			}

			return returnValue;
		}

		public static async Task<UIImage> GetImageAsync(this ImageSource source)
		{
			UIImage result = null;
			
			var handler = source.GetHandler();

			using (var image = await handler.LoadImageAsync(source))
			{
				if (image != null)
				{
					UIGraphics.BeginImageContextWithOptions(image.Size, false, UIScreen.MainScreen.Scale);
					image.Draw(new CGRect(0, 0, image.Size.Width, image.Size.Height));
					result = UIGraphics.GetImageFromCurrentImageContext();
					UIGraphics.EndImageContext();
				}
			}

			return result;
		}
	}
}

