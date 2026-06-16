using RaiNa.Services;

namespace RaiNa.Unity.Services
{
    public class PersistentMonoSingleton<TDerived> : MonoSingleton<TDerived>, ISingleton
        where TDerived : PersistentMonoSingleton<TDerived>, ISingleton
    {
        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(gameObject);
        }
    }
}
