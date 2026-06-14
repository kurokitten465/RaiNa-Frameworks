using UnityEngine;

namespace Kuro.UniKit.Samples.Singleton
{
    public class Client : MonoBehaviour
    {
        private SingletonA _cachedMono;
        private SingletonB _cachedLegacy;

        private void Start()
        {
            _cachedMono = FindFirstObjectByType<SingletonA>();
            _cachedLegacy = FindFirstObjectByType<SingletonB>();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Call MonoSingleton"))
                SingletonA.Instance.Print();

            if (GUILayout.Button("Call LegacySingleton"))
                SingletonA.Instance.Print();

            if (GUILayout.Button("Destroy MonoSingleton"))
                Destroy(_cachedMono);

            if (GUILayout.Button("Destroy LegacySingleton"))
                Destroy(_cachedLegacy);

            if (GUILayout.Button("Check Status MonoSingleton"))
                Debug.Log($"Initialize: {SingletonA.IsInitialize}");

            if (GUILayout.Button("Check Status LegacySingleton"))
                Debug.Log($"Initialize: {SomeService.IsInitialize}");
        }
    }
}
