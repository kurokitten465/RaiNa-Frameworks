namespace UniKuroKit.Security
{
    public sealed class NoEncryptionProvider : IEncryptionProvider
    {
        public byte[] Encrypt(byte[] data) => data;
        public byte[] Decrypt(byte[] data) => data;
    }
}
