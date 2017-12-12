using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace XamarinForms.Core.Standard.Converters
{
    public sealed class ToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isVisible = true;

            if (value != null)
            {
                if (value is bool)
                {
                    isVisible = (bool)value;
                }
                else if (value is string)
                {
                    isVisible = !string.IsNullOrEmpty(value.ToString());
                }
                else if (value is IEnumerable<object>)
                {
                    isVisible = ((IEnumerable<object>)value).Any();
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
                isVisible = !isVisible;

            return isVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
