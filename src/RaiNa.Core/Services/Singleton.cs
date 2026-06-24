using System;

namespace RaiNa.Services
{
    public abstract class Singleton<TDerived> : ISingleton, IDisposable where TDerived : class, ISingleton, new()
    {
        private static readonly Lazy<TDerived> _lazy = new(() =>
        {
            InitializeState = SingletonInitializeState.Initializing;
            TDerived instance = new();
            instance.Initialize();
            return instance;
        }, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

        public static TDerived Instance => _lazy.Value;
        public static bool IsInitialized => _lazy.IsValueCreated;
        public static SingletonInitializeState InitializeState { get; private set; } = SingletonInitializeState.Uninitialized;

        private bool _disposed;

        public void Initialize()
        {
            if (_disposed || IsInitialized) return;

            OnInitialized();
            InitializeState = SingletonInitializeState.Initialized;
        }

        protected virtual void OnInitialized() { }
        protected virtual void OnDisposing() { }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                OnDisposing();

            _disposed = true;
        }

        ~Singleton()
        {
             Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}