using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class FrameWithContextMenu
{
    public FrameWithContextMenu()
    {
        InitializeComponent();
    }

    #region Bindable Properties

    #region PressedColor

    public static readonly BindableProperty PressedColorProperty = BindableProperty.Create(nameof(PressedColor),
                                                                                           typeof(Color),
                                                                                           typeof(FrameWithContextMenu));

    public Color PressedColor
    {
        get => (Color)GetValue(PressedColorProperty);
        set => SetValue(PressedColorProperty, value);
    }

    #endregion

    #region NormalColor

    public static readonly BindableProperty NormalColorProperty = BindableProperty.Create(nameof(NormalColor),
                                                                                          typeof(Color),
                                                                                          typeof(FrameWithContextMenu));

    public Color NormalColor
    {
        get => (Color)GetValue(NormalColorProperty);
        set => SetValue(NormalColorProperty, value);
    }

    #endregion

    #endregion

    #region ContextMenu

    public static readonly BindableProperty ContextMenuProperty = BindableProperty.Create(nameof(ContextMenu),
                                                                                          typeof(ContextMenuContainer),
                                                                                          typeof(FrameWithContextMenu));

    public ContextMenuContainer ContextMenu
    {
        get => (ContextMenuContainer)GetValue(ContextMenuProperty);
        set => SetValue(ContextMenuProperty, value);
    }

    #endregion

    #region TapCommand

    public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(nameof(TapCommand),
                                                                                         typeof(ICommand),
                                                                                         typeof(FrameWithContextMenu),
                                                                                         propertyChanged: OnTapCommandChanged);

    public ICommand TapCommand
    {
        get => (ICommand)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }

    private static void OnTapCommandChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (Device.RuntimePlatform == Device.iOS
            && VersionHelper.IsEqualOrGreater(13))
            return;

        var ctrl = (FrameWithContextMenu)bindable;
        TouchEffect.SetCommand(ctrl.Content, (ICommand)newValue);
        TouchEffect.SetCommandParameter(ctrl.Content, ctrl.BindingContext);
    }

    #endregion
}

public sealed class ContextMenuContainer
{
    public string Text { get; set; }

    public IReadOnlyCollection<ContextMenuItemInfo> MenuItems { get; set; }
}

public sealed class ContextMenuItemInfo
{
    public ContextMenuItemInfo(string text)
    {
        Text = text;
    }

    public string Text { get; }

    public string IconForApple { get; set; }

    public bool IsDestructive { get; set; }

    public int Group { get; set; }

    public Func<Task> Action { get; set; }
}