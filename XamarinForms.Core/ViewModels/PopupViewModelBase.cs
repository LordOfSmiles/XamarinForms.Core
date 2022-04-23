using Xamarin.Core.Models;

namespace XamarinForms.Core.ViewModels
{
    public abstract class PopupViewModelBase:NotifyObject
    {

        public bool IsConfirmed
        {
            get => _isConfirmed;
            protected set => SetProperty(ref _isConfirmed, value);
        }
        private bool _isConfirmed;

        public virtual bool IsValid { get; }
    }
}