using XamarinForms.Core.Controls.Layouts;

namespace XamarinForms.Core.Controls.Selectors;

public sealed class SelectorLayout : FrameWithTap
{
    public SelectorLayout()
    {
        SetColor();
    }

    #region Bindable Proeprties

    #region IsSelected

    public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected),
                                                                                         typeof(bool),
                                                                                         typeof(SelectorControl),
                                                                                         false);

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    #endregion

    #region SelectedColor

    public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor),
                                                                                            typeof(Color),
                                                                                            typeof(SelectorControl));

    public Color SelectedColor
    {
        get => (Color)GetValue(SelectedColorProperty);
        set => SetValue(SelectedColorProperty, value);
    }

    #endregion

    #region UnselectedColor

    public static readonly BindableProperty UnselectedColorProperty = BindableProperty.Create(nameof(UnselectedColor),
                                                                                              typeof(Color),
                                                                                              typeof(SelectorControl));

    public Color UnselectedColor
    {
        get => (Color)GetValue(UnselectedColorProperty);
        set => SetValue(UnselectedColorProperty, value);
    }

    #endregion

    #region DisabledColor

    public static readonly BindableProperty DisabledColorProperty = BindableProperty.Create(nameof(DisabledColor),
                                                                                            typeof(Color),
                                                                                            typeof(SelectorControl));

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
            || propertyName == SelectedColorProperty.PropertyName
            || propertyName == UnselectedColorProperty.PropertyName
            || propertyName == IsEnabledProperty.PropertyName
            || propertyName == DisabledColorProperty.PropertyName)
        {
            SetColor();
        }

        base.OnPropertyChanged(propertyName);
    }

    #region Private Methods

    private void SetColor()
    {
        if (IsEnabled)
        {
            NormalColor = IsSelected
                              ? SelectedColor
                              : UnselectedColor;
        }
        else
        {
            NormalColor = DisabledColor;
        }
    }

    #endregion
}