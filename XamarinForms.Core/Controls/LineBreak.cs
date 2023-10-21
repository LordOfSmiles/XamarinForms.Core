namespace XamarinForms.Core.Controls;

public sealed class LineBreak:Span
{
    public LineBreak(double fontSize = 2.0)
    {
        FontSize = fontSize;
        Text = Environment.NewLine;
    }
}