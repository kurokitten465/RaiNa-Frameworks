namespace UniKuroKit.Security
{
    public static class EncryptionFactory
    {
        public static IEncryptionProvider Create(bool useEncryption, string key)
            => useEncryption ? new AesEncryptionProvider(key) : new NoEncryptionProvider();
    }
}
