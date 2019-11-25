using System.Windows.Input;

namespace Xamarin.Core.Models
{
    public class BottomSheetItem
    {
        public BottomSheetItem(string header, string icon, ICommand command, object commandParameter)
        {
            Header = header;
            Icon = icon;
            Command = command;
            CommandParameter = commandParameter;
        }
        
        public string Header { get; }
        
        public string Icon { get; }

        public ICommand Command { get; }
        
        public object CommandParameter { get; }
    }
}