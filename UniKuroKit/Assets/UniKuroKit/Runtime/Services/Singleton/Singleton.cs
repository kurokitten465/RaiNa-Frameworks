using System;
using UniKuroKit.Exceptions;

namespace UniKuroKit.Services.Singleton
{
    public abstract class Singleton<TDerived> : ISingleton where TDerived : Singleton<TDerived>, new()
    {
        public static SingletonInitializeState InitializeState { get; private set; } = SingletonInitializeState.Uninitialized;
        public static bool IsInstanceExists => _instance != null;

        public static TDerived Instance => IsInstanceExists ? _instance : CreateInstance();

        static TDerived _instance;
        static readonly object _lock = new();

        public Singleton()
        {
            if (InitializeState == SingletonInitializeState.Initialized)
                throw new SingletonInitializeException($"Invalid operation to create a new instance of {typeof(TDerived).Name}. Use {typeof(TDerived).Name}.CreateInstance() instead.");
        }

        public static TDerived CreateInstance()
        {
            if (InitializeState == SingletonInitializeState.Initialized)
                return _instance; //

            lock (_lock)
            {
                InitializeState = SingletonInitializeState.Initializing;
                _instance = Activator.CreateInstance<TDerived>();
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
            _instance = null;
            InitializeState = SingletonInitializeState.Uninitialized;
        }

        public void OnInitialized() { }
        public void OnDestroyInstance() { }
    }
}
