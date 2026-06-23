namespace RaiNa.StateMachine
{
    public readonly struct NullStateContext : IStateContext
    {
        public static NullStateContext Token { get; } = new();
    }
}
