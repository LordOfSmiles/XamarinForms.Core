using Xamarin.CommunityToolkit.Effects;
using XamarinForms.Core.Helpers;
using XamarinForms.Core.Infrastructure.ContextMenu;

namespace XamarinForms.Core.Controls;

// public sealed class ContentViewWithContextMenu : TouchableContentView, IControlWithContextMenu
// {
//     #region Bindable Properties
//
//     #region ContextMenu
//
//     public static readonly BindableProperty ContextMenuProperty = BindableProperty.Create(nameof(ContextMenu),
//                                                                                           typeof(ContextMenuContainer),
//                                                                                           typeof(ContentViewWithContextMenu));
//
//     public ContextMenuContainer ContextMenu
//     {
//         get => (ContextMenuContainer)GetValue(ContextMenuProperty);
//         set => SetValue(ContextMenuProperty, value);
//     }
//
//     #endregion
//
//     #region TapCommand
//
//     public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(nameof(TapCommand),
//                                                                                          typeof(ICommand),
//                                                                                          typeof(ContentViewWithContextMenu),
//                                                                                          propertyChanged: OnTapCommandChanged);
//
//     public ICommand TapCommand
//     {
//         get => (ICommand)GetValue(TapCommandProperty);
//         set => SetValue(TapCommandProperty, value);
//     }
//
//     private static void OnTapCommandChanged(BindableObject bindable, object oldValue, object newValue)
//     {
//         if (DeviceHelper.IsIos
//             && VersionHelper.IsEqualOrGreater(13))
//             return;
//
//         var ctrl = (ContentViewWithContextMenu)bindable;
//         TouchEffect.SetCommand(ctrl, (ICommand)newValue);
//         TouchEffect.SetCommandParameter(ctrl, ctrl.BindingContext);
//     }
//
//     #endregion
//
//     #endregion
// }
//
// public sealed class GridWithContextMenu : TouchableGrid, IControlWithContextMenu
// {
//     #region Bindable Properties
//
//     #region ContextMenu
//
//     public static readonly BindableProperty ContextMenuProperty = BindableProperty.Create(nameof(ContextMenu),
//                                                                                           typeof(ContextMenuContainer),
//                                                                                           typeof(GridWithContextMenu));
//
//     public ContextMenuContainer ContextMenu
//     {
//         get => (ContextMenuContainer)GetValue(ContextMenuProperty);
//         set => SetValue(ContextMenuProperty, value);
//     }
//
//     #endregion
//
//     #region TapCommand
//
//     public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(nameof(TapCommand),
//                                                                                          typeof(ICommand),
//                                                                                          typeof(GridWithContextMenu),
//                                                                                          propertyChanged: OnTapCommandChanged);
//
//     public ICommand TapCommand
//     {
//         get => (ICommand)GetValue(TapCommandProperty);
//         set => SetValue(TapCommandProperty, value);
//     }
//
//     private static void OnTapCommandChanged(BindableObject bindable, object oldValue, object newValue)
//     {
//         if (DeviceHelper.IsIos
//             && VersionHelper.IsEqualOrGreater(13))
//             return;
//
//         var ctrl = (GridWithContextMenu)bindable;
//         TouchEffect.SetCommand(ctrl, (ICommand)newValue);
//         TouchEffect.SetCommandParameter(ctrl, ctrl.BindingContext);
//     }
//
//     #endregion
//
//     #endregion
// }
//
// public sealed class StackLayoutWithContextMenu : TouchableStackLayout, IControlWithContextMenu
// {
//     #region Bindable Properties
//
//     #region ContextMenu
//
//     public static readonly BindableProperty ContextMenuProperty = BindableProperty.Create(nameof(ContextMenu),
//                                                                                           typeof(ContextMenuContainer),
//                                                                                           typeof(StackLayoutWithContextMenu));
//
//     public ContextMenuContainer ContextMenu
//     {
//         get => (ContextMenuContainer)GetValue(ContextMenuProperty);
//         set => SetValue(ContextMenuProperty, value);
//     }
//
//     #endregion
//
//     #region TapCommand
//
//     public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(nameof(TapCommand),
//                                                                                          typeof(ICommand),
//                                                                                          typeof(StackLayoutWithContextMenu),
//                                                                                          propertyChanged: OnTapCommandChanged);
//
//     public ICommand TapCommand
//     {
//         get => (ICommand)GetValue(TapCommandProperty);
//         set => SetValue(TapCommandProperty, value);
//     }
//
//     private static void OnTapCommandChanged(BindableObject bindable, object oldValue, object newValue)
//     {
//         if (DeviceHelper.IsIos
//             && VersionHelper.IsEqualOrGreater(13))
//             return;
//
//         var ctrl = (StackLayoutWithContextMenu)bindable;
//         TouchEffect.SetCommand(ctrl, (ICommand)newValue);
//         TouchEffect.SetCommandParameter(ctrl, ctrl.BindingContext);
//     }
//
//     #endregion
//
//     #endregion
// }
//
// public interface IControlWithContextMenu
// {
//     ContextMenuContainer ContextMenu { get; set; }
// }