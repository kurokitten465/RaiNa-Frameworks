using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace RaiNa.Services.Dependency
{
    public sealed class ServiceContainer : IServiceContainer
    {
        private readonly ReaderWriterLockSlim _lock = new();
        private readonly Dictionary<Type, RegistrationEntry> _descriptors;
        private readonly ConcurrentDictionary<Type, object> _singletons = new();
        private readonly ConcurrentDictionary<Type, object> _scoped = new();
        private readonly List<ServiceContainer> _children = new();

        private volatile bool _disposed;

        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
        public IServiceContainer Parent { get; }

        public ServiceContainer(string name)
        {
            Name = name;
            Parent = this;
            _descriptors = new Dictionary<Type, RegistrationEntry>();
        }

        public ServiceContainer(string name, ServiceContainer parent)
        {
            Name = name;
            Parent = parent;

            parent._lock.EnterReadLock();

            try
            {
                _descriptors = new Dictionary<Type, RegistrationEntry>(parent._descriptors);
            }
            finally
            {
                parent._lock.ExitReadLock();
            }
        }

        public IServiceContainer CreateChildContainer(string name)
        {
            ThrowIfDisposed();

            var child = new ServiceContainer(name, this);

            _lock.EnterWriteLock();

            try
            {
                _children.Add(child);
            }
            finally
            {
                _lock.ExitWriteLock();
            }

            return child;
        }

        public void AddSingleton<T>(Func<IRaiNaServiceProvider, T> factory) =>
            Register(typeof(T), ServiceLifetime.Singleton, p => factory(p));

        public void AddScoped<T>(Func<IRaiNaServiceProvider, T> factory) => 
            Register(typeof(T), ServiceLifetime.Scoped, p => factory(p));

        public void AddTransient<T>(Func<IRaiNaServiceProvider, T> factory) =>
            Register(typeof(T), ServiceLifetime.Transient, p => factory(p));

        private void Register(
            Type serviceType,
            ServiceLifetime lifetime,
            Func<IRaiNaServiceProvider, object> factory)
        {
            ThrowIfDisposed();

            _lock.EnterWriteLock();

            try
            {
                if (_descriptors.ContainsKey(serviceType))
                {
                    throw new InvalidOperationException(
                        $"Service already registered: {serviceType.FullName}");
                }

                _descriptors.Add(
                    serviceType,
                    new RegistrationEntry(Id, Name, new ServiceDescriptor(serviceType, lifetime, factory))
                );
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public T Get<T>() => (T)Get(typeof(T));

        public object Get(Type serviceType)
        {
            ThrowIfDisposed();

            _lock.EnterReadLock();

            try
            {
                if (!_descriptors.TryGetValue(serviceType, out var registry))
                {
                    throw new InvalidOperationException(
                        $"Service not registered: {serviceType.FullName}");
                }

                return registry.Descriptor.Lifetime switch
                {
                    ServiceLifetime.Singleton => ResolveSingleton(registry.Descriptor),
                    ServiceLifetime.Scoped => ResolveScoped(registry.Descriptor),
                    ServiceLifetime.Transient => registry.Descriptor.Factory(this),
                    _ => throw new NotSupportedException()
                };
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public bool TryGet<T>(out T service)
        {
            try
            {
                service = Get<T>();
                return true;
            }
            catch
            {
                service = default!;
                return false;
            }
        }

        private object ResolveSingleton(ServiceDescriptor descriptor) =>
            _singletons.GetOrAdd(descriptor.ServiceType, _ => descriptor.Factory(this));

        private object ResolveScoped(ServiceDescriptor descriptor) =>
            _scoped.GetOrAdd(descriptor.ServiceType, _ => descriptor.Factory(this));

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(Name);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                foreach (var child in _children)
                {
                    child.Dispose();
                }

                foreach (var instance in _scoped.Values)
                {
                    if (instance is IDisposable disposable)
                        disposable.Dispose();
                }

                foreach (var instance in _singletons.Values)
                {
                    if (instance is IDisposable disposable)
                        disposable.Dispose();
                }
            }

            _lock.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
