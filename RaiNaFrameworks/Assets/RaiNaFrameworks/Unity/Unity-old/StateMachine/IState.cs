namespace KuroKitten.UniKuroKit.StateMachine
{
    public interface IState<TContext> where TContext : IStateContext
    {
        void OnEnter(in TContext ctx);
        void OnUpdate(in TContext ctx);
        void OnFixedUpdate(in TContext ctx);
        void OnExit(in TContext ctx);
    }
}
