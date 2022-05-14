using Xamarin.Forms;
using XamarinForms.Core.Extensions;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.Controls;

public sealed class FlatButton : Label
{
    public FlatButton()
    {
        Padding = DeviceHelper.OnPlatform(new Thickness(16,10,8,10), new Thickness(8, 12));
        BackgroundColor = Color.White;
        TextColor = Color.Black;
        VerticalTextAlignment = TextAlignment.Center;
        LineBreakMode = LineBreakMode.NoWrap;

        var gesture = new TapGestureRecognizer();
        gesture.Tapped += GestureOnTapped;
        GestureRecognizers.Add(gesture);
    }

    #region Bindable Properties

    #region Command

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(FlatButton));

    public ICommand Command
    {
        get => (ICommand) GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    #endregion

    #region CommandParameter

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(FlatButton));

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    #endregion

    #endregion

    #region Handlers

    private async void GestureOnTapped(object sender, EventArgs e)
    {
        if (BackgroundColor.Equals(Color.Transparent) || BackgroundColor.IsDefault)
        {
            Command?.Execute(CommandParameter);
        }
        else
        {
            var startColor = BackgroundColor;
            var endColor = startColor.MultiplyAlpha(0.7);

            await this.ColorTo(endColor, 100);
            Command?.Execute(CommandParameter);
            await this.ColorTo(startColor, 100);
        }

        #endregion
    }
}