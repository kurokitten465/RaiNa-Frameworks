using System;

namespace KuroKitten.UniKuroKit.StateMachine
{
    public sealed class StateMachine<TContext> : IStateMachine, IDisposable where TContext : IStateContext
    {
        public bool Enabled { get; private set; } = true;

        public event StateMachineDelegate<TContext> OnStateChanged;

        bool _isTransition = false;

        IState<TContext> _currentState;
        IState<TContext> _nextState;

        readonly TContext _context;
        readonly IStateMachineSubsystem _subsystem;

        public StateMachine(in TContext context, in IStateMachineSubsystem subsystem)
        {
            _context = context;
            _subsystem = subsystem;
            _subsystem.Register(this);
        }

        public void Disable() => Enabled = false;
        public void Enable() => Enabled = true;

        public void ChangeState<TState>(TState state)
        {
            if (state is not IState<TContext> newState)
                throw new ArgumentException(
                    $"State type '{typeof(TState).Name}' does not implement {typeof(IState<TContext>).Name}.");

            _nextState = newState;
            _isTransition = true;
        }

        public void FixedUpdate()
        {
            if (!Enabled)
                return;

            if (_isTransition)
            {
                ProcessTransition();
                return;
            }

            _currentState?.OnFixedUpdate(_context);
        }

        public void Update()
        {
            if (!Enabled)
                return;

            if (_isTransition)
            {
                ProcessTransition();
                return;
            }

            _currentState?.OnUpdate(_context);
        }

        void ProcessTransition()
        {
            if (_nextState == null)
                return;

            _currentState?.OnExit(_context);

            var oldState = _currentState ?? _nextState;
            _currentState = _nextState;

            _currentState?.OnEnter(_context);

            OnStateChanged?.Invoke(_context, oldState, _currentState);

            _nextState = null;
            _isTransition = false;
        }

        bool _disposed = false;

        void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _subsystem.Unregister(this);
                Enabled = false;
                _currentState = null;
                _nextState = null;
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~StateMachine()
        {
            Dispose(false);
        }
    }
}
