using System;
using System.Collections.Generic;

namespace KuroKitten.UniKuroKit.StateMachine
{
    public sealed class StateMachineSubsystem : IStateMachineSubsystem, IDisposable
    {
        private readonly List<IStateMachine> _machines = new();

        public StateMachineSubsystem()
        {
            StateMachineScheduler.RegisterSubsystem(this);
        }

        public bool Enabled { get; private set; } = true;

        public void Disable() => Enabled = false;
        public void Enable() => Enabled = true;

        public void Register(IStateMachine machine)
        {
            if (!_machines.Contains(machine))
                _machines.Add(machine);
        }

        public void Unregister(IStateMachine machine)
        {
            _machines.Remove(machine);
        }

        public void Update()
        {
            if (!Enabled)
                return;

            for (int i = 0; i < _machines.Count; i++)
            {
                var m = _machines[i];

                if (m == null)
                    continue;

                if (m.Enabled)
                    m.Update();
            }
        }

        public void FixedUpdate()
        {
            if (!Enabled)
                return;

            for (int i = 0; i < _machines.Count; i++)
            {
                var m = _machines[i];

                if (m == null)
                    continue;

                if (m.Enabled)
                    m.FixedUpdate();
            }
        }

        bool _disposed = false;

        void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                StateMachineScheduler.UnregisterSubsystem(this);
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~StateMachineSubsystem()
        {
            Dispose(false);
        }
    }
}
