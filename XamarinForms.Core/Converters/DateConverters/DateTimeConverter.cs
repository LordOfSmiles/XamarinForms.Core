using System.Globalization;

namespace XamarinForms.Core.Converters.DateConverters;

public sealed class DateTimeConverter : GenericConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var input = (DateTime?)value;
        if (input.HasValue)
        {
            var format = parameter?.ToString() ?? "dd.MM.yyyy";

            return input.Value.ToString(format);
        }
        else
        {
            return string.Empty;
        }
    }
}