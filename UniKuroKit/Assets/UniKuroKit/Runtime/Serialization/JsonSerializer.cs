using System;
using System.Text;
using UnityEngine;

namespace UniKuroKit.Serialization
{
    public class JsonSerializer : ISerializer
    {
        public byte[] Serialize(DataContainer container)
        {
            // JsonUtility doesn't support top-level lists → wrap in a root object
            var root  = new ContainerRoot { Entries = container.Entries };
            string json = JsonUtility.ToJson(root, prettyPrint: true);
            return Encoding.UTF8.GetBytes(json);
        }

        public DataContainer Deserialize(byte[] data)
        {
            string json  = Encoding.UTF8.GetString(data);
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
