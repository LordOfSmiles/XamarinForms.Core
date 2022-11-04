using System.Globalization;

namespace XamarinForms.Core.Converters;

public sealed class NullableToFontAttributesConverter:GenericConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value != null
                   ? FontAttributes.Bold
                   : FontAttributes.None;
    }
}