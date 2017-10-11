using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.Converters
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
