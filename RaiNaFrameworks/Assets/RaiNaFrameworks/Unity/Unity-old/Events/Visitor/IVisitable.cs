namespace UniKuroKit.Events.Visitor
{
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}
