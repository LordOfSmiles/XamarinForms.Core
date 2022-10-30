using System.Globalization;

namespace XamarinForms.Core.Converters;

public abstract class GenericConverter : IValueConverter
{
    public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    protected GenericConverter()
    {
        Current = this;
    }

    public static IValueConverter Current { get; private set; }
}