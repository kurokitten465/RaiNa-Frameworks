namespace KuroKitten.UniKuroKit.StateMachine
{
    public interface IStateMachine
    {
        bool Enabled { get; }
        void Enable();
        void Disable();
        void ChangeState<TState>(TState state);
        void FixedUpdate();
        void Update();
    }
}
