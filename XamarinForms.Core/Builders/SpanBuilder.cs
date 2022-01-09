using System;
using Xamarin.Forms;

namespace XamarinForms.Core.Builders
{
    public class SpanBuilder
    {
        public static Span NewLine(double fontSize = 2.0)
        {
            return new SpanBuilder(Environment.NewLine).SetFontSize(fontSize).GetSpan();
        }

        public Span GetSpan()
        {
            return _span;
        }

        public SpanBuilder WithBold()
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

        public SpanBuilder WithItalic()
        {
            _span.FontAttributes = FontAttributes.Italic;
            return this;
        }

        public SpanBuilder SetFontSize(double fontSize)
        {
            _span.FontSize = fontSize;
            return this;
        }

        public SpanBuilder SetTextColor(Color lightColor, Color darkColor)
        {
            _span.SetAppThemeColor(Span.TextColorProperty, lightColor, darkColor);
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
}