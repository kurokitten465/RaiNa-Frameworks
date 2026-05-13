using UnityEngine;

namespace UniKuroKit.Services.Singleton
{
    public abstract class NonLazyMonoSingleton<TDerived> : MonoBehaviour, ISingleton where TDerived : NonLazyMonoSingleton<TDerived>
    {
        public static SingletonInitializeState InitializeState { get; private set; } = SingletonInitializeState.Uninitialized;
        public static bool IsInstanceExists => _instance != null;

        public static TDerived Instance => _instance;
        static TDerived _instance;

        protected virtual void Awake()
        {
            if (!IsInstanceExists)
                CreateInstance();

            if (this as TDerived != _instance)
                Destroy(gameObject);
        }

        public static TDerived CreateInstance()
        {
            if (InitializeState == SingletonInitializeState.Initialized)
                return _instance;

            InitializeState = SingletonInitializeState.Initializing;

            if (_instance == null)
            {
                _instance = FindFirstObjectByType<TDerived>(FindObjectsInactive.Exclude);

                if (_instance != null)
                {
                    InitializeState = SingletonInitializeState.Initialized;
                    return _instance;
                }
            }

            GameObject singletonInstance = new($"{typeof(TDerived).Name} Instance");
            _instance = singletonInstance.AddComponent<TDerived>();
            InitializeState = SingletonInitializeState.Initialized;
            _instance.OnInitialized();
            return _instance;
        }

        protected virtual void OnDestroy()
        {
            if (!IsInstanceExists)
                return;

            OnDestroyInstance();
            InitializeState = SingletonInitializeState.Uninitialized;
            _instance = null;
        }

        public static void DestroyInstance() => Destroy(_instance.gameObject);

        public virtual void OnInitialized() { }
        public virtual void OnDestroyInstance() { }

        public static TDerived GetInstance() => IsInstanceExists ? _instance : CreateInstance();
    }
}
