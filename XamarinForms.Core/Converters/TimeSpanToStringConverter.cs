using System.Globalization;

namespace XamarinForms.Core.Converters;

public sealed class TimeSpanToStringConverter:GenericConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var input = (TimeSpan)value;

        return input.ToString("hh:mm");
    }
}