namespace XamarinForms.Core.Controls.BarChart;

public abstract class BarWidgetBase : BorderOld
{
    #region Value

    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value),
                                                                                    typeof(BarViewDataModel),
                                                                                    typeof(BarWidgetBase));

    public BarViewDataModel Value
    {
        get => (BarViewDataModel)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    #endregion

    #region Color

    public static readonly BindableProperty ValueColorProperty = BindableProperty.Create(nameof(ValueColor),
                                                                                         typeof(Color),
                                                                                         typeof(BarWidgetBase));

    public Color ValueColor
    {
        get => (Color)GetValue(ValueColorProperty);
        set => SetValue(ValueColorProperty, value);
    }

    #endregion

    protected (double filled, double empty) GetProgress()
    {
        if (Value != null)
        {
            double emptySpace;
            double filledSpace;

            if (Value.Max > 0)
            {
                filledSpace = Value.Value / Value.Max;
                emptySpace = 1 - filledSpace;
            }
            else
            {
                emptySpace = 1;
                filledSpace = 0;
            }

            if (emptySpace < 0)
                emptySpace = 0;

            if (filledSpace < 0)
                filledSpace = 0;

            return (filledSpace, emptySpace);
        }
        else
        {
            return (0, 1);
        }
    }
}

public sealed record BarViewDataModel(double Value, double Max)
{
    public double Max { get; } = Max;
    public double Value { get; } = Value;
}