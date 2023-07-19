using Xamarin.Core.Models;

namespace XamarinForms.Core.Controls.Tabs;

public sealed class TabOption: NotifyObject
{
    public TabOption(string name)
    {
        Name = name;
    }

    public ICommand TapCommand => new Command(OnTap);

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