using UnityEngine;

namespace KuroKitten.UniKuroKit.StateMachine
{
    public abstract class MonoStateMachine<TContext> : MonoBehaviour where TContext : IStateContext
    {
        [Header("Subsystem Asset")]
        [SerializeField] StateMachineSubsystemWrapper _stateMachineSubsystem;

        StateMachine<TContext> _stateMachine;
        public StateMachine<TContext> StateMachine => _stateMachine;

        void Awake()
        {
            if (_stateMachineSubsystem == null)
            {
                Debug.LogError($"{name}: StateMachineSubsystem is not assigned in inspector!");
                enabled = false;
                return;
            }

            CreateStateMachine(_stateMachineSubsystem.Subsystem, out _stateMachine);
        }

        protected abstract void CreateStateMachine(StateMachineSubsystem subsystem, out StateMachine<TContext> stateMachine);

        void OnDestroy()
        {
            _stateMachine?.Dispose();
            _stateMachine = null;
        }

        void OnEnable()
        {
            _stateMachine.Enable();
        }

        void OnDisable()
        {
            _stateMachine.Disable();
        }
    }
}
