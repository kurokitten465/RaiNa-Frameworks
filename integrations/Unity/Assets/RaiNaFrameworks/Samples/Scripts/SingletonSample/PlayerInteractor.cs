using TMPro;
using UnityEngine;

namespace RaiNa.Unity.Samples.Singleton
{
    public enum SingletonType
    {
        OldSchoolSingleton, RaiNaSingleton
    }

    public class PlayerInteractor : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Flag>(out Flag flag))
                flag.OnInteracted();
        }
    }
}
