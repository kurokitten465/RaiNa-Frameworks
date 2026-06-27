using UnityEngine;
using RaiNa.StateMachine;
using System;

namespace RaiNa.Unity.StateMachine
{
    public abstract class MonoStateMachine<TContext> : MonoBehaviour where TContext : IStateContext
    {
        /* [Header("Subsystem Asset")]
        [SerializeField] StateMachineSubsystemWrapper _stateMachineSubsystem; */

        protected StateMachine<TContext> StateMachine;

        void OnDestroy()
        {
            StateMachine?.Dispose();
            StateMachine = null;
        }

        void OnEnable()
        {
            StateMachine.Enable();
        }

        void OnDisable()
        {
            StateMachine.Disable();
        }
    }
}
