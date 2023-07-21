namespace XamarinForms.Core.Controls.Layouts.Helpers;

public interface ITouchableLayout
{
    bool IsBusy { get; set; }
    Color NormalColor { get; }
    ICommand Command { get; }
    object CommandParameter { get; }

    Task ColorTo(Color endColor, uint duration);
}