using System.Globalization;

namespace XamarinForms.Core.Converters;

public sealed class StringCaseConverter : GenericConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var result = value?.ToString() ?? string.Empty;

        var stringParameter = parameter?.ToString() ?? string.Empty;
        result = stringParameter.Contains("l")
                     ? result.ToLower()
                     : result.ToUpper();

        return result;
    }
}