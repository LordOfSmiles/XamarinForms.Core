namespace XamarinForms.Core.ViewModels;

public abstract class TabbedViewModelBase:ViewModelBase
{
    public bool IsActive
    {
        get => _isActive;
        set => SetProperty(ref _isActive, value);
    }
    private bool _isActive;
}