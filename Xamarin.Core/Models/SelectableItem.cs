using Xamarin.Core.Interfaces;

namespace Xamarin.Core.Models
{
    public class SelectableItem :NotifyObject, ISelectable
    {
        #region ISelectable
        
        public int Id { get; }
        
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
        private bool _isSelected;

        public void SetSelected(bool isSelected)
        {
            _isSelected = isSelected;
            OnPropertyChanged(nameof(IsSelected));
        }

        #endregion

        public SelectableItem(int id, string header, bool isSelected = false)
        {
            Id = id;
            Header = header;
            IsSelected = isSelected;
        }

        public string Header { get; }
    }
}