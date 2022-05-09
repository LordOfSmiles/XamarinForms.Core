using System;

namespace XamarinForms.Core.Infrastructure.Interfaces
{
    public interface ITimerUpdatable
    {
        void ForceRefresh();
        
        DateTime LastDate { get; set; }
    }
}