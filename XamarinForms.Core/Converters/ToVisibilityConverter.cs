using System.Globalization;

namespace XamarinForms.Core.Converters;

public sealed class ToVisibilityConverter : GenericConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool isVisible;

        if (value != null)
        {
            isVisible = value switch
            {
                int intValue                    => intValue > 0,
                double doubleValue              => doubleValue > 0,
                float floatValue                => floatValue > 0,
                bool boolValue                  => boolValue,
                string stringValue              => !string.IsNullOrWhiteSpace(stringValue),
                IEnumerable<object> enumerable  => enumerable.Any(),
                FormattedString formattedString => formattedString.Spans.Any(),
                _                               => true
            };
        }
        else
        {
            isVisible = false;
        }

        var stringParameter = parameter is string
                                  ? parameter.ToString()
                                  : string.Empty;

        if (stringParameter.Contains("i"))
        {
            isVisible = !isVisible;
        }

        return isVisible;
    }
}