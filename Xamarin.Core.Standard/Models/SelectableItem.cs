namespace Xamarin.Core.Standard.Models
{
    public sealed class SelectableItem:NotifyObject
    {
        public SelectableItem(int id, string header)
            : this(id, header, false)
        {

        }

        public SelectableItem(int id, string header, bool isLastItem)
        {
            Id = id;
            Header = header;
            IsLastItem = isLastItem;
        }
        
        public int Id { get; }
        public string Header { get; }
        public bool IsLastItem { get; }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
        private bool _isSelected;
    }
}