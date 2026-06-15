using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using RaiNa.Events;

namespace RaiNa.Unity.Events.Asset
{
    [CreateAssetMenu(
        fileName = "asset_new_event_bus",
        menuName = "RaiNa/Events/Event Bus Asset",
        order = 0 )]
    public class EventBusAsset : ScriptableObject, IEventBusAsset
    {
        [Tooltip("Human-readable label used in logs and debug overlays.")]
        [SerializeField] private string _busName = "EventBus";

        [Tooltip("Clear all runtime subscriptions when the active scene changes.")]
        [SerializeField] private bool _resetOnSceneUnload = true;

        [Tooltip("Print dispatch logs to the Unity console. Editor + Development builds only.")]
        [SerializeField] private bool _enableDebugLog = false;

        private EventBus _bus;

        public string BusName => _busName;
        public bool ResetOnSceneUnload => _resetOnSceneUnload;

        public void ResetRuntime()
        {
            _bus?.ClearAll();
            _bus = CreateBus();
            OnBusReady(_bus);

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (_enableDebugLog)
                Debug.Log($"[{_busName}] Runtime reset.");
#endif
        }

        public IEventContext<TEvent> Publish<TEvent>(TEvent @event)
            where TEvent : IEvent
        {
            EnsureBus();

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (_enableDebugLog)
                Debug.Log($"[{_busName}] >> {typeof(TEvent).Name}");
#endif

            return _bus.Publish(@event);
        }

        public void Subscribe<TEvent>(IEventHandler<TEvent> handler)
            where TEvent : IEvent
        {
            EnsureBus();
            _bus.Subscribe(handler);
        }

        public void Unsubscribe<TEvent>(IEventHandler<TEvent> handler)
            where TEvent : IEvent
        {
            EnsureBus();
            _bus.Unsubscribe(handler);
        }

        public void Clear<TEvent>() where TEvent : IEvent
        {
            _bus?.Clear<TEvent>();
        }

        public void ClearAll()
        {
            _bus?.ClearAll();
        }

        private void OnEnable()
        {
            _bus = CreateBus();
            OnBusReady(_bus);

            if (_resetOnSceneUnload)
                UnityEngine.SceneManagement.SceneManager.sceneUnloaded += OnSceneUnloaded;

#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
#endif
        }

        private void OnDisable()
        {
            if (_resetOnSceneUnload)
                UnityEngine.SceneManagement.SceneManager.sceneUnloaded -= OnSceneUnloaded;

#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeChanged;
#endif

            _bus?.ClearAll();
            _bus = null;
        }

        private void OnSceneUnloaded(UnityEngine.SceneManagement.Scene _)
        {
            ResetRuntime();
        }

#if UNITY_EDITOR
        private void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                _bus?.ClearAll();
            }
        }
#endif

        protected virtual EventBus CreateBus() => new();
        protected virtual void OnBusReady(EventBus bus) { }

        private void EnsureBus()
        {
            if (_bus == null)
            {
                _bus = CreateBus();
                OnBusReady(_bus);
            }
        }
    }
}
