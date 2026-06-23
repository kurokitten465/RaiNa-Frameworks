using UnityEngine;

namespace RaiNa.Unity.Services.Dependency
{
    public static class ServiceBootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Initialize()
        {
            _ = new GlobalService();

            Application.quitting += () =>
            {
                GlobalService.Instance.Dispose();
            };
        }
    }
}
