using Xamarin.Core.Models;
using XamarinForms.Core.Helpers;

namespace XamarinForms.Core.Controls.Tabs;

public sealed class TabOption: NotifyObject
{
    public TabOption(string name)
    {
        Name = name;
    }

    public ICommand TapCommand => CommandHelper.Create(OnTap);

    private void OnTap()
    {
        IsSelected = true;
    }
        
    public string Name { get; }
        
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            _isSelected = value;
            OnPropertyChanged();
        } 
    }
    private bool _isSelected;
}