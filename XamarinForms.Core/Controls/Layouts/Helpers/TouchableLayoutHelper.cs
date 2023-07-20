using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.Controls.Layouts.Helpers;

public static class TouchableLayoutHelper
{
    public static async void ProcessTapAsync(ITouchableLayout layout, object sender)
    {
        if (layout.IsBusy)
            return;

        layout.IsBusy = true;

        // var startColor = layout.NormalColor.IsTransparent()
        //                      ? ColorHelper.FindParentRealColor((View)sender)
        //                      : layout.NormalColor;
        
        var startColor = layout.NormalColor;

        var pressedColor = ColorHelper.CalculatePressedColor(startColor);
        
        await layout.ColorTo(pressedColor, 150);
        
        await Task.Delay(25);
        
        await layout.ColorTo(startColor, 50);
        
        // if (layout.NormalColor.IsTransparent())
        // {
        //     await layout.ColorTo(Color.Transparent, 50);
        // }
        // else
        // {
        //     await layout.ColorTo(layout.NormalColor, 50);
        // }

        layout.Command?.Execute(layout.CommandParameter);

        //для избежания повторных нажатий
        await Task.Delay(100);

        layout.IsBusy = false;
    }
}