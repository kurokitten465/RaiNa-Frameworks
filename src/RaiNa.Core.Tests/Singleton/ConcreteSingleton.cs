using RaiNa.Services;

namespace RaiNa.Tests.Singleton
{
    internal sealed class ConcreteSingleton : Singleton<ConcreteSingleton>
    {
        private readonly bool _val = true;

        public bool Hello() => _val;
    }
}
