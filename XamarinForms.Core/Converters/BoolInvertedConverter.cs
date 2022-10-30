using System.Globalization;

namespace XamarinForms.Core.Converters;

public sealed class BoolInvertedConverter:IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return !(bool) value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return !(bool) value;
    }
}