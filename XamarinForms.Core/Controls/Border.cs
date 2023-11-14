using XamarinForms.Core.Controls.Renderers;

namespace XamarinForms.Core.Controls;

public class Border : CustomFrame
{
    public Border()
    {
        HasShadow = false;
        Padding = new Thickness(0);
        IsClippedToBounds = true;
        BackgroundColor = Color.Transparent;
    }
}

public class BorderOld : Frame
{
    public BorderOld()
    {
        CornerRadius = 0;
        HasShadow = false;
        Padding = new Thickness(0);
        IsClippedToBounds = true;
        BackgroundColor = Color.Transparent;
    }
}