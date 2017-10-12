using System;
using System.Globalization;
using NewXamarinForms.Core.Helpers;
using Xamarin.Forms;

namespace NewXamarinForms.Core.Converters
{
    public sealed class BytesToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource result = null;

            var bytes = value as byte[];
            if (bytes != null)
            {
                result = ImageSource.FromStream(() => ImageHelper.BytesToStream(bytes));
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
