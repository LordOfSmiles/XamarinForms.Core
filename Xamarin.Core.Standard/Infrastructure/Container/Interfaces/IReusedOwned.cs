using System.ComponentModel;

namespace Xamarin.Core.Standard.Infrastructure.Container.Interfaces
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IReusedOwned : IReused, IOwned, IFluentInterface
    {
    }
}
