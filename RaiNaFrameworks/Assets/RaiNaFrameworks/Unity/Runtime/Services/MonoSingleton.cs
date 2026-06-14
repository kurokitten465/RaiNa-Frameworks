using UnityEngine;
using RaiNa.Services;

namespace RaiNa.Unity.Services
{
    public abstract class MonoSingleton<TDerived> : MonoBehaviour where TDerived : MonoSingleton<TDerived>
    {
        public static SingletonInitializeState InitializeState { get; private set; } = SingletonInitializeState.Uninitialized;
        public static bool IsInstanceExists => _instance != null;

        public static TDerived Instance => IsInstanceExists ? _instance : CreateInstance();

        private static TDerived _instance;

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

            _instance.OnInitialized();

            InitializeState = SingletonInitializeState.Initialized;
            return _instance;
        }

        public static TDerived GetInstance() => IsInstanceExists ? _instance : CreateInstance();

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
    }
}
