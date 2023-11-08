namespace XamarinForms.Core.Controls.Renderers;

public class CustomLabel : Label
{
    public FontWeightTypeEnum FontWeight { get; set; } = FontWeightTypeEnum.Regular;
}

public enum FontWeightTypeEnum
{
    Regular,
    Light,
    Medium,
    Bold
}