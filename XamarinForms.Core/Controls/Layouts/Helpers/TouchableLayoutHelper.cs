using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.Controls.Layouts.Helpers;

public static class TouchableLayoutHelper
{
    public static async void ProcessTapAsync(ITouchableLayout layout)
    {
        if (layout.IsBusy)
            return;

        layout.IsBusy = true;
        
        var startColor = layout.NormalColor;

        var endColor = ColorHelper.CalculatePressedColor(startColor);
        await layout.ColorTo(endColor, 150);
        
        await Task.Delay(25);
        
        await layout.ColorTo(startColor, 50);

        layout.Command?.Execute(layout.CommandParameter);

        //для избежания повторных нажатий
        await Task.Delay(100);

        layout.IsBusy = false;
    }
}