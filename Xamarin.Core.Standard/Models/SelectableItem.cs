namespace Xamarin.Core.Standard.Models
{
    public sealed class SelectableItem
    {
        public SelectableItem(int id, string header)
        {
            Id = id;
            Header = header;
        }
        
        public int Id { get; }
        
        public string Header { get; }
    }
}