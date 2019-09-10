using System.Collections.Generic;

namespace Core.Storages
{
    public class StorageType<K, V> : IStorageType<K, V>
    {
        public IDictionary<K, V> Values => new Dictionary<K, V>();

        public void Clear() => Values.Clear();
        public V Get(K key) => Values[key];
        public void Set(K key, V value) => Values[key] = value;
        public bool TryGet(K key, out V value) => Values.TryGetValue(key, out value);
    }
}
