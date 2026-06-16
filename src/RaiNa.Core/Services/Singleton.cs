using RaiNa.Exceptions;

namespace RaiNa.Services
{
    public abstract class Singleton<TDerived> : ISingleton where TDerived : ISingleton, new()
    {
        public static SingletonInitializeState InitializeState { get; private set; } = SingletonInitializeState.Uninitialized;
        public static bool IsInstanceExists => _instance != null;

        public static TDerived Instance => IsInstanceExists ? _instance : CreateInstance();

        private static TDerived _instance;
        private static readonly object _lock = new();
        private static bool _isValidInitialized = false;

        public Singleton()
        {
            if (!_isValidInitialized)
                throw new SingletonInitializeException($"Invalid operation to create a new instance of {typeof(TDerived).Name}.\nUse {typeof(TDerived).Name}.CreateInstance() instead.");
        }

        public static TDerived CreateInstance()
        {
            if (InitializeState == SingletonInitializeState.Initialized)
                return _instance;

            lock (_lock)
            {
                InitializeState = SingletonInitializeState.Initializing;
                _isValidInitialized = true;

                _instance = new();

                _instance.OnInitialized();
                InitializeState = SingletonInitializeState.Initialized;

                return _instance;
            }
        }

        public static void DestroyInstance()
        {
            if (!IsInstanceExists)
                return;

            _instance.OnDestroyInstance();

            _instance = default;
            
            InitializeState = SingletonInitializeState.Uninitialized;
            _isValidInitialized = false;
        }

        public virtual void OnInitialized() { }
        public virtual void OnDestroyInstance() { }
    }
}
