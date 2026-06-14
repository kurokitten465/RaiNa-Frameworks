namespace UniKuroKit.Events.Visitor
{
    public interface IVisitor { }
    public interface IVisitor<in T> : IVisitor
    {
        void Visit(T visitable);
    }
}
