using System.Collections;

namespace Core.Storages
{
    public class StorageStringKey : IStorage<string>
    {
        Hashtable StorageTypes => new Hashtable()
        {
            {  typeof(bool), Bools },
            {  typeof(int), Ints },
            {  typeof(float), Floats },
            {  typeof(string), Strings },
        };

        public IStorageType<string, bool> Bools => new StorageType<string, bool>();
        public IStorageType<string, int> Ints => new StorageType<string, int>();
        public IStorageType<string, float> Floats => new StorageType<string, float>();
        public IStorageType<string, string> Strings => new StorageTypeStringString();

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
