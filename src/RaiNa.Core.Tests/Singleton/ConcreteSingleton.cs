using RaiNa.Services;

namespace RaiNa.Tests.Singleton
{
    internal sealed class ConcreteSingleton : Singleton<ConcreteSingleton>
    {
        public bool Hello() => true;
    }
}
