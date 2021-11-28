using System.Windows.Input;

namespace XamarinForms.Core.Infrastructure.Interfaces
{
    public interface IDraggableItem
    {
        public ICommand DragStartingCommand { get; set; }
        public ICommand DropCompletedCommand { get; set; }
        public ICommand DropCommand { get; set; }
        public ICommand DragOverCommand { get; set; }
        
        public bool CanDrop { get; }
        public bool CanDrag { get; }
    }
}