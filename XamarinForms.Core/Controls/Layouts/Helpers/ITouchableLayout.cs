namespace XamarinForms.Core.Controls.Layouts.Helpers;

public interface ITouchableLayout
{
    bool IsBusy { get; set; }
    Color NormalColor { get; set; }
    Color BackgroundColor { get; set; }
    ICommand Command { get; set; }
    object CommandParameter { get; set; }

    Task ColorTo(Color endColor, uint duration);
}