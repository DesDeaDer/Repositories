using System;
using System.Linq;
using UnityEngine;

namespace Core.Storages
{
    public class PlayerPrefsStorageProcessor : IStorageProcessor<string>
    {
        public void Load(IStorage<string> storage)
        {
            Load(storage, key => PlayerPrefs.GetInt(key) == 1);
            Load(storage, key => PlayerPrefs.GetInt(key));
            Load(storage, key => PlayerPrefs.GetFloat(key));
            Load(storage, key => PlayerPrefs.GetString(key));
        }

        public void Save(IStorage<string> storage)
        {
            Save<bool>(storage, (key, value) => PlayerPrefs.SetInt(key, value ? 1 : 0));
            Save<int>(storage, (key, value) => PlayerPrefs.SetInt(key, value));
            Save<float>(storage, (key, value) => PlayerPrefs.SetFloat(key, value));
            Save<string>(storage, (key, value) => PlayerPrefs.SetString(key, value));
            PlayerPrefs.Save();
        }

        public void Clear(IStorage<string> storage)
        {
            Clear<bool>(storage);
            Clear<int>(storage);
            Clear<float>(storage);
            Clear<string>(storage);
            PlayerPrefs.Save();
        }

        public void Clear()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        private static void Load<T>(IStorage<string> storage, Func<string, T> load) => Processing<T>(storage, (key, storageType) => storageType.Set(key, load(key)));

        private static void Save<T>(IStorage<string> storage, Action<string, T> save) => Processing<T>(storage, (key, storageType) => save(key, storageType.Get(key)));

        private static void Clear<T>(IStorage<string> storage) => Processing<T>(storage, (key, _) => PlayerPrefs.DeleteKey(key));

        private static void Processing<T>(IStorage<string> storage, Action<string, IStorageType<string, T>> action)
        {
            if (storage.IsSupport<T>())
            {
                var storageType = storage.Get<T>();
                foreach (var key in storageType.Keys.ToArray()) //[Optimize] TODo: write when can be writen operations with storage and tmp factory
                {
                    action(key, storageType);
                }
            }
        }
    }
}
