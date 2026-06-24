using RaiNa.Services.Dependency;
using UnityEngine;

namespace RaiNa.Unity.Services.Dependency
{
    /// <summary>
    /// Initializes the global ServiceContainer during Unity's SubsystemRegistration phase.
    /// This ensures dependency injection is available early in the application lifecycle,
    /// before any game systems or managers are initialized.
    /// </summary>
    public static class ServiceContainerInitializer
    {
        private static IServiceContainer _globalContainer;

        /// <summary>
        /// Gets the global root ServiceContainer instance.
        /// Automatically initialized during SubsystemRegistration.
        /// </summary>
        public static IServiceContainer Global => _globalContainer;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Initialize()
        {
            if (_globalContainer != null)
            {
                Debug.LogWarning("ServiceContainerInitializer already initialized. Skipping re-initialization.");
                return;
            }

            _globalContainer = new ServiceContainer("Global");
            Debug.Log("Global ServiceContainer initialized during SubsystemRegistration.");

            Application.quitting += Reset;
        }

        /// <summary>
        /// Resets the global container. Useful for testing or application reset scenarios.
        /// </summary>
        public static void Reset()
        {
            _globalContainer?.ClearAll();
            _globalContainer = null;
            Application.quitting -= Reset;
        }
    }
}
