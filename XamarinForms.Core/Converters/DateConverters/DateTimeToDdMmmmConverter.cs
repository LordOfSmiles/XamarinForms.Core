using System.Globalization;

namespace XamarinForms.Core.Converters.DateConverters;

public sealed class DateTimeToDdMmmmConverter : DateTimeConverterBase
{
    protected override string DateFormat => "dd MMMM";
}