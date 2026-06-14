using System.IO;
using UnityEngine;

namespace UniKuroKit.Serialization
{
    public class SerializerConfigurations
    {
        // ── File layout ───────────────────────────────────────────────────────
        /// Base name used for all chunk files: "save" → save_01.bin / save_01.sav
        public string ProfileName  { get; private set; } = "save";

        /// Root folder inside Application.persistentDataPath
        public string SubFolder    { get; private set; } = "Saves";

        /// Number of chunk files to split data across (≥1)
        public int    ChunkCount   { get; private set; } = 1;

        // ── Format ────────────────────────────────────────────────────────────
        /// If true, output is pretty-printed JSON wrapped in UTF-8 text.
        /// If false, output is compact binary (faster, smaller).
        public bool   HumanReadable { get; private set; } = false;

        // ── Security ──────────────────────────────────────────────────────────
        public bool   UseEncryption { get; private set; } = true;

        /// 32-character (256-bit) AES key. Override with your own secret.
        public string EncryptionKey { get; private set; } = "DefaultKey_PleaseChangeMe!12345!";

        // ── Convenience ───────────────────────────────────────────────────────
        public static SerializerConfigurations Default => new SerializerConfigurations();

        // ── Fluent builder ────────────────────────────────────────────────────
        public SerializerConfigurations WithProfile(string name)      { ProfileName   = name;  return this; }
        public SerializerConfigurations WithSubFolder(string folder)  { SubFolder     = folder; return this; }
        public SerializerConfigurations WithChunks(int count)         { ChunkCount    = Mathf.Max(1, count); return this; }
        public SerializerConfigurations AsHumanReadable(bool hr=true) { HumanReadable = hr;    return this; }
        public SerializerConfigurations WithEncryption(bool on=true)  { UseEncryption = on;    return this; }
        public SerializerConfigurations WithKey(string key)           { EncryptionKey = key;   return this; }

        // ── Derived paths ─────────────────────────────────────────────────────
        public string Extension => HumanReadable ? ".sav" : ".bin";

        public string GetChunkPath(int chunkIndex)
        {
            string dir  = System.IO.Path.Combine(Application.persistentDataPath, SubFolder);
            string file = $"{ProfileName}_{chunkIndex + 1:D2}{Extension}";
            return Path.Combine(dir, file);
        }

        public string GetDirectory()
            => Path.Combine(Application.persistentDataPath, SubFolder);
    }
}
