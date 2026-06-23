using System;

namespace RaiNa.Services.Dependency
{
    public interface IServiceContainer : IRaiNaServiceProvider, IServiceRegistry, IDisposable
    {
        Guid Id { get; }
        string Name { get; }
        IServiceContainer Parent { get; }

        IServiceContainer CreateChildContainer(string name);
    }
}
