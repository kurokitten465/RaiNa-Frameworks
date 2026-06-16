using RaiNa.Services;
using RaiNa.Unity.Services;

namespace RaiNa.Unity
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
