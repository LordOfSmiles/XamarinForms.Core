using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinForms.Core.Infrastructure.FunqContainer.Interfaces
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IInitializable<TService> : IFluentInterface
    {
        IReusedOwned InitializedBy(Action<Container, TService> initializer);
    }
}
