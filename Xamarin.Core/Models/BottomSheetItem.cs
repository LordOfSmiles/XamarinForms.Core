using System.Windows.Input;

namespace Xamarin.Core.Models
{
    public sealed class BottomSheetItem
    {
        public BottomSheetItem(string title, string icon, ICommand command)
        {
            Title = title;
            Icon = icon;
            Command = command;
        }
        
        public string Title { get; set; }
        public string Icon { get; set; }
        public ICommand Command { get; set; }

        public bool WithSeparator { get; set; } = true;
    }
}