namespace XamarinForms.Core.Controls.BarChart;

public partial class BarView
{
    public BarView()
    {
        InitializeComponent();
    }

    #region Value

    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value),
        typeof(BarViewDataModel),
        typeof(BarView),
        propertyChanged: OnValueChanged);

    public BarViewDataModel Value
    {
        get => (BarViewDataModel)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (BarView)bindable;

        var data = (BarViewDataModel)newValue;

        if (data != null)
        {
            double emptySpace;
            double filledSpace;

            if (data.Max > 0)
            {
                filledSpace = data.Value / data.Max;
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

            ctrl.rowEmpty.Height = new GridLength(emptySpace, GridUnitType.Star);
            ctrl.rowValue.Height = new GridLength(filledSpace, GridUnitType.Star);
        }
        else
        {
            ctrl.rowEmpty.Height = new GridLength(1, GridUnitType.Star);
            ctrl.rowValue.Height = new GridLength(0, GridUnitType.Star);
        }
    }

    #endregion

    #region Color

    public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(ValueColor),
        typeof(Color),
        typeof(BarView));

    public Color ValueColor
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    #endregion
}

public sealed record BarViewDataModel(double Value, double Max)
{
    public double Max { get; } = Max;
    public double Value { get; } = Value;
}