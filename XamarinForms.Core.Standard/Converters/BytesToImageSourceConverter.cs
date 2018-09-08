using System;
using System.Globalization;
using Xamarin.Forms;
using XamarinForms.Core.Standard.Helpers;

namespace XamarinForms.Core.Standard.Converters
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

        public static BytesToImageSourceConverter Current => _current ?? (_current = new BytesToImageSourceConverter());
        private static BytesToImageSourceConverter _current;
    }
}
