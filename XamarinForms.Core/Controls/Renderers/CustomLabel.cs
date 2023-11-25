namespace XamarinForms.Core.Controls.Renderers;

public class CustomLabel : Label
{
    public static readonly BindableProperty FontWeightProperty = BindableProperty.Create(nameof(FontWeight),
                                                                                         typeof(FontWeightTypeEnum),
                                                                                         typeof(CustomLabel),
                                                                                         FontWeightTypeEnum.Regular);

    public FontWeightTypeEnum FontWeight
    {
        get => (FontWeightTypeEnum)GetValue(FontWeightProperty);
        set => SetValue(FontWeightProperty, value);
    }
}

public enum FontWeightTypeEnum
{
    Regular,
    Light,
    Medium,
}