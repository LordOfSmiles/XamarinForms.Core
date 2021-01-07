using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinForms.Core.Converters
{
    public sealed class StringCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = value?.ToString() ?? string.Empty;

            var stringParameter = parameter?.ToString() ?? string.Empty;
            if (stringParameter.Contains("l"))
            {
                result = result.ToLower();
            }
            else
            {
                result = result.ToUpper();
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static StringCaseConverter Current => _current ?? (_current = new StringCaseConverter());
        private static StringCaseConverter _current;
    }
}
