namespace XamarinForms.Core.Controls.Renderers;

public class CustomFrame : Frame
{
    public new static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CustomFrame),
                                                                                               typeof(CornerRadius),
                                                                                               typeof(CustomFrame),
                                                                                               new CornerRadius(0));

    public CustomFrame()
    {
        base.CornerRadius = 0;
    }

    public new CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
}