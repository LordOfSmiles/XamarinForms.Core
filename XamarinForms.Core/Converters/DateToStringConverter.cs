using System.Globalization;

namespace XamarinForms.Core.Converters;

public sealed class DateToStringConverter : GenericConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var date = (DateTime)value;
        var format = parameter?.ToString();

        if (string.IsNullOrWhiteSpace(format))
            format = "dd.MM.yyyy";

        return date.ToString(format);
    }
}