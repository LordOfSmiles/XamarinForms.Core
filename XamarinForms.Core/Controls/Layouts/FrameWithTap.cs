using XamarinForms.Core.Controls.Layouts.Helpers;

namespace XamarinForms.Core.Controls.Layouts;

public class FrameWithTap : Border, ITouchableLayout
{
    public FrameWithTap()
    {
        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += (_, _) => TouchableLayoutHelper.ProcessTapAsync(this);
        GestureRecognizers.Add(tapGesture);

        BackgroundColor = NormalColor;
    }

    #region Bindable Properties

    #region NormalColor

    public static readonly BindableProperty NormalColorProperty = BindableProperty.Create(nameof(NormalColor),
                                                                                          typeof(Color),
                                                                                          typeof(FrameWithTap),
                                                                                          Color.Transparent);

    public Color NormalColor
    {
        get => (Color)GetValue(NormalColorProperty);
        set => SetValue(NormalColorProperty, value);
    }

    #endregion

    #region Command

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command),
                                                                                      typeof(ICommand),
                                                                                      typeof(FrameWithTap));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    #endregion

    #region CommandParameter

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter),
                                                                                               typeof(object),
                                                                                               typeof(FrameWithTap));

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    #endregion

    #endregion

    protected override void OnPropertyChanged(string propertyName = null)
    {
        if (propertyName == nameof(NormalColor))
        {
            BackgroundColor = NormalColor;
        }

        base.OnPropertyChanged(propertyName);
    }

    #region ITouchableLayout

    public bool IsBusy { get; set; }

    #endregion
}