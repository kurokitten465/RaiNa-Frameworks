using System.Collections.Generic;
using UniKuroKit.Serialization;

namespace UniKuroKit.IO
{
    public class ChunkSplitter
    {
        /// <summary>
        /// Partition the master container into <paramref name="chunkCount"/> sub-containers.
        /// Entries are distributed round-robin so each chunk stays roughly equal.
        /// </summary>
        public static List<DataContainer> Split(DataContainer master, int chunkCount)
        {
            if (chunkCount <= 1)
            {
                return new List<DataContainer> { master };
            }

            // Create empty sub-containers
            var chunks = new List<DataContainer>(chunkCount);
            for (int i = 0; i < chunkCount; i++)
                chunks.Add(new DataContainer());

            // Distribute entries round-robin
            for (int i = 0; i < master.Entries.Count; i++)
                chunks[i % chunkCount].Entries.Add(master.Entries[i]);

            return chunks;
        }

        public static DataContainer Merge(List<DataContainer> chunks)
        {
            var master = new DataContainer();
            foreach (var chunk in chunks)
                master.Entries.AddRange(chunk.Entries);

            master.RebuildIndex();
            return master;
        }
    }
}
