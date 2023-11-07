namespace XamarinForms.Core.Controls.Selectors;

public sealed class SelectorLabel : Label
{
    public SelectorLabel()
    {
        LineBreakMode = LineBreakMode.NoWrap;
        
        SetColors();
    }

    #region Bindable Proeprties

    #region IsSelected

    public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected),
                                                                                         typeof(bool),
                                                                                         typeof(SelectorLabel),
                                                                                         false,
                                                                                         BindingMode.TwoWay);

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    #endregion

    #region SelectedColor

    public static readonly BindableProperty SelectedTextColorProperty = BindableProperty.Create(nameof(SelectedTextColor),
                                                                                                typeof(Color),
                                                                                                typeof(SelectorLabel));

    public Color SelectedTextColor
    {
        get => (Color)GetValue(SelectedTextColorProperty);
        set => SetValue(SelectedTextColorProperty, value);
    }

    #endregion

    #region UnselectedColor

    public static readonly BindableProperty UnselectedTextColorProperty = BindableProperty.Create(nameof(UnselectedTextColor),
                                                                                                  typeof(Color),
                                                                                                  typeof(SelectorLabel));

    public Color UnselectedTextColor
    {
        get => (Color)GetValue(UnselectedTextColorProperty);
        set => SetValue(UnselectedTextColorProperty, value);
    }

    #endregion

    #region DisabledColor

    public static readonly BindableProperty DisabledColorProperty = BindableProperty.Create(nameof(DisabledColor),
                                                                                            typeof(Color),
                                                                                            typeof(SelectorLabel));

    public Color DisabledColor
    {
        get => (Color)GetValue(DisabledColorProperty);
        set => SetValue(DisabledColorProperty, value);
    }

    #endregion

    #endregion

    protected override void OnPropertyChanged(string propertyName = null)
    {
        if (propertyName == IsSelectedProperty.PropertyName
            || propertyName == SelectedTextColorProperty.PropertyName
            || propertyName == UnselectedTextColorProperty.PropertyName
            || propertyName == DisabledColorProperty.PropertyName
            || propertyName == IsEnabledProperty.PropertyName)
        {
            SetColors();
        }

        base.OnPropertyChanged(propertyName);
    }

    #region Private Methods

    private void SetColors()
    {
        if (IsEnabled)
        {
            TextColor = IsSelected
                            ? SelectedTextColor
                            : UnselectedTextColor;
        }
        else
        {
            TextColor = DisabledColor;
        }
    }

    #endregion
}