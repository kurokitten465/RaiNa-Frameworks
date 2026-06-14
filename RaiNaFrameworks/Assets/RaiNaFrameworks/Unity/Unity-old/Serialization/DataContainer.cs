using System;
using System.Collections.Generic;

namespace UniKuroKit.Serialization
{
    public class DataContainer
    {
        public List<DataEntry> Entries = new();
        private Dictionary<string, DataEntry> _index;

        public void RebuildIndex()
        {
            _index = new Dictionary<string, DataEntry>(Entries.Count);
            foreach (var entry in Entries)
                _index[entry.Key] = entry;
        }

        public void Set<T>(string key, T value)
        {
            EnsureIndex();

            var entry = new DataEntry
            {
                Key      = key,
                TypeName = typeof(T).AssemblyQualifiedName,
                JsonValue = UnityEngine.JsonUtility.ToJson(new Wrapper<T> { Value = value })
            };

            if (_index.TryGetValue(key, out var existing))
            {
                int idx = Entries.IndexOf(existing);
                Entries[idx] = entry;
                _index[key]  = entry;
            }
            else
            {
                Entries.Add(entry);
                _index[key] = entry;
            }
        }
        
        public bool TryGet<T>(string key, out T value)
        {
            EnsureIndex();

            if (_index.TryGetValue(key, out var entry))
            {
                try
                {
                    var wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(entry.JsonValue);
                    value = wrapper.Value;
                    return true;
                }
                catch
                {
                    value = default;
                    return false;
                }
            }

            value = default;
            return false;
        }

        public T Get<T>(string key, T defaultValue = default)
            => TryGet<T>(key, out var v) ? v : defaultValue;

        public bool HasKey(string key) { EnsureIndex(); return _index.ContainsKey(key); }

        public void Remove(string key)
        {
            EnsureIndex();
            if (_index.TryGetValue(key, out var entry))
            {
                Entries.Remove(entry);
                _index.Remove(key);
            }
        }

        public void Clear() { Entries.Clear(); _index?.Clear(); }

        private void EnsureIndex()
        {
            if (_index == null) RebuildIndex();
        }

        [Serializable]
        private class Wrapper<T> { public T Value; }
    }
}
