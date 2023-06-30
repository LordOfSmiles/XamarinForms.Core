using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.Controls.Layouts.Helpers;

public static class TouchableLayoutHelper
{
    public static async void ProcessTapAsync(ITouchableLayout layout, object sender)
    {
        if (layout.IsBusy)
            return;

        layout.IsBusy = true;

        var startColor = layout.NormalColor.IsTransparent()
                             ? ColorHelper.FindParentRealColor((View)sender)
                             : layout.NormalColor;
        
        await layout.ColorTo(ColorHelper.CalculatePressedColor(startColor), 150);
        await Task.Delay(25);

        if (layout.NormalColor.IsTransparent())
        {
            await layout.ColorTo(startColor, 50);
            layout.BackgroundColor = layout.NormalColor;
        }
        else
        {
            await layout.ColorTo(layout.NormalColor, 50);
        }

        layout.Command?.Execute(layout.CommandParameter);

        //для избежания повторных нажатий
        await Task.Delay(100);

        layout.IsBusy = false;
    }
}