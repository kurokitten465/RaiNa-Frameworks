using System.Collections.Generic;
using UnityEngine;

namespace KuroKitten.UniKuroKit.StateMachine
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
                
                _subsystems[i]?.Update();
            }
        }

        public static void FixedUpdate()
        {
            for (int i = 0; i < _subsystems.Count; i++)
            {
                if (_subsystems[i] == null)
                    continue;

                _subsystems[i]?.FixedUpdate();
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Clean()
        {
            _subsystems.Clear();
        }
    }
}
