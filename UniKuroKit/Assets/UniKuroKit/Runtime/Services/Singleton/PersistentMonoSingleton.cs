namespace UniKuroKit.Services.Singleton
{
    public abstract class PersistentMonoSingleton<TDerived> : MonoSingleton<TDerived> where TDerived : MonoSingleton<TDerived>
    {
        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(gameObject);
        }
    }
}
