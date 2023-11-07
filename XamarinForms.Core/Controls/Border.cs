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