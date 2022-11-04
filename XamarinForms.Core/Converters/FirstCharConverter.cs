using System.Globalization;

namespace XamarinForms.Core.Converters;

public sealed class FirstCharConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var inputString = value.ToString() ?? string.Empty;
        if (!string.IsNullOrWhiteSpace(inputString))
        {
            return inputString.Length == 1
                       ? inputString
                       : inputString[0].ToString().ToUpper();
        }
        else
        {
            return inputString;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}