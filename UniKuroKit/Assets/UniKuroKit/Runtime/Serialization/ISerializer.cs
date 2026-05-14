namespace UniKuroKit.Serialization
{
    public interface ISerializer
    {
        byte[] Serialize(DataContainer container);
        DataContainer Deserialize(byte[] data);
    }
}
