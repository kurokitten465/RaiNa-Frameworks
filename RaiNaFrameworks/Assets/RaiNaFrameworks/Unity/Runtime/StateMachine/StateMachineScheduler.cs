using System.Collections.Generic;
using UnityEngine;
using RaiNa.StateMachine;

namespace RaiNa.Unity.StateMachine
{
    public static class StateMachineScheduler
    {
        private static readonly List<IStateMachineSubsystem> _subsystems = new();

        public static void RegisterSubsystem(IStateMachineSubsystem subsystem)
        {
            if (!_subsystems.Contains(subsystem))
                _subsystems.Add(subsystem);
        }

        public static void UnregisterSubsystem(IStateMachineSubsystem subsystem)
        {
            _subsystems.Remove(subsystem);
        }

        public static void Update()
        {
            for (int i = 0; i < _subsystems.Count; i++)
            {
                if (_subsystems[i] == null)
                    continue;

                float dt = Time.deltaTime;
                
                _subsystems[i]?.Update(dt);
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Clean()
        {
            _subsystems.Clear();
        }
    }
}
