using Xamarin.Core.Interfaces;

namespace Xamarin.Core.Models
{
    public class SelectableItem : NotifyObject, ISelectable, IUiListItem
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
        
        #region IUiListItem
        
        public bool IsFirst
        {
            get => _isFirst;
            set => SetProperty(ref _isFirst, value);
        }
        private bool _isFirst;

        public bool IsLast
        {
            get => _isLast;
            set => SetProperty(ref _isLast, value);
        }
        private bool _isLast;
        
        #endregion

        public SelectableItem(int id, string header, bool isSelected = false)
        {
            Id = id;
            Header = header;
            IsSelected = isSelected;
        }

        public string Header { get; protected set; }
       
    }
}