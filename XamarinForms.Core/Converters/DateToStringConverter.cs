using System.Globalization;

namespace XamarinForms.Core.Converters;

public sealed class DateToStringConverter : GenericConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var input = (DateTime)value;

        var format = parameter?.ToString() ?? "dd MMM";

        return input.ToString(format);
    }
}