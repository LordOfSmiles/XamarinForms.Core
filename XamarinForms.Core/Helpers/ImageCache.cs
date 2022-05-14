using Xamarin.Forms;

namespace XamarinForms.Core.Helpers;

public static class ImageCache
{
	private static readonly Dictionary<string, ImageSource> Cache = new Dictionary<string, ImageSource>();

	public static ImageSource GetImageFromFileName(string filename)
	{
		ImageSource retVal = null;
		var hit = Cache.TryGetValue(filename, out retVal);

		if (!hit)
		{
			retVal = ImageSource.FromFile(filename);
			Cache[filename] = retVal;
		}

		return retVal;
	}

	public static ImageSource GetImageFromResource(string filename)
	{
		ImageSource retVal = null;
		var hit = Cache.TryGetValue(filename, out retVal);

		if (!hit)
		{
			retVal = ImageSource.FromResource(filename);
			Cache[filename] = retVal;
		}

		return retVal;
	}
}