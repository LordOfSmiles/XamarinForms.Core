namespace XamarinForms.Core.Controls.Selectors;

public sealed class SelectorImage : TintedImage
{
    public SelectorImage()
    { }

    #region Bindable Proeprties

    #region IsSelected

    public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected),
                                                                                         typeof(bool),
                                                                                         typeof(SelectorImage),
                                                                                         false,
                                                                                         BindingMode.TwoWay);

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    #endregion

    #region SelectedColor

    public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor),
                                                                                            typeof(Color),
                                                                                            typeof(SelectorImage));

    public Color SelectedColor
    {
        get => (Color)GetValue(SelectedColorProperty);
        set => SetValue(SelectedColorProperty, value);
    }

    #endregion

    #region UnselectedColor

    public static readonly BindableProperty UnselectedColorProperty = BindableProperty.Create(nameof(UnselectedColor),
                                                                                              typeof(Color),
                                                                                              typeof(SelectorImage));

    public Color UnselectedColor
    {
        get => (Color)GetValue(UnselectedColorProperty);
        set => SetValue(UnselectedColorProperty, value);
    }

    #endregion

    #region DisabledColor

    public static readonly BindableProperty DisabledColorProperty = BindableProperty.Create(nameof(DisabledColor),
                                                                                            typeof(Color),
                                                                                            typeof(SelectorImage));

    public Color DisabledColor
    {
        get => (Color)GetValue(DisabledColorProperty);
        set => SetValue(DisabledColorProperty, value);
    }

    #endregion

    #endregion

    protected override void OnPropertyChanged(string propertyName = null)
    {
        if (propertyName is nameof(IsSelected)
                            or nameof(SelectedColor)
                            or nameof(UnselectedColor)
                            or nameof(DisabledColor)
                            or nameof(IsEnabled))
        {
            SetColors();
        }

        base.OnPropertyChanged(propertyName);
    }

    #region Private Methods

    private void SetColors()
    {
        Color color;

        if (IsEnabled)
        {
            color = IsSelected
                        ? SelectedColor
                        : UnselectedColor;
        }
        else
        {
            color = DisabledColor;
        }

        this.TintColor = color;
    }

    #endregion
}