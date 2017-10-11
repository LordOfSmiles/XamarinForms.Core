using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinForms.Core.Infrastructure.FunqContainer.Interfaces
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IOwned : IFluentInterface
    {
        void OwnedBy(Owner owner);
    }
}
