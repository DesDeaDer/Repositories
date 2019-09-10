namespace Core.Storages
{
    public interface IStorageProcessor<K>
    {
        void Save(IStorage<K> storage);
        void Load(IStorage<K> storage);
    }
}
