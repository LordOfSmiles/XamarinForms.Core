using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace XamarinForms.Core.Converters
{
    public sealed class ToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isVisible = true;

            if (value != null)
            {
                switch (value)
                {
                    case int i:
                        isVisible = i > 0;
                        break;
                    case bool b:
                        isVisible = b;
                        break;
                    case string s:
                        isVisible = !string.IsNullOrEmpty(s);
                        break;
                    case IEnumerable<object> enumerable:
                        isVisible = enumerable.Any();
                        break;
                }
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static ToVisibilityConverter Current => _current ?? (_current = new ToVisibilityConverter());
        private static ToVisibilityConverter _current;
    }
}
