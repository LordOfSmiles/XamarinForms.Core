using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinForms.Core.Infrastructure.FunqContainer.Interfaces;

namespace XamarinForms.Core.Infrastructure.FunqContainer
{
    internal class ServiceEntry : IRegistration, IReusedOwned, IReused, IOwned, IFluentInterface
    {
        public Owner Owner;
        public ReuseScope Reuse;
        public Container Container;

        protected ServiceEntry()
        {
        }

        public void OwnedBy(Owner owner)
        {
            this.Owner = owner;
        }

        public IOwned ReusedWithin(ReuseScope scope)
        {
            this.Reuse = scope;
            return this;
        }

        Type IFluentInterface.GetType()
        {
            return this.GetType();
        }
    }

    internal sealed class ServiceEntry<TService, TFunc> : ServiceEntry, IRegistration<TService>, IRegistration, IReusedOwned, IReused, IOwned, IInitializable<TService>, IFluentInterface
    {
        public TFunc Factory;
        internal TService Instance;
        internal Action<Container, TService> Initializer;

        public ServiceEntry(TFunc factory)
        {
            this.Factory = factory;
        }

        internal void InitializeInstance(TService instance)
        {
            if (this.Reuse != ReuseScope.None)
                this.Instance = instance;
            if (this.Owner == Owner.Container && (object)instance is IDisposable)
                this.Container.TrackDisposable(instance);
            if (this.Initializer == null)
                return;
            this.Initializer(this.Container, instance);
        }

        public IReusedOwned InitializedBy(Action<Container, TService> initializer)
        {
            this.Initializer = initializer;
            return this;
        }

        public ServiceEntry<TService, TFunc> CloneFor(Container newContainer)
        {
            ServiceEntry<TService, TFunc> serviceEntry = new ServiceEntry<TService, TFunc>(this.Factory);
            serviceEntry.Owner = this.Owner;
            serviceEntry.Reuse = this.Reuse;
            serviceEntry.Container = newContainer;
            serviceEntry.Initializer = this.Initializer;
            return serviceEntry;
        }
    }
}
