using System.Windows.Input;
using XamarinForms.Core.Infrastructure.Interfaces;

namespace XamarinForms.Core.Controls.BottomSheets.Models
{
    public abstract class BottomSheetItemBase:IUiListItem
    {
        #region IUiListItem
        
        public bool IsFirst { get; set; }
        public bool IsLast { get; set; }
        
        #endregion

        protected BottomSheetItemBase(string text, bool isDestructive = false)
        {
            Text = text;
            IsDestructive = isDestructive;
        }

        public string Text { get; }
        public bool IsDestructive { get; }
        
        public bool IsSecondary { get; set; }
        
        
        public ICommand Command { get; set; }
        public object CommandParameter { get; set; }
       
    }
}