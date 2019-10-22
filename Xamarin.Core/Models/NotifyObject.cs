using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Xamarin.Core.Models
{
    public abstract class NotifyObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(oldValue, newValue)) 
                return;
            
            oldValue = newValue;
            OnPropertyChanged(propertyName);
        }
    }
}

