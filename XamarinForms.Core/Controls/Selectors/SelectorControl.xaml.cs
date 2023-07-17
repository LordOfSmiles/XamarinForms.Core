namespace XamarinForms.Core.Controls.Selectors;

public partial class SelectorControl
{
    public SelectorControl()
    {
        InitializeComponent();
        SetColor();

        Command = TapCommand;
    }

    #region Commands

    private ICommand TapCommand => new Command(OnTap);

    private void OnTap()
    {
        IsSelected = !IsSelected;
    }

    #endregion

    #region Bindable Proeprties

    #region IsSelected

    public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected),
                                                                                         typeof(bool),
                                                                                         typeof(SelectorControl),
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
                                                                                            typeof(SelectorControl),
                                                                                            Color.Red);

    public Color SelectedColor
    {
        get => (Color)GetValue(SelectedColorProperty);
        set => SetValue(SelectedColorProperty, value);
    }

    #endregion

    #region UnselectedColor

    public static readonly BindableProperty UnselectedColorProperty = BindableProperty.Create(nameof(UnselectedColor),
                                                                                              typeof(Color),
                                                                                              typeof(SelectorControl),
                                                                                              Color.Default);

    public Color UnselectedColor
    {
        get => (Color)GetValue(UnselectedColorProperty);
        set => SetValue(UnselectedColorProperty, value);
    }

    #endregion

    #endregion

    protected override void OnPropertyChanged(string propertyName = null)
    {
        if (propertyName == IsSelectedProperty.PropertyName
            || propertyName == SelectedColorProperty.PropertyName
            || propertyName == UnselectedColorProperty.PropertyName)
        {
            SetColor();
        }

        base.OnPropertyChanged(propertyName);
    }

    #region Private Methods

    private void SetColor()
    {
        NormalColor = IsSelected
                               ? SelectedColor
                               : UnselectedColor;
    }

    #endregion
}