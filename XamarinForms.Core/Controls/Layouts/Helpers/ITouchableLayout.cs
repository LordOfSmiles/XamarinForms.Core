namespace XamarinForms.Core.Controls.Layouts.Helpers;

public interface ITouchableLayout
{
    bool IsBusy { get; set; }
    Color NormalColor { get; }
    Color BackgroundColor { set; }
    ICommand Command { get; }
    object CommandParameter { get; }
}