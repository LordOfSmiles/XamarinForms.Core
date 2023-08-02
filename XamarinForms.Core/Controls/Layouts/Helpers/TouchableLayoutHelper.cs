using XamarinForms.Core.Extensions;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.Controls.Layouts.Helpers;

public static class TouchableLayoutHelper
{
    public static async void ProcessTapAsync(ITouchableLayout layout)
    {
        if (layout.IsBusy)
            return;

        if (layout.Command == null)
            return;

        layout.IsBusy = true;

        if (layout.Command.CanExecute(layout.CommandParameter))
        {
            var view = (View)layout;

            var startColor = layout.NormalColor.IsTransparent()
                                 ? ColorHelper.FindParentRealColor(view)
                                 : layout.NormalColor;

            var endColor = ColorHelper.CalculatePressedColor(startColor);
            await view.ColorTo(endColor, 150);

            await Task.Delay(25);

            await view.ColorTo(startColor, 50);

            if (layout.NormalColor.IsTransparent())
            {
                layout.BackgroundColor = Color.Transparent;
            }

            layout.Command.Execute(layout.CommandParameter);
        }

        //для избежания повторных нажатий
        await Task.Delay(100);

        layout.IsBusy = false;
    }
}