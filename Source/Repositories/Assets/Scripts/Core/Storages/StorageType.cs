using System.Collections.Generic;

namespace Core.Storages
{
    public class StorageType<K, V> : IStorageType<K, V>
    {
        public StorageType() => _values = new Dictionary<K, V>();

        private IDictionary<K, V> _values { get; }

        public IEnumerable<K> Keys => _values.Keys;

        public void Clear() => _values.Clear();
        public V Get(K key) => _values[key];
        public void Set(K key, V value) => _values[key] = value;
        public bool TryGet(K key, out V value) => _values.TryGetValue(key, out value);
    }
}
