using Xamarin.Core.Models;

namespace XamarinForms.Core.ViewModels;

public abstract class PopupViewModelBase:NotifyObject
{
    public virtual bool IsModified { get; }
}