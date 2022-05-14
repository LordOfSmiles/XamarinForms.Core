using System.ComponentModel;

namespace Xamarin.Core.Infrastructure.Container.Interfaces;

[EditorBrowsable(EditorBrowsableState.Never)]
public interface IRegistration : IReusedOwned
{
}

public interface IRegistration<TService> : IRegistration, IInitializable<TService>
{
}