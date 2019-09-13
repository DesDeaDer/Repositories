using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq; 

namespace Core.Storages
{
    public class StorageTypeStringString : IStorageType<string, string>
    {
        private StringDictionary _values;

        public StorageTypeStringString()
        {
            _values = new StringDictionary();
        }

        public IEnumerable<string> Keys => _values.Keys.Cast<string>();

        public void Clear() => _values.Clear();
        public string Get(string key) => _values[key];
        public void Set(string key, string value) => _values[key] = value;
        public bool TryGet(string key, out string value)
        {
            if (_values.ContainsKey(key))
            {
                value = _values[key];
                return true;
            }
            value = null;
            return false;
        }
    }
}
