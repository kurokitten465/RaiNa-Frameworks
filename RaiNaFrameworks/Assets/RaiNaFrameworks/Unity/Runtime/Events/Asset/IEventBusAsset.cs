using RaiNa.Events;

namespace RaiNa.Unity.Events.Asset
{
    public interface IEventBusAsset : IEventBus
    {
        string BusName { get; }
        bool ResetOnSceneUnload { get; }
        void ResetRuntime();
    }
}
