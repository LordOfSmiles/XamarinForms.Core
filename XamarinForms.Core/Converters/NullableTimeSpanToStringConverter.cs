using System.Globalization;

namespace XamarinForms.Core.Converters;

public sealed class NullableTimeSpanToStringConverter : GenericConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var input = (TimeSpan?)value;

        return input.HasValue
                   ? input.Value.ToString("hh:mm")
                   : string.Empty;
    }
}