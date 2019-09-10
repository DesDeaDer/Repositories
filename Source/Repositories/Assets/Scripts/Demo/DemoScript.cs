using Core.Storages;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    private void D()
    {
        var storage = new StorageStringKey();
        var storageProcessor = new PlayerPrefsStorageProcessor();

        storageProcessor.Load(storage);

        storage.Get<bool>().Set("KeyBool0", true);
        storage.Get<int>().Set("KeyInt0", 1);
        storage.Get<float>().Set("KeyFloat0", 0f);
        storage.Get<string>().Set("KeyString0", string.Empty);

        if (storage.Get<int>().TryGet("KeyInt0", out var intValue))
        {
            //
        }

        storageProcessor.Save(storage);
    }
}
