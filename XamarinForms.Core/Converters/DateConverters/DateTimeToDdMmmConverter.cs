using System.Globalization;

namespace XamarinForms.Core.Converters.DateConverters;

public sealed class DateTimeToDdMmmConverter:DateTimeConverterBase
{
    protected override string DateFormat => "dd MMM";
}