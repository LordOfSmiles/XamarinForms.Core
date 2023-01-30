namespace XamarinForms.Core.ViewModels;

public abstract class TabbedViewModelBase:ViewModelBase
{
    public bool IsTabActive
    {
        get => _isTabActive;
        set => SetProperty(ref _isTabActive, value);
    }
    private bool _isTabActive;
}