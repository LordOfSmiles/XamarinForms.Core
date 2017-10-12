using System;
using System.Globalization;
using Xamarin.Forms;

namespace NewXamarinForms.Core.Converters
{
    public sealed class StringCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = "";

            if (value != null)
            {
                result = value.ToString().ToUpper();
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
