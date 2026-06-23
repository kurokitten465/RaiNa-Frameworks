using UnityEngine;

namespace RaiNa.Unity.Samples.Singleton
{
    public class Flag : MonoBehaviour
    {
        [SerializeField] private SingletonType _singletonType;

        public void OnInteracted()
        {
            switch (_singletonType)
            {
                case SingletonType.OldSchoolSingleton :
                    GameManager.Instance.AddScore();
                    break;
                case SingletonType.RaiNaSingleton : 
                    RaiNaGameManager.Instance.AddScore();
                    break;
            }
        }
    }
}
