using Xamarin.CommunityToolkit.Markup;

namespace XamarinForms.Core.Controls.BarChart;

public sealed class BarWidget : BarWidgetBase
{
    #region Fields

    private readonly BoxView _bx;
    private readonly Grid _grd;

    #endregion

    public BarWidget()
    {
        _grd = new Grid()
        {
            RowSpacing = 2
        };

        _grd.RowDefinitions.Add(new RowDefinition());
        _grd.RowDefinitions.Add(new RowDefinition());

        _bx = new BoxView().Row(1);

        _grd.Children.Add(_bx);

        Content = _grd;
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        if (propertyName == nameof(ValueColor))
        {
            _bx.Color = ValueColor;
        }
        else if (propertyName == nameof(Value))
        {
            var progress = GetProgress();

            _grd.RowDefinitions[0].Height = new GridLength(progress.empty, GridUnitType.Star);
            _grd.RowDefinitions[1].Height = new GridLength(progress.filled, GridUnitType.Star);
        }

        base.OnPropertyChanged(propertyName);
    }
}