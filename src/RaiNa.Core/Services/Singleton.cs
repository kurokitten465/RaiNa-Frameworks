using RaiNa.Exceptions;
using System;

namespace RaiNa.Services
{
    public abstract class Singleton<TDerived> : ISingleton where TDerived : class, ISingleton, new()
    {
        private static readonly object _lock = new();

        private static TDerived _instance = null;
        private static bool _allowConstruction;

        public static SingletonInitializeState InitializeState { get; private set; }
            = SingletonInitializeState.Uninitialized;

        public static bool IsInstanceExists => _instance != null;

        public static TDerived Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                if (InitializeState == SingletonInitializeState.Initializing)
                    throw new InvalidOperationException($"{typeof(TDerived).Name} is currently initializing.");

                CreateInstance();
                return _instance;
            }
        }

        protected Singleton()
        {
            if (!_allowConstruction)
            {
                throw new SingletonInitializeException(
                    $"Invalid operation to create a new instance of {typeof(TDerived).Name}.\n" +
                    $"Use {typeof(TDerived).Name}.Instance instead.");
            }
        }

        private static void CreateInstance()
        {
            lock (_lock)
            {
                InitializeState = SingletonInitializeState.Initializing;

                _allowConstruction = true;
                _instance = new TDerived();
                _instance.OnInitialized();

                InitializeState = SingletonInitializeState.Initialized;

                _allowConstruction = false;
            }
        }

        public static void DestroyInstance()
        {
            if (_instance == null)
                return;

            _instance.OnDestroyInstance();
            _instance = null;
            InitializeState = SingletonInitializeState.Uninitialized;
        }

        public virtual void OnInitialized() { }

        public virtual void OnDestroyInstance() { }
    }
}