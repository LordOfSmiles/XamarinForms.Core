using System.Globalization;
using Xamarin.Forms;

namespace XamarinForms.Core.Converters;

public sealed class DateToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var date = (DateTime)value;
        var format = parameter?.ToString();

        if (string.IsNullOrWhiteSpace(format))
            format = "dd.MM.yyyy";

        return date.ToString(format);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}