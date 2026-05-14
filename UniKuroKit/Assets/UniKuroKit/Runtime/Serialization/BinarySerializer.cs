using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace UniKuroKit.Serialization
{
    public class BinarySerializer : ISerializer
    {
        public byte[] Serialize(DataContainer container)
        {
            string json = JsonUtility.ToJson(new ContainerRoot { Entries = container.Entries });
            byte[] raw  = Encoding.UTF8.GetBytes(json);

            byte[] magic = { 0x53, 0x41, 0x56, 0x45 };
            byte[] result = new byte[magic.Length + raw.Length];
            Buffer.BlockCopy(magic, 0, result, 0,            magic.Length);
            Buffer.BlockCopy(raw,   0, result, magic.Length, raw.Length);
            return result;
        }

        public DataContainer Deserialize(byte[] data)
        {
            if (data.Length < 4 ||
                data[0] != 0x53 || data[1] != 0x41 || data[2] != 0x56 || data[3] != 0x45)
                throw new InvalidDataException("Save file has invalid header.");

            byte[] raw   = new byte[data.Length - 4];
            Buffer.BlockCopy(data, 4, raw, 0, raw.Length);

            string json  = Encoding.UTF8.GetString(raw);
            var root     = JsonUtility.FromJson<ContainerRoot>(json);

            var container = new DataContainer();
            if (root?.Entries != null)
                container.Entries.AddRange(root.Entries);

            container.RebuildIndex();
            return container;
        }

        [Serializable]
        private class ContainerRoot
        {
            public System.Collections.Generic.List<DataEntry> Entries;
        }
    }
}
