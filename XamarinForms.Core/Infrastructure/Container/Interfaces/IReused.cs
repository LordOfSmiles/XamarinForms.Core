using System.ComponentModel;

namespace XamarinForms.Core.Infrastructure.Container.Interfaces
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IReused : IFluentInterface
    {
        IOwned ReusedWithin(ReuseScope scope);
    }
}
