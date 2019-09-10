using System.Collections.Specialized;

namespace Core.Storages
{
    public class StorageTypeStringString : IStorageType<string, string>
    {
        public StringDictionary Values => new StringDictionary();

        public void Clear() => Values.Clear();
        public string Get(string key) => Values[key];
        public void Set(string key, string value) => Values[key] = value;
        public bool TryGet(string key, out string value)
        {
            if (Values.ContainsKey(key))
            {
                value = Values[key];
                return true;
            }
            value = null;
            return false;
        }
    }
}
