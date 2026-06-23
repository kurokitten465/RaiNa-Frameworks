using System;

namespace RaiNa.Services.Dependency
{
    public interface IRaiNaServiceProvider
    {
        T Get<T>();
        object Get(Type serviceType);
        bool TryGet<T>(out T service);
    }
}
