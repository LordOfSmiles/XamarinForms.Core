using System.ComponentModel;

namespace Xamarin.Core.Infrastructure.Container.Interfaces
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IReused : IFluentInterface
    {
        IOwned ReusedWithin(ReuseScope scope);
    }
}
