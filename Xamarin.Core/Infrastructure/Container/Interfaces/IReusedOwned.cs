using System.ComponentModel;

namespace Xamarin.Core.Infrastructure.Container.Interfaces
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IReusedOwned : IReused, IOwned
    {
    }
}
