using System.Globalization;

namespace XamarinForms.Core.Converters.TimeConverters;

public sealed class TimeSpanConverter : GenericConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var input = (TimeSpan?)value;

        return input?.ToString(@"hh\:mm");
    }

    // public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    // {
    //     var input = value?.ToString() ?? string.Empty;
    //
    //     if (!string.IsNullOrWhiteSpace(input))
    //     {
    //         if (TimeSpan.TryParse(input,out var result))
    //         {
    //             return result;
    //         }
    //         else
    //         {
    //             return TimeSpan.Zero;
    //         }
    //     }
    //     else
    //     {
    //         return TimeSpan.Zero;
    //     }
    // }
}