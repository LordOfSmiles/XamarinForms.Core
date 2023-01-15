using System.Globalization;

namespace XamarinForms.Core.Converters.DateConverters;

public sealed class DateTimeToDdMmmConverter:GenericConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var input = (DateTime?)value;

        return input?.ToString("dd MMM");
    }
}