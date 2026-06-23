using System;

namespace RaiNa.Services.Dependency
{
    public interface IServiceRegistry
    {
        void AddSingleton<T>(Func<IRaiNaServiceProvider, T> factory);
        void AddScoped<T>(Func<IRaiNaServiceProvider, T> factory);
        void AddTransient<T>(Func<IRaiNaServiceProvider, T> factory);
    }
}
