namespace XamarinForms.Core.Controls.BarChart;

public partial class HorizontalBarView
{
    public HorizontalBarView()
    {
        InitializeComponent();
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        if (propertyName == nameof(ValueColor))
        {
            bxValue.Color = ValueColor;
        }
        else if (propertyName == nameof(Value))
        {
            var progress = GetProgress();

            columnFilled.Width = new GridLength(progress.filled, GridUnitType.Star);
            columnEmpty.Width = new GridLength(progress.empty, GridUnitType.Star);
        }

        base.OnPropertyChanged(propertyName);
    }
}