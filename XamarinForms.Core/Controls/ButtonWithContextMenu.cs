using XamarinForms.Core.Infrastructure.ContextMenu;

namespace XamarinForms.Core.Controls;

public sealed class ButtonWithContextMenu : Button
{
    #region ContextMenu

    public static readonly BindableProperty ContextMenuProperty = BindableProperty.Create(nameof(ContextMenu),
                                                                                          typeof(ContextMenuContainer),
                                                                                          typeof(ButtonWithContextMenu));

    public ContextMenuContainer ContextMenu
    {
        get => (ContextMenuContainer)GetValue(ContextMenuProperty);
        set => SetValue(ContextMenuProperty, value);
    }

    #endregion
}