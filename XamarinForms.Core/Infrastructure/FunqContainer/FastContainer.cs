using System;
using XamarinForms.Core.Infrastructure.FunqContainer.Interfaces;

namespace XamarinForms.Core.Infrastructure.FunqContainer
{
    public static class FastContainer
    {
        private static readonly Container Container = new Container();

        public static Owner DefaultOwner
        {
            get => Container.DefaultOwner;
            set => Container.DefaultOwner = value;
        }

        public static ReuseScope DefaultReuse
        {
            get => Container.DefaultReuse;
            set => Container.DefaultReuse = value;
        }

        public static Container CreateChildContainer()
        {
            return Container.CreateChildContainer();
        }

        public static IRegistration<TService> Register<TService>(Func<Container, TService> factory)
        {
            return Container.Register(factory);
        }

        public static IRegistration<TService> Register<TService>(string name, Func<Container, TService> factory)
        {
            return Container.Register(name, factory);
        }

        public static IRegistration<TService> Register<TService, TArg>(string name, Func<Container, TArg, TService> factory)
        {
            return Container.Register(name, factory);
        }

        public static IRegistration<TService> Register<TService, TArg>(Func<Container, TArg, TService> factory)
        {
            return Container.Register(factory);
        }

        public static void Register<TService>(TService instance)
        {
            Container.Register(instance);
        }

        public static void Register<TService>(string name, TService instance)
        {
            Container.Register(name, instance);
        }

        public static TService Resolve<TService>()
        {
            return Container.Resolve<TService>();
        }

        public static TService Resolve<TService, TArg>(TArg arg)
        {
            return Container.Resolve<TService, TArg>(arg);
        }

        public static TService ResolveNamed<TService>(string name)
        {
            return Container.ResolveNamed<TService>(name);
        }

        public static TService ResolveNamed<TService, TArg>(string name, TArg arg)
        {
            return Container.ResolveNamed<TService, TArg>(name, arg);
        }

        public static Func<TService> LazyResolve<TService>()
        {
            return Container.LazyResolve<TService>();
        }

        public static Func<TArg, TService> LazyResolve<TService, TArg>()
        {
            return Container.LazyResolve<TService, TArg>();
        }

        public static Func<TService> LazyResolveNamed<TService>(string name)
        {
            return Container.LazyResolveNamed<TService>(name);
        }

        public static Func<TArg, TService> LazyResolveNamed<TService, TArg>(string name)
        {
            return Container.LazyResolveNamed<TService, TArg>(name);
        }

        public static TService TryResolve<TService>()
        {
            return Container.TryResolve<TService>();
        }

        public static TService TryResolve<TService, TArg>(TArg arg)
        {
            return Container.TryResolve<TService, TArg>(arg);
        }

        public static TService TryResolveNamed<TService>(string name)
        {
            return Container.TryResolveNamed<TService>(name);
        }

        public static TService TryResolveNamed<TService, TArg>(string name, TArg arg)
        {
            return Container.TryResolveNamed<TService, TArg>(name, arg);
        }
    }
}
