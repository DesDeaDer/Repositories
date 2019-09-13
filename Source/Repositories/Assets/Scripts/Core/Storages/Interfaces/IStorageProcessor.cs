namespace Core.Storages
{
    public interface IStorageProcessor<K>
    {
        void Save(IStorage<K> storage);
        void Load(IStorage<K> storage);
        void Clear(IStorage<K> storage);
        void Clear();
    }
}
