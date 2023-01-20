using System.Globalization;

namespace XamarinForms.Core.Converters.DateConverters;

public sealed class DateTimeToDdmmmmyyyyConverter:DateTimeConverterBase
{
    protected override string DateFormat => "dd MMMM yyyy";
}