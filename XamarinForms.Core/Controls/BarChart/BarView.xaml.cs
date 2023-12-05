namespace XamarinForms.Core.Controls.BarChart;

public partial class BarView
{
    public BarView()
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

            rowValue.Height = new GridLength(progress.filled, GridUnitType.Star);
            rowEmpty.Height = new GridLength(progress.empty, GridUnitType.Star);
        }

        base.OnPropertyChanged(propertyName);
    }
}