namespace Core.Storages
{
    public interface IStorageType<K, V> : ICleaner
    {
        bool TryGet(K key, out V value);
        V Get(K key);
        void Set(K key, V value);
    }
}
