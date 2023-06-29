namespace XamarinForms.Core.Builders;

public sealed class SpanBuilder
{
    public static Span LineBreak(double fontSize = 2.0)
    {
        return new LineBreak(fontSize);
    }

    public static Span NewSpan(string text)
    {
        return new Span()
        {
            Text = text
        };
    }

    public Span Build()
    {
        return _span;
    }

    public SpanBuilder Bold()
    {
        _span.FontAttributes = FontAttributes.Bold;
        return this;
    }

    public SpanBuilder WithUpper()
    {
        _span.TextTransform = TextTransform.Uppercase;
        return this;
    }

    public SpanBuilder WithLower()
    {
        _span.TextTransform = TextTransform.Lowercase;
        return this;
    }

    public SpanBuilder Italic()
    {
        _span.FontAttributes = FontAttributes.Italic;
        return this;
    }

    public SpanBuilder FontSize(double fontSize)
    {
        _span.FontSize = fontSize;
        return this;
    }

    public SpanBuilder TextColor(Color lightColor, Color darkColor)
    {
        _span.SetAppThemeColor(Span.TextColorProperty, lightColor, darkColor);
        return this;
    }

    public SpanBuilder TextColor(Color color)
    {
        _span.TextColor = color;
        return this;
    }

    #region Fields

    private readonly Span _span;

    #endregion

    public SpanBuilder(string text)
    {
        _span = new Span()
        {
            Text = text
        };
    }
}

public sealed class LineBreak : Span
{
    public LineBreak(double fontSize = 2.0)
    {
        FontSize = fontSize;
        Text = Environment.NewLine;
    }
}