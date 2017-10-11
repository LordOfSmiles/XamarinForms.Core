using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinForms.Core.Infrastructure.FunqContainer.Interfaces;

namespace XamarinForms.Core.Infrastructure.FunqContainer
{
    public sealed class Container : IDisposable
    {
        private readonly Dictionary<ServiceKey, ServiceEntry> _services = new Dictionary<ServiceKey, ServiceEntry>();
        private readonly Stack<WeakReference> _disposables = new Stack<WeakReference>();
        private readonly Stack<Container> _childContainers = new Stack<Container>();
        private Container _parent;

        public Container()
        {
            var services = _services;
            var index = new ServiceKey(typeof(Func<Container, Container>), null);
            var serviceEntry1 = new ServiceEntry<Container, Func<Container, Container>>(c => c);
            serviceEntry1.Container = this;
            serviceEntry1.Instance = this;

            serviceEntry1.Owner = Owner.External;
            serviceEntry1.Reuse = ReuseScope.Container;

            var serviceEntry2 = serviceEntry1;
            services[index] = serviceEntry2;
        }

        public Owner DefaultOwner { get; set; }

        public ReuseScope DefaultReuse { get; set; }

        public Container CreateChildContainer()
        {
            var container = new Container()
            {
                _parent = this
            };
            _childContainers.Push(container);
            return container;
        }

        public void Dispose()
        {
            while (_disposables.Count > 0)
            {
                var weakReference = _disposables.Pop();
                var target = (IDisposable)weakReference.Target;
                if (weakReference.IsAlive)
                    target.Dispose();
            }
            while (_childContainers.Count > 0)
                _childContainers.Pop().Dispose();
        }

        public IRegistration<TService> Register<TService>(Func<Container, TService> factory)
        {
            return Register<TService>(null, factory);
        }

        public IRegistration<TService> Register<TService>(string name, Func<Container, TService> factory)
        {
            return RegisterImpl<TService, Func<Container, TService>>(name, factory);
        }

        public IRegistration<TService> Register<TService, TArg>(string name, Func<Container, TArg, TService> factory)
        {
            return RegisterImpl<TService, Func<Container, TArg, TService>>(name, factory);
        }

        public IRegistration<TService> Register<TService, TArg>(Func<Container, TArg, TService> factory)
        {
            return Register<TService, TArg>(null, factory);
        }

        public void Register<TService>(TService instance)
        {
            Register<TService>(null, instance);
        }

        public void Register<TService>(string name, TService instance)
        {
            var serviceEntry = RegisterImpl<TService, Func<Container, TService>>(name, null);
            serviceEntry.ReusedWithin(ReuseScope.Hierarchy).OwnedBy(Owner.External);
            serviceEntry.InitializeInstance(instance);
        }

        public TService Resolve<TService>()
        {
            return ResolveNamed<TService>(null);
        }

        public TService Resolve<TService, TArg>(TArg arg)
        {
            return ResolveNamed<TService, TArg>(null, arg);
        }

        public TService ResolveNamed<TService>(string name)
        {
            return ResolveImpl<TService>(name, true);
        }

        public TService ResolveNamed<TService, TArg>(string name, TArg arg)
        {
            return ResolveImpl<TService, TArg>(name, true, arg);
        }

        public Func<TService> LazyResolve<TService>()
        {
            return LazyResolveNamed<TService>(null);
        }

        public Func<TArg, TService> LazyResolve<TService, TArg>()
        {
            return LazyResolveNamed<TService, TArg>(null);
        }

        public Func<TService> LazyResolveNamed<TService>(string name)
        {
            ThrowIfNotRegistered<TService, Func<Container, TService>>(name);
            return () => ResolveNamed<TService>(name);
        }

        public Func<TArg, TService> LazyResolveNamed<TService, TArg>(string name)
        {
            ThrowIfNotRegistered<TService, Func<Container, TArg, TService>>(name);
            return arg => ResolveNamed<TService, TArg>(name, arg);
        }

        public TService TryResolve<TService>()
        {
            return TryResolveNamed<TService>(null);
        }

        public TService TryResolve<TService, TArg>(TArg arg)
        {
            return TryResolveNamed<TService, TArg>(null, arg);
        }

        public TService TryResolveNamed<TService>(string name)
        {
            return ResolveImpl<TService>(name, false);
        }

        public TService TryResolveNamed<TService, TArg>(string name, TArg arg)
        {
            return ResolveImpl<TService, TArg>(name, false, arg);
        }

        internal void TrackDisposable(object instance)
        {
            _disposables.Push(new WeakReference(instance));
        }

        private ServiceEntry<TService, TFunc> RegisterImpl<TService, TFunc>(string name, TFunc factory)
        {
            if (typeof(TService) == typeof(Container))
                throw new ArgumentException("Cant register container");

            var serviceEntry1 = new ServiceEntry<TService, TFunc>(factory);
            serviceEntry1.Container = this;
            serviceEntry1.Reuse = DefaultReuse;
            serviceEntry1.Owner = DefaultOwner;
            var serviceEntry2 = serviceEntry1;
            _services[new ServiceKey(typeof(TFunc), name)] = serviceEntry2;
            return serviceEntry2;
        }

        private TService ResolveImpl<TService>(string name, bool throwIfMissing)
        {
            var entry = GetEntry<TService, Func<Container, TService>>(name, throwIfMissing);
            if (entry == null)
                return default(TService);
            var instance = entry.Instance;
            if (instance == null)
            {
                instance = entry.Factory(entry.Container);
                entry.InitializeInstance(instance);
            }
            return instance;
        }

        private TService ResolveImpl<TService, TArg>(string name, bool throwIfMissing, TArg arg)
        {
            var entry = GetEntry<TService, Func<Container, TArg, TService>>(name, throwIfMissing);
            if (entry == null)
                return default(TService);
            var instance = entry.Instance;
            if (instance == null)
            {
                instance = entry.Factory(entry.Container, arg);
                entry.InitializeInstance(instance);
            }
            return instance;
        }

        private ServiceEntry<TService, TFunc> GetEntry<TService, TFunc>(string serviceName, bool throwIfMissing)
        {
            var key = new ServiceKey(typeof(TFunc), serviceName);
            ServiceEntry serviceEntry = null;
            var container = this;
            while (!container._services.TryGetValue(key, out serviceEntry) && container._parent != null)
                container = container._parent;
            if (serviceEntry != null)
            {
                if (serviceEntry.Reuse == ReuseScope.Container && serviceEntry.Container != this)
                {
                    serviceEntry = ((ServiceEntry<TService, TFunc>)serviceEntry).CloneFor(this);
                    _services[key] = serviceEntry;
                }
            }
            else if (throwIfMissing)
                ThrowMissing<TService>(serviceName);
            return (ServiceEntry<TService, TFunc>)serviceEntry;
        }

        private static TService ThrowMissing<TService>(string serviceName)
        {
            if (serviceName == null)
                throw new ResolutionException(typeof(TService));
            throw new ResolutionException(typeof(TService), serviceName);
        }

