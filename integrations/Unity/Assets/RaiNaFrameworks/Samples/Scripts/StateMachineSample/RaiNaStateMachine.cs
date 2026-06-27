using RaiNa.StateMachine;
using RaiNa.Unity.StateMachine;
using UnityEngine;

namespace RaiNa.Unity.Samples
{
    public class RaiNaStateMachine : MonoBehaviour, IStateContext
    {
        public readonly string Context = "RaiNa";

        private StateMachineSubsystem _subsystem;
        private StateMachine<RaiNaStateMachine> _machine;

        private void Awake()
        {
            // Create the subsystem that will drive the state-machine updates.
            _subsystem = new();
            StateMachineScheduler.RegisterSubsystem(_subsystem);

            // Create the machine and register it with the subsystem.
            _machine = new(this, _subsystem);
            _subsystem.Register(_machine);
        }

        void OnEnable()
        {
            // Enable the machine and its subsystem when the component becomes active.
            _machine.Enable();
            _subsystem.Enable();
        }

        void OnDisable()
        {
            // Disable the machine and its subsystem when the component is disabled.
            _machine.Disable();
            _subsystem.Disable();
        }

        private void Update()
        {
            // Press A/S/D to switch to different states.
            if (Input.GetKeyDown(KeyCode.A))
                _machine.ChangeState(new RedLight());
            if (Input.GetKeyDown(KeyCode.S))
                _machine.ChangeState(new YellowLight());
            if (Input.GetKeyDown(KeyCode.D))
                _machine.ChangeState(new GreenLight());
        }

        void OnDestroy()
        {
            // Clean up the machine and subsystem when the object is destroyed.
            _machine?.Dispose();
            _subsystem?.Dispose();
        }
    }

    public class RedLight : IState<RaiNaStateMachine>
    {
        public void OnEnter(in RaiNaStateMachine ctx) => Debug.Log($"{ctx.Context} Enter : {this}");
        public void OnExit(in RaiNaStateMachine ctx) => Debug.Log($"Leaving : {this}");
        public void OnUpdate(in RaiNaStateMachine ctx, float deltaTime) { }
    }

    public class YellowLight : IState<RaiNaStateMachine>
    {
        public void OnEnter(in RaiNaStateMachine ctx) => Debug.Log($"{ctx.Context} Enter : {this}");
        public void OnExit(in RaiNaStateMachine ctx) => Debug.Log($"Leaving : {this}");
        public void OnUpdate(in RaiNaStateMachine ctx, float deltaTime) { }
    }

    public class GreenLight : IState<RaiNaStateMachine>
    {
        public void OnEnter(in RaiNaStateMachine ctx) => Debug.Log($"{ctx.Context} Enter : {this}");
        public void OnExit(in RaiNaStateMachine ctx) => Debug.Log($"Leaving : {this}");
        public void OnUpdate(in RaiNaStateMachine ctx, float deltaTime) { }
    }
}
