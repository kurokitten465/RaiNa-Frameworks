using System;
using UnityEngine;

namespace KuroKitten.UniKuroKit.StateMachine
{
    [CreateAssetMenu(fileName = "NewStateMachineSubsystemSO", menuName = "UniKuroKit/StateMachineSubsystemSO")]
    public class StateMachineSubsystemWrapper : ScriptableObject
    {
        [SerializeField] bool _autoInitialize = true;

        StateMachineSubsystem _subsystem;

        public StateMachineSubsystem Subsystem
        {
            get
            {
                if (_subsystem == null)
                    throw new NullReferenceException($"{name}: Subsystem must calls Initialize() first!");

                return _subsystem;
            }
        }

        public void Register(IStateMachine stateMachine) =>
            Subsystem?.Register(stateMachine);
        public void Unregister(IStateMachine stateMachine) =>
            Subsystem?.Unregister(stateMachine);

        public void Active() => Subsystem?.Enable();
        public void Disable() => Subsystem?.Disable();

        public void Initialize()
        {
            if (_subsystem != null)
            {
                Debug.LogWarning($"{name} is Already Initialized");
                return;
            }

            _subsystem = new();
        }

        void OnEnable()
        {
            if (_autoInitialize)
                Initialize();
        }

        void OnDisable()
        {
            _subsystem?.Dispose();
            _subsystem = null;
        }
    }
}
