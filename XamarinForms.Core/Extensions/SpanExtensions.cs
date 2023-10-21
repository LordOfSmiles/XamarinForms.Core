namespace XamarinForms.Core.Extensions;

public static class SpanExtensions
{
    public static Span Text(this Span span, string text)
    {
        span.Text = text;
        return span;
    }
    
    public static Span Text(this Span span, object text)
    {
        span.Text = text?.ToString() ?? string.Empty;
        return span;
    }

    public static Span Font(this Span span, double fontSize, FontAttributes attributes = FontAttributes.None)
    {
        span.FontSize = fontSize;
        span.FontAttributes = attributes;
        return span;
    }

    public static Span TextColor(this Span span, Color light, Color dark)
    {
        span.SetAppThemeColor(Span.TextColorProperty, light, dark);
        return span;
    }
}