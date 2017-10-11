using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinForms.Core.Infrastructure.FunqContainer.Interfaces
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IRegistration : IReusedOwned, IReused, IOwned, IFluentInterface
    {
    }

    public interface IRegistration<TService> : IRegistration, IReusedOwned, IReused, IOwned, IInitializable<TService>, IFluentInterface
    {
    }
}
