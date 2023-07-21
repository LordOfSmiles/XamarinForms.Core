using XamarinForms.Core.Controls.Layouts.Helpers;
using XamarinForms.Core.Extensions;
using XamarinForms.Core.Helpers;
using ViewExtensions = XamarinForms.Core.Extensions.ViewExtensions;

namespace XamarinForms.Core.Controls.Layouts;

public class TouchableContentView : ContentView,ITouchableLayout
{
    public TouchableContentView()
    {
        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += (_, _) => TouchableLayoutHelper.ProcessTapAsync(this);
        GestureRecognizers.Add(tapGesture);
    }

    #region Bindable Properties

    #region NormalColor

    public static readonly BindableProperty NormalColorProperty = BindableProperty.Create(nameof(NormalColor),
                                                                                          typeof(Color),
                                                                                          typeof(TouchableContentView),
                                                                                          propertyChanged: OnDefaultColorChanged);

    public Color NormalColor
    {
        get => (Color)GetValue(NormalColorProperty);
        set => SetValue(NormalColorProperty, value);
    }

    private static void OnDefaultColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var ctrl = (TouchableContentView)bindable;

        ctrl.BackgroundColor = (Color)newValue;
    }

    #endregion

    #region Command

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command),
                                                                                      typeof(ICommand),
                                                                                      typeof(TouchableContentView));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    #endregion

    #region CommandParameter

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter),
                                                                                               typeof(object),
                                                                                               typeof(TouchableContentView));

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    #endregion

    #endregion

    #region ITouchableLayout

    public bool IsBusy { get; set; }
    
    public Task ColorTo(Color endColor, uint duration) => ViewExtensions.ColorTo(this, endColor, duration);

    #endregion
}