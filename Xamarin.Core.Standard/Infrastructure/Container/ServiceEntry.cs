using System;
using Xamarin.Core.Standard.Infrastructure.Container.Interfaces;

namespace Xamarin.Core.Standard.Infrastructure.Container
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
            Owner = owner;
        }

        public IOwned ReusedWithin(ReuseScope scope)
        {
            Reuse = scope;
            return this;
        }

        Type IFluentInterface.GetType()
        {
            return GetType();
        }
    }

    internal sealed class ServiceEntry<TService, TFunc> : ServiceEntry, IRegistration<TService>, IRegistration, IReusedOwned, IReused, IOwned, IInitializable<TService>, IFluentInterface
    {
        public TFunc Factory;
        internal TService Instance;
        internal Action<Container, TService> Initializer;

        public ServiceEntry(TFunc factory)
        {
            Factory = factory;
        }

        internal void InitializeInstance(TService instance)
        {
            if (Reuse != ReuseScope.None)
                Instance = instance;
            if (Owner == Owner.Container && (object)instance is IDisposable)
                Container.TrackDisposable(instance);
            if (Initializer == null)
                return;
            Initializer(Container, instance);
        }

        public IReusedOwned InitializedBy(Action<Container, TService> initializer)
        {
            Initializer = initializer;
            return this;
        }

        public ServiceEntry<TService, TFunc> CloneFor(Container newContainer)
        {
            ServiceEntry<TService, TFunc> serviceEntry = new ServiceEntry<TService, TFunc>(Factory);
            serviceEntry.Owner = Owner;
            serviceEntry.Reuse = Reuse;
            serviceEntry.Container = newContainer;
            serviceEntry.Initializer = Initializer;
            return serviceEntry;
        }
    }
}
