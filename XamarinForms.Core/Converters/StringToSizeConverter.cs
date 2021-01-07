using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinForms.Core.Converters
{
    public sealed class StringToSizeConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(double.TryParse(value.ToString(), out var result))
            {
     
            }
            else
            {
                if (Enum.TryParse(value.ToString(), true, out NamedSize namedSize))
                {
                    result = Device.GetNamedSize(namedSize, typeof(Label));
                }
                else
                {
                    result = Device.GetNamedSize(NamedSize.Default, typeof(Label));
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}