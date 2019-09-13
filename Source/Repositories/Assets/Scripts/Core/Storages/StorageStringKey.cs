using System.Collections;

namespace Core.Storages
{
    public class StorageStringKey : IStorage<string>
    {
        Hashtable StorageTypes;

        public StorageStringKey()
        {
            StorageTypes = new Hashtable()
            {
                {  typeof(bool), new StorageType<string, bool>() },
                {  typeof(int), new StorageType<string, int>() },
                {  typeof(float), new StorageType<string, float>() },
                {  typeof(string), new StorageTypeStringString() },
            };
        }

        public StorageStringKey(Hashtable storageTypes) => StorageTypes = storageTypes;

        public void ClearAll()
        {
            foreach (var item in StorageTypes.Values)
            {
                ((ICleaner)item).Clear();
            }
        }

        public IStorageType<string, V> Get<V>() => (IStorageType<string, V>)StorageTypes[typeof(V)];

        public bool IsSupport<V>() => StorageTypes.ContainsKey(typeof(V));
    }
}
