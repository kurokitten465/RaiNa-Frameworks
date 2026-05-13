namespace UniKuroKit.Services.Singleton
{
    public abstract class PersistentNonLazyMonoSingleton<TDerived> : NonLazyMonoSingleton<TDerived> where TDerived : NonLazyMonoSingleton<TDerived>
    {
        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(gameObject);
        }
    }
}