        private void ThrowIfNotRegistered<TService, TFunc>(string name)
        {
            GetEntry<TService, TFunc>(name, true);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, TService> LazyResolve<TService, T1, T2>()
        {
            return LazyResolveNamed<TService, T1, T2>(null);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, TService> LazyResolveNamed<TService, T1, T2>(string name)
        {
            ThrowIfNotRegistered<TService, Func<Container, T1, T2, TService>>(name);
            return (arg1, arg2) => ResolveNamed<TService, T1, T2>(name, arg1, arg2);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, TService> LazyResolve<TService, T1, T2, T3>()
        {
            return LazyResolveNamed<TService, T1, T2, T3>(null);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, TService> LazyResolveNamed<TService, T1, T2, T3>(string name)
        {
            ThrowIfNotRegistered<TService, Func<Container, T1, T2, T3, TService>>(name);
            return (arg1, arg2, arg3) => ResolveNamed<TService, T1, T2, T3>(name, arg1, arg2, arg3);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, TService> LazyResolve<TService, T1, T2, T3, T4>()
        {
            return LazyResolveNamed<TService, T1, T2, T3, T4>(null);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, TService> LazyResolveNamed<TService, T1, T2, T3, T4>(string name)
        {
            ThrowIfNotRegistered<TService, Func<Container, T1, T2, T3, T4, TService>>(name);
            return (arg1, arg2, arg3, arg4) => ResolveNamed<TService, T1, T2, T3, T4>(name, arg1, arg2, arg3, arg4);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, T5, TService> LazyResolve<TService, T1, T2, T3, T4, T5>()
        {
            return LazyResolveNamed<TService, T1, T2, T3, T4, T5>(null);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, T5, TService> LazyResolveNamed<TService, T1, T2, T3, T4, T5>(string name)
        {
            ThrowIfNotRegistered<TService, Func<Container, T1, T2, T3, T4, T5, TService>>(name);
            return (arg1, arg2, arg3, arg4, arg5) => ResolveNamed<TService, T1, T2, T3, T4, T5>(name, arg1, arg2, arg3, arg4, arg5);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, T5, T6, TService> LazyResolve<TService, T1, T2, T3, T4, T5, T6>()
        {
            return LazyResolveNamed<TService, T1, T2, T3, T4, T5, T6>(null);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, T5, T6, TService> LazyResolveNamed<TService, T1, T2, T3, T4, T5, T6>(string name)
        {
            ThrowIfNotRegistered<TService, Func<Container, T1, T2, T3, T4, T5, T6, TService>>(name);
            return (arg1, arg2, arg3, arg4, arg5, arg6) => ResolveNamed<TService, T1, T2, T3, T4, T5, T6>(name, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, T5, T6, T7, TService> LazyResolve<TService, T1, T2, T3, T4, T5, T6, T7>()
        {
            return LazyResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7>(null);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, T5, T6, T7, TService> LazyResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7>(string name)
        {
            ThrowIfNotRegistered<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, TService>>(name);
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7>(name, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, TService> LazyResolve<TService, T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            return LazyResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8>(null);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, TService> LazyResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8>(string name)
        {
            ThrowIfNotRegistered<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, TService>>(name);
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8>(name, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TService> LazyResolve<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            return LazyResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9>(null);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TService> LazyResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string name)
        {
            ThrowIfNotRegistered<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, TService>>(name);
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9>(name, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TService> LazyResolve<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return LazyResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(null);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TService> LazyResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string name)
        {
            ThrowIfNotRegistered<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TService>>(name);
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(name, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TService> LazyResolve<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return LazyResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(null);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TService> LazyResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string name)
        {
            ThrowIfNotRegistered<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TService>>(name);
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(name, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TService> LazyResolve<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return LazyResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(null);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TService> LazyResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string name)
        {
            ThrowIfNotRegistered<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TService>>(name);
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(name, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TService> LazyResolve<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return LazyResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(null);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TService> LazyResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string name)
        {
            ThrowIfNotRegistered<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TService>>(name);
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(name, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TService> LazyResolve<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return LazyResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(null);
        }

        [DebuggerStepThrough]
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TService> LazyResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string name)
        {
            ThrowIfNotRegistered<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TService>>(name);
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(name, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2>(Func<Container, T1, T2, TService> factory)
        {
            return Register<TService, T1, T2>(null, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2>(string name, Func<Container, T1, T2, TService> factory)
        {
            return RegisterImpl<TService, Func<Container, T1, T2, TService>>(name, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3>(Func<Container, T1, T2, T3, TService> factory)
        {
            return Register<TService, T1, T2, T3>(null, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3>(string name, Func<Container, T1, T2, T3, TService> factory)
        {
            return RegisterImpl<TService, Func<Container, T1, T2, T3, TService>>(name, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4>(Func<Container, T1, T2, T3, T4, TService> factory)
        {
            return Register<TService, T1, T2, T3, T4>(null, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4>(string name, Func<Container, T1, T2, T3, T4, TService> factory)
        {
            return RegisterImpl<TService, Func<Container, T1, T2, T3, T4, TService>>(name, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4, T5>(Func<Container, T1, T2, T3, T4, T5, TService> factory)
        {
            return Register<TService, T1, T2, T3, T4, T5>(null, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4, T5>(string name, Func<Container, T1, T2, T3, T4, T5, TService> factory)
        {
            return RegisterImpl<TService, Func<Container, T1, T2, T3, T4, T5, TService>>(name, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4, T5, T6>(Func<Container, T1, T2, T3, T4, T5, T6, TService> factory)
        {
            return Register<TService, T1, T2, T3, T4, T5, T6>(null, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4, T5, T6>(string name, Func<Container, T1, T2, T3, T4, T5, T6, TService> factory)
        {
            return RegisterImpl<TService, Func<Container, T1, T2, T3, T4, T5, T6, TService>>(name, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4, T5, T6, T7>(Func<Container, T1, T2, T3, T4, T5, T6, T7, TService> factory)
        {
            return Register<TService, T1, T2, T3, T4, T5, T6, T7>(null, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4, T5, T6, T7>(string name, Func<Container, T1, T2, T3, T4, T5, T6, T7, TService> factory)
        {
            return RegisterImpl<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, TService>>(name, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4, T5, T6, T7, T8>(Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, TService> factory)
        {
            return Register<TService, T1, T2, T3, T4, T5, T6, T7, T8>(null, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4, T5, T6, T7, T8>(string name, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, TService> factory)
        {
            return RegisterImpl<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, TService>>(name, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, TService> factory)
        {
            return Register<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9>(null, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string name, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, TService> factory)
        {
            return RegisterImpl<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, TService>>(name, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TService> factory)
        {
            return Register<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(null, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string name, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TService> factory)
        {
            return RegisterImpl<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TService>>(name, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TService> factory)
        {
            return Register<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(null, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string name, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TService> factory)
        {
            return RegisterImpl<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TService>>(name, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TService> factory)
        {
            return Register<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(null, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string name, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TService> factory)
        {
            return RegisterImpl<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TService>>(name, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TService> factory)
        {
            return Register<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(null, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string name, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TService> factory)
        {
            return RegisterImpl<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TService>>(name, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TService> factory)
        {
            return Register<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(null, factory);
        }

        [DebuggerStepThrough]
        public IRegistration<TService> Register<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string name, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TService> factory)
        {
            return RegisterImpl<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TService>>(name, factory);
        }

        [DebuggerStepThrough]
        public TService Resolve<TService, T1, T2>(T1 arg1, T2 arg2)
        {
            return ResolveNamed<TService, T1, T2>(null, arg1, arg2);
        }

        [DebuggerStepThrough]
        public TService ResolveNamed<TService, T1, T2>(string name, T1 arg1, T2 arg2)
        {
            return ResolveImpl<TService, T1, T2>(name, true, arg1, arg2);
        }

        [DebuggerStepThrough]
        public TService Resolve<TService, T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3)
        {
            return ResolveNamed<TService, T1, T2, T3>(null, arg1, arg2, arg3);
        }

        [DebuggerStepThrough]
        public TService ResolveNamed<TService, T1, T2, T3>(string name, T1 arg1, T2 arg2, T3 arg3)
        {
            return ResolveImpl<TService, T1, T2, T3>(name, true, arg1, arg2, arg3);
        }

        [DebuggerStepThrough]
        public TService Resolve<TService, T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return ResolveNamed<TService, T1, T2, T3, T4>(null, arg1, arg2, arg3, arg4);
        }

        [DebuggerStepThrough]
        public TService ResolveNamed<TService, T1, T2, T3, T4>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return ResolveImpl<TService, T1, T2, T3, T4>(name, true, arg1, arg2, arg3, arg4);
        }

        [DebuggerStepThrough]
        public TService Resolve<TService, T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            return ResolveNamed<TService, T1, T2, T3, T4, T5>(null, arg1, arg2, arg3, arg4, arg5);
        }

        [DebuggerStepThrough]
        public TService ResolveNamed<TService, T1, T2, T3, T4, T5>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            return ResolveImpl<TService, T1, T2, T3, T4, T5>(name, true, arg1, arg2, arg3, arg4, arg5);
        }

        [DebuggerStepThrough]
        public TService Resolve<TService, T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            return ResolveNamed<TService, T1, T2, T3, T4, T5, T6>(null, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        [DebuggerStepThrough]
        public TService ResolveNamed<TService, T1, T2, T3, T4, T5, T6>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            return ResolveImpl<TService, T1, T2, T3, T4, T5, T6>(name, true, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        [DebuggerStepThrough]
        public TService Resolve<TService, T1, T2, T3, T4, T5, T6, T7>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            return ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7>(null, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        [DebuggerStepThrough]
        public TService ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            return ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7>(name, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        [DebuggerStepThrough]
        public TService Resolve<TService, T1, T2, T3, T4, T5, T6, T7, T8>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            return ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8>(null, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        [DebuggerStepThrough]
        public TService ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            return ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8>(name, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        [DebuggerStepThrough]
        public TService Resolve<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            return ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9>(null, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        [DebuggerStepThrough]
        public TService ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            return ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9>(name, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        [DebuggerStepThrough]
        public TService Resolve<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            return ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(null, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        [DebuggerStepThrough]
        public TService ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            return ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(name, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        [DebuggerStepThrough]
        public TService Resolve<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            return ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(null, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        [DebuggerStepThrough]
        public TService ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            return ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(name, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        [DebuggerStepThrough]
        public TService Resolve<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            return ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(null, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        [DebuggerStepThrough]
        public TService ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            return ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(name, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        [DebuggerStepThrough]
        public TService Resolve<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            return ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(null, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        [DebuggerStepThrough]
        public TService ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            return ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(name, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        [DebuggerStepThrough]
        public TService Resolve<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            return ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(null, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

        [DebuggerStepThrough]
        public TService ResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            return ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(name, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

        private TService ResolveImpl<TService, T1, T2>(string name, bool throwIfMissing, T1 arg1, T2 arg2)
        {
            var entry = GetEntry<TService, Func<Container, T1, T2, TService>>(name, throwIfMissing);
            if (entry == null)
                return default(TService);
            var instance = entry.Instance;
            if (instance == null)
            {
                instance = entry.Factory(entry.Container, arg1, arg2);
                entry.InitializeInstance(instance);
            }
            return instance;
        }

        private TService ResolveImpl<TService, T1, T2, T3>(string name, bool throwIfMissing, T1 arg1, T2 arg2, T3 arg3)
        {
            var entry = GetEntry<TService, Func<Container, T1, T2, T3, TService>>(name, throwIfMissing);
            if (entry == null)
                return default(TService);
            var instance = entry.Instance;
            if (instance == null)
            {
                instance = entry.Factory(entry.Container, arg1, arg2, arg3);
                entry.InitializeInstance(instance);
            }
            return instance;
        }

        private TService ResolveImpl<TService, T1, T2, T3, T4>(string name, bool throwIfMissing, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            var entry = GetEntry<TService, Func<Container, T1, T2, T3, T4, TService>>(name, throwIfMissing);
            if (entry == null)
                return default(TService);
            var instance = entry.Instance;
            if (instance == null)
            {
                instance = entry.Factory(entry.Container, arg1, arg2, arg3, arg4);
                entry.InitializeInstance(instance);
            }
            return instance;
        }

        private TService ResolveImpl<TService, T1, T2, T3, T4, T5>(string name, bool throwIfMissing, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            var entry = GetEntry<TService, Func<Container, T1, T2, T3, T4, T5, TService>>(name, throwIfMissing);
            if (entry == null)
                return default(TService);
            var instance = entry.Instance;
            if (instance == null)
            {
                instance = entry.Factory(entry.Container, arg1, arg2, arg3, arg4, arg5);
                entry.InitializeInstance(instance);
            }
            return instance;
        }

        private TService ResolveImpl<TService, T1, T2, T3, T4, T5, T6>(string name, bool throwIfMissing, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            var entry = GetEntry<TService, Func<Container, T1, T2, T3, T4, T5, T6, TService>>(name, throwIfMissing);
            if (entry == null)
                return default(TService);
            var instance = entry.Instance;
            if (instance == null)
            {
                instance = entry.Factory(entry.Container, arg1, arg2, arg3, arg4, arg5, arg6);
                entry.InitializeInstance(instance);
            }
            return instance;
        }

        private TService ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7>(string name, bool throwIfMissing, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            var entry = GetEntry<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, TService>>(name, throwIfMissing);
            if (entry == null)
                return default(TService);
            var instance = entry.Instance;
            if (instance == null)
            {
                instance = entry.Factory(entry.Container, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                entry.InitializeInstance(instance);
            }
            return instance;
        }

        private TService ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8>(string name, bool throwIfMissing, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            var entry = GetEntry<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, TService>>(name, throwIfMissing);
            if (entry == null)
                return default(TService);
            var instance = entry.Instance;
            if (instance == null)
            {
                instance = entry.Factory(entry.Container, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                entry.InitializeInstance(instance);
            }
            return instance;
        }

        private TService ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string name, bool throwIfMissing, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            var entry = GetEntry<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, TService>>(name, throwIfMissing);
            if (entry == null)
                return default(TService);
            var instance = entry.Instance;
            if (instance == null)
            {
                instance = entry.Factory(entry.Container, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                entry.InitializeInstance(instance);
            }
            return instance;
        }

        private TService ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string name, bool throwIfMissing, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            var entry = GetEntry<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TService>>(name, throwIfMissing);
            if (entry == null)
                return default(TService);
            var instance = entry.Instance;
            if (instance == null)
            {
                instance = entry.Factory(entry.Container, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                entry.InitializeInstance(instance);
            }
            return instance;
        }

        private TService ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string name, bool throwIfMissing, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            var entry = GetEntry<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TService>>(name, throwIfMissing);
            if (entry == null)
                return default(TService);
            var instance = entry.Instance;
            if (instance == null)
            {
                instance = entry.Factory(entry.Container, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                entry.InitializeInstance(instance);
            }
            return instance;
        }

        private TService ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string name, bool throwIfMissing, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            var entry = GetEntry<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TService>>(name, throwIfMissing);
            if (entry == null)
                return default(TService);
            var instance = entry.Instance;
            if (instance == null)
            {
                instance = entry.Factory(entry.Container, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                entry.InitializeInstance(instance);
            }
            return instance;
        }

        private TService ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string name, bool throwIfMissing, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            var entry = GetEntry<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TService>>(name, throwIfMissing);
            if (entry == null)
                return default(TService);
            var instance = entry.Instance;
            if (instance == null)
            {
                instance = entry.Factory(entry.Container, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                entry.InitializeInstance(instance);
            }
            return instance;
        }

        private TService ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string name, bool throwIfMissing, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            var entry = GetEntry<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TService>>(name, throwIfMissing);
            if (entry == null)
                return default(TService);
            var instance = entry.Instance;
            if (instance == null)
            {
                instance = entry.Factory(entry.Container, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                entry.InitializeInstance(instance);
            }
            return instance;
        }

        private TService ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string name, bool throwIfMissing, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            var entry = GetEntry<TService, Func<Container, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TService>>(name, throwIfMissing);
            if (entry == null)
                return default(TService);
            var instance = entry.Instance;
            if (instance == null)
            {
                instance = entry.Factory(entry.Container, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                entry.InitializeInstance(instance);
            }
            return instance;
        }

        [DebuggerStepThrough]
        public TService TryResolve<TService, T1, T2>(T1 arg1, T2 arg2)
        {
            return TryResolveNamed<TService, T1, T2>(null, arg1, arg2);
        }

        [DebuggerStepThrough]
        public TService TryResolveNamed<TService, T1, T2>(string name, T1 arg1, T2 arg2)
        {
            return ResolveImpl<TService, T1, T2>(name, false, arg1, arg2);
        }

        [DebuggerStepThrough]
        public TService TryResolve<TService, T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3)
        {
            return TryResolveNamed<TService, T1, T2, T3>(null, arg1, arg2, arg3);
        }

        [DebuggerStepThrough]
        public TService TryResolveNamed<TService, T1, T2, T3>(string name, T1 arg1, T2 arg2, T3 arg3)
        {
            return ResolveImpl<TService, T1, T2, T3>(name, false, arg1, arg2, arg3);
        }

        [DebuggerStepThrough]
        public TService TryResolve<TService, T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return TryResolveNamed<TService, T1, T2, T3, T4>(null, arg1, arg2, arg3, arg4);
        }

        [DebuggerStepThrough]
        public TService TryResolveNamed<TService, T1, T2, T3, T4>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return ResolveImpl<TService, T1, T2, T3, T4>(name, false, arg1, arg2, arg3, arg4);
        }

        [DebuggerStepThrough]
        public TService TryResolve<TService, T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            return TryResolveNamed<TService, T1, T2, T3, T4, T5>(null, arg1, arg2, arg3, arg4, arg5);
        }

        [DebuggerStepThrough]
        public TService TryResolveNamed<TService, T1, T2, T3, T4, T5>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            return ResolveImpl<TService, T1, T2, T3, T4, T5>(name, false, arg1, arg2, arg3, arg4, arg5);
        }

        [DebuggerStepThrough]
        public TService TryResolve<TService, T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            return TryResolveNamed<TService, T1, T2, T3, T4, T5, T6>(null, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        [DebuggerStepThrough]
        public TService TryResolveNamed<TService, T1, T2, T3, T4, T5, T6>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            return ResolveImpl<TService, T1, T2, T3, T4, T5, T6>(name, false, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        [DebuggerStepThrough]
        public TService TryResolve<TService, T1, T2, T3, T4, T5, T6, T7>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            return TryResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7>(null, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        [DebuggerStepThrough]
        public TService TryResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            return ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7>(name, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        [DebuggerStepThrough]
        public TService TryResolve<TService, T1, T2, T3, T4, T5, T6, T7, T8>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            return TryResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8>(null, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        [DebuggerStepThrough]
        public TService TryResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            return ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8>(name, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        [DebuggerStepThrough]
        public TService TryResolve<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            return TryResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9>(null, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        [DebuggerStepThrough]
        public TService TryResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            return ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9>(name, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        [DebuggerStepThrough]
        public TService TryResolve<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            return TryResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(null, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        [DebuggerStepThrough]
        public TService TryResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            return ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(name, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        [DebuggerStepThrough]
        public TService TryResolve<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            return TryResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(null, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        [DebuggerStepThrough]
        public TService TryResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            return ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(name, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        [DebuggerStepThrough]
        public TService TryResolve<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            return TryResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(null, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        [DebuggerStepThrough]
        public TService TryResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            return ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(name, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        [DebuggerStepThrough]
        public TService TryResolve<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            return TryResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(null, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        [DebuggerStepThrough]
        public TService TryResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            return ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(name, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        [DebuggerStepThrough]
        public TService TryResolve<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            return TryResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(null, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

        [DebuggerStepThrough]
        public TService TryResolveNamed<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string name, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            return ResolveImpl<TService, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(name, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }
    }
}
