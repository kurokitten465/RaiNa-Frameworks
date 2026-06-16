namespace RaiNa.Services
{
    public interface ISingleton
    {
        void OnInitialized();
        void OnDestroyInstance();
    }
}
