using RaiNa.Unity.Services;

namespace RaiNa.Unity
{
    public class PersistentMonoSingleton<TDerived> : MonoSingleton<TDerived> where TDerived : PersistentMonoSingleton<TDerived>
    {
        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(gameObject);
        }
    }
}
