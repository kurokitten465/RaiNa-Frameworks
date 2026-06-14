using UnityEngine;
using Kuro.UniKit.DesignPatterns;

namespace Kuro.UniKit.Samples.Singleton
{
    public class SingletonB : MonoBehaviour
    {
        private SomeService _instance;

        private void Awake()
        {
            _instance = SomeService.Instance;

            DontDestroyOnLoad(gameObject);
        }

        void OnDestroy()
        {
            _instance.Dispose();
        }
    }

    public class SomeService : LegacySingleton<SomeService>
    {
        public string Text = "Hello World";

        public void Print()
        {
            Debug.Log($"{this} working!");
            Debug.Log($"Initialize: {IsInitialize}");
            Debug.Log($"Print: {Text}");
        }
    }
}
