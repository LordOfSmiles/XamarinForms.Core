using Xamarin.Core.Models;

namespace XamarinForms.Core.ViewModels;

public abstract class PopupViewModelBase<T> : NotifyObject
{
    public abstract T GetResult();
    
    protected T Original { get; set; }

    public abstract bool IsModified { get; }

    public bool InputTransparent
    {
        get => _inputTransparent;
        protected set => SetProperty(ref _inputTransparent, value);
    }
    private bool _inputTransparent;

    public bool HasKeyboardOffset
    {
        get => _keyboardHasKeyboardOffset;
        set => SetProperty(ref _keyboardHasKeyboardOffset, value);
    }
    private bool _keyboardHasKeyboardOffset;
}