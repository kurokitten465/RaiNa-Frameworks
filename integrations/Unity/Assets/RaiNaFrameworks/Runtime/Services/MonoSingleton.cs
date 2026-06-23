using UnityEngine;
using RaiNa.Services;

namespace RaiNa.Unity.Services
{
    /// <summary>
    /// Generic MonoBehaviour singleton that manages a single instance across the scene lifetime.
    /// </summary>
    /// <typeparam name="TDerived">The derived singleton class type.</typeparam>
    public abstract class MonoSingleton<TDerived> : MonoBehaviour, ISingleton
        where TDerived : MonoSingleton<TDerived>, ISingleton
    {
        public static SingletonInitializeState InitializeState { get; private set; }
            = SingletonInitializeState.Uninitialized;
        public static bool HasInstance => _instance != null;

        public static TDerived Instance => GetOrCreateInstance();
        private static TDerived _instance;

        private static readonly object _lockObject = new();

        protected virtual void Awake()
        {
            HandleDuplicateInstance();
            InitializeInstance();
        }

        /// <summary>
        /// Gets or creates the singleton instance. Thread-safe.
        /// </summary>
        public static TDerived GetOrCreateInstance()
        {
            lock (_lockObject)
            {
                if (HasInstance)
                    return _instance;

                return CreateInstance();
            }
        }

        private static TDerived CreateInstance()
        {
            if (InitializeState == SingletonInitializeState.Initialized)
                return _instance;

            InitializeState = SingletonInitializeState.Initializing;

            _instance = FindExistingInstance() ?? CreateNewInstance();
            FinalizeInitialization();
            return _instance;
        }

        private static TDerived FindExistingInstance() => 
            FindFirstObjectByType<TDerived>(FindObjectsInactive.Exclude);

        private static TDerived CreateNewInstance()
        {
            string instanceName = $"{typeof(TDerived).Name} Instance";
            GameObject singletonGameObject = new(instanceName);
            return singletonGameObject.AddComponent<TDerived>();
        }

        private void HandleDuplicateInstance()
        {
            if (HasInstance && this as TDerived != _instance)
            {
                Debug.LogWarning($"Duplicate {typeof(TDerived).Name} singleton detected. Destroying duplicate.", gameObject);
                Destroy(gameObject);
            }
        }

        private void InitializeInstance()
        {
            _instance = this as TDerived;
            OnInitialized();
            FinalizeInitialization();
        }

        private static void FinalizeInitialization()
        {
            if (InitializeState != SingletonInitializeState.Initialized)
                InitializeState = SingletonInitializeState.Initialized;
        }

        protected virtual void OnDestroy()
        {
            if (!HasInstance || _instance != this as TDerived)
                return;

            OnDestroyInstance();
            ResetSingleton();
        }

        private static void ResetSingleton()
        {
            InitializeState = SingletonInitializeState.Uninitialized;
            _instance = null;
        }

        /// <summary>
        /// Destroy the singleton instance.
        /// </summary>
        public static void DestroyInstance()
        {
            if (HasInstance)
                Destroy(_instance.gameObject);
        }

        /// <summary>
        /// Called when the singleton is initialized.
        /// </summary>
        public virtual void OnInitialized() { }

        /// <summary>
        /// Called when the singleton instance is being destroyed.
        /// </summary>
        public virtual void OnDestroyInstance() { }
    }
}
