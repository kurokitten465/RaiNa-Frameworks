using System;
using RaiNa.Services.Dependency;

namespace RaiNa.Unity
{
    public sealed class GlobalService : IDisposable
    {
        public static IServiceContainer Container { get; private set; }
        public static GlobalService Instance { get; private set; }
        public static bool IsInitialized { get; private set; } = false;

        public static Action<bool> OnInstanceDisposing;

        private bool _disposed;

        public GlobalService()
        {
            if (IsInitialized)
                throw new InvalidOperationException($"{this} Container is already initialzed.");

            Container = new ServiceContainer("Global");
            Instance = this;
            IsInitialized = true;
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;

            OnInstanceDisposing?.Invoke(disposing);

            if (disposing)
                Container.Dispose();

            Container = null;
            Instance = null;
            OnInstanceDisposing = null;

            IsInitialized = false;

            _disposed = true;
        }

        ~GlobalService()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
