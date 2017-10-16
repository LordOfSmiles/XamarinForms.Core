using System;
using System.ComponentModel;

namespace XamarinForms.Core.Infrastructure.Container.Interfaces
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IInitializable<TService> : IFluentInterface
    {
        IReusedOwned InitializedBy(Action<Container, TService> initializer);
    }
}
