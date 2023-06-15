using System.Globalization;

namespace XamarinForms.Core.Converters.DateConverters;

public abstract class DateTimeConverterBase : GenericConverter
{
    protected abstract string DateFormat { get; }

    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var input = (DateTime?)value;
        if (input.HasValue)
        {
            return input.Value.ToString(DateFormat);
        }
        else
        {
            return string.Empty;
        }
    }
}