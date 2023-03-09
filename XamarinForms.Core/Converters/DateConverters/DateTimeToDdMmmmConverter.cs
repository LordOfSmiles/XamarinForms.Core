using System.Globalization;
using Xamarin.Core;

namespace XamarinForms.Core.Converters.DateConverters;

public sealed class DateTimeToDdMmmmConverter : DateTimeConverterBase
{
    protected override string DateFormat => DateFormats.dd_MMMM;
}