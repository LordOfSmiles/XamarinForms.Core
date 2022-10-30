namespace XamarinForms.Core.Helpers;

public static class ImageCache
{
	private static readonly Dictionary<string, ImageSource> Cache = new Dictionary<string, ImageSource>();

	public static ImageSource GetImageFromFileName(string filename)
	{
		var hit = Cache.TryGetValue(filename, out var result);

		if (!hit)
		{
			result = ImageSource.FromFile(filename);
			Cache[filename] = result;
		}

		return result;
	}

	public static ImageSource GetImageFromResource(string filename)
	{
		var hit = Cache.TryGetValue(filename, out var result);

		if (!hit)
		{
			result = ImageSource.FromResource(filename);
			Cache[filename] = result;
		}

		return result;
	}
}