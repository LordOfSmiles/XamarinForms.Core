using System.Globalization;
using System.Linq;

namespace XamarinForms.Core.Converters;

public sealed class ToVisibilityConverter : GenericConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var isVisible = true;

        if (value != null)
        {
            isVisible = value switch
            {
                int i => i > 0,
                double d => d > 0,
                float f => f > 0,
                bool b => b,
                string s => !string.IsNullOrWhiteSpace(s),
                IEnumerable<object> enumerable => enumerable.Any(),
                _ => true
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