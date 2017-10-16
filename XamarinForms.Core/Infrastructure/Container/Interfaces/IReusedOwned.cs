using System.ComponentModel;

namespace XamarinForms.Core.Infrastructure.Container.Interfaces
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IReusedOwned : IReused, IOwned, IFluentInterface
    {
    }
}
