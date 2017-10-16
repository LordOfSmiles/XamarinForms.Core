using System.ComponentModel;

namespace XamarinForms.Core.Infrastructure.Container.Interfaces
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IOwned : IFluentInterface
    {
        void OwnedBy(Owner owner);
    }
}
