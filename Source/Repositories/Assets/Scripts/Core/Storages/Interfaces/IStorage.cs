namespace Core.Storages
{
    public interface IStorage<K>
    {
        void ClearAll();

        bool IsSupport<V>();

        IStorageType<K, V> Get<V>();
    }
}
