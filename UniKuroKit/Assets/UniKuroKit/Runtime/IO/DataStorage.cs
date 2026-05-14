using UniKuroKit.Serialization;
using UniKuroKit.Security;
using System.IO;

namespace UniKuroKit.IO
{
    public class DataStorage
    {
        private readonly SerializerConfigurations _settings;
        private readonly ISerializer _serializer;
        private readonly IEncryptionProvider _encryption;

        public DataStorage(SerializerConfigurations settings)
        {
            _settings = settings;
            _serializer = SerializerFactory.Create(settings.HumanReadable);
            _encryption = EncryptionFactory.Create(settings.UseEncryption, settings.EncryptionKey);
        }

        // ── Write ─────────────────────────────────────────────────────────────
        /// <summary>Serialize, encrypt, and write a single chunk to disk.</summary>
        public void WriteChunk(int index, DataContainer container)
        {
            EnsureDirectory();

            byte[] raw = _serializer.Serialize(container);
            byte[] processed = _encryption.Encrypt(raw);

            string path = _settings.GetChunkPath(index);
            File.WriteAllBytes(path, processed);
        }

        // ── Read ──────────────────────────────────────────────────────────────
        /// <summary>Read, decrypt, and deserialize a single chunk from disk.</summary>
        public DataContainer ReadChunk(int index)
        {
            string path = _settings.GetChunkPath(index);
            if (!File.Exists(path))
                return new DataContainer();

            byte[] processed = File.ReadAllBytes(path);
            byte[] raw = _encryption.Decrypt(processed);
            var container = _serializer.Deserialize(raw);
            return container;
        }

        // ── Existence helpers ─────────────────────────────────────────────────
        public bool ChunkExists(int index)
            => File.Exists(_settings.GetChunkPath(index));

        public bool AnyChunkExists()
        {
            for (int i = 0; i < _settings.ChunkCount; i++)
                if (ChunkExists(i)) return true;
            return false;
        }

        // ── Delete ────────────────────────────────────────────────────────────
        public void DeleteAll()
        {
            for (int i = 0; i < _settings.ChunkCount; i++)
            {
                string path = _settings.GetChunkPath(i);
                if (File.Exists(path))
                    File.Delete(path);
            }
        }

        // ── Directory ─────────────────────────────────────────────────────────
        private void EnsureDirectory()
        {
            string dir = _settings.GetDirectory();
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
    }
}
