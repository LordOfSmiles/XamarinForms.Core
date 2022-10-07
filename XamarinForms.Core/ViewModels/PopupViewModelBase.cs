using Xamarin.Core.Models;

namespace XamarinForms.Core.ViewModels;

public abstract class PopupViewModelBase<T> : NotifyObject
{
    public abstract T GetResult();

    public virtual bool IsModified { get; }

    public bool InputTransparent
    {
        get => _inputTransparent;
        protected set => SetProperty(ref _inputTransparent, value);
    }
    private bool _inputTransparent;
}