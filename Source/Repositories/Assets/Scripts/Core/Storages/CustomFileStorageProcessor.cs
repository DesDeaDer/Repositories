using System;
using System.IO;
using UnityEngine;

namespace Core.Storages
{
    public class CustomFileStorageProcessor : IStorageProcessor<string>
    {
        enum TypeId : byte
        {
            None = 0,
            Bool = 1,
            Int = 2,
            Float = 3,
            String = 4,
        }

        public string FullPath => Path.Combine(Application.persistentDataPath, $"{UUID}.{Extension}");

        public string UUID { get; }
        public string Extension { get; }

        public CustomFileStorageProcessor(string uuid, string extension)
        {
            UUID = uuid;
            Extension = extension;
        }

        public void Clear(IStorage<string> storage)
        {
            //TODO: write when can be writen operations with storage and tmp factory
        }

        public void Clear()
        {
            File.Delete(FullPath);
        }

        public void Load(IStorage<string> storage)
        {
            if (File.Exists(FullPath))
            {
                using (var file = File.OpenText(FullPath))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        Deserialize(storage, Parce(line));
                    }
                }
            }
        }

        public void Save(IStorage<string> storage)
        {
            using (var file = File.CreateText(FullPath))
            {
                Processing<bool>(storage, (key, storageType) => WriteSerialize(file, key, TypeId.Bool, storageType));
                Processing<int>(storage, (key, storageType) => WriteSerialize(file, key, TypeId.Int, storageType));
                Processing<float>(storage, (key, storageType) => WriteSerialize(file, key, TypeId.Float, storageType));
                Processing<string>(storage, (key, storageType) => WriteSerialize(file, key, TypeId.String, storageType));
            }
        }

        private static TypeId DeserializeTypeId(string type) => (TypeId)Convert.ToByte(type);
        private static string SerializeTypeId(TypeId typeId) => ((byte)typeId).ToString();

        private static Action<IStorage<string>, string, string>[] Loads =
        {
            (storage, key, value) => storage.Get<bool>().Set(key, bool.Parse(value)),
            (storage, key, value) => storage.Get<int>().Set(key, int.Parse(value)),
            (storage, key, value) => storage.Get<float>().Set(key, float.Parse(value)),
            (storage, key, value) => storage.Get<string>().Set(key, value),
        };

        private static void Deserialize(IStorage<string> storage, (string key, string typeId, string value) p) => Deserialize(storage, p.key, DeserializeTypeId(p.typeId), p.value);
        private static void Deserialize(IStorage<string> storage, string key, TypeId typeId, string value) => Loads[(int)typeId - 1](storage, key, value);

        private static (string key, string typeId, string value) Parce(string line)
        {
            var indexKeyBegin = 1;
            var indexKeyEnd = line.IndexOf(' ', indexKeyBegin);
            var indexTypeBegin = indexKeyEnd + 1;
            var indexTypeEnd = line.IndexOf('>', indexTypeBegin);
            var indexValueBegin = indexTypeEnd + 1;
            var indexValueEnd = line.Length - 3;

            var key = line.Substring(indexKeyBegin, indexKeyEnd - indexKeyBegin);
            var type = line.Substring(indexTypeBegin, indexTypeEnd - indexTypeBegin);
            var value = line.Substring(indexValueBegin, indexValueEnd - indexValueBegin);

            return (key, type, value);
        }

        private static void WriteSerialize<T>(StreamWriter streamWriter, string key, TypeId typeId, IStorageType<string, T> storageType) => streamWriter.WriteLine(Serialize(key, typeId, storageType.Get(key)));

        private static string Serialize(string key, TypeId typeId, object value) => $"<{key} {SerializeTypeId(typeId)}>{value}</>";

        private static void Processing<T>(IStorage<string> storage, Action<string, IStorageType<string, T>> action)
        {
            if (storage.IsSupport<T>())
            {
                var storageString = storage.Get<T>();
                foreach (var key in storageString.Keys)
                {
                    action(key, storageString);
                }
            }
        }
    }
}
