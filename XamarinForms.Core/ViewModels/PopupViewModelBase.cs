using Xamarin.Core.Models;

namespace XamarinForms.Core.ViewModels;

public abstract class PopupViewModelBase<T> : NotifyObject
{
    public abstract T GetResult();
    
    public virtual bool IsModified { get; }
}