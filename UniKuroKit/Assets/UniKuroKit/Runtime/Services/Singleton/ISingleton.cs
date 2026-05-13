namespace UniKuroKit.Services.Singleton
{
    public interface ISingleton
    {
        void OnInitialized();
        void OnDestroyInstance();
    }
}
