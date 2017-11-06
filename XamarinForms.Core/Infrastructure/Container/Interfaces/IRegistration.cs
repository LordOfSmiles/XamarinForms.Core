using System.ComponentModel;

namespace Xamarin.Core.Infrastructure.Container.Interfaces
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IRegistration : IReusedOwned, IReused, IOwned, IFluentInterface
    {
    }

    public interface IRegistration<TService> : IRegistration, IReusedOwned, IReused, IOwned, IInitializable<TService>, IFluentInterface
    {
    }
}
