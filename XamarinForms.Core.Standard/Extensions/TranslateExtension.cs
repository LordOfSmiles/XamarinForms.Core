using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinForms.Core.Standard.Extensions
{
    //[ContentProperty("Text")]
    //public class TranslateExtension : IMarkupExtension
    //{
    //    private readonly CultureInfo _cultureInfo;
    //    private const string ResourceId = "XFLocalization.Resources.AppResources";
    //    private static readonly Lazy<ResourceManager> ResourceManager = new Lazy<ResourceManager>(() => new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly));

    //    public string Text { get; set; }

    //    public TranslateExtension()
    //    {
    //        _cultureInfo = CultureInfo.CurrentUICulture;
    //    }

    //    public object ProvideValue(IServiceProvider serviceProvider)
    //    {
    //        if (Text == null)
    //            return string.Empty;

    //        try
    //        {
    //            var translation = ResourceManager.Value.GetString(Text, _cultureInfo);
    //            return translation ?? Text;
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine(e);
    //            return Text;
    //        }
    //    }
    //}
}
