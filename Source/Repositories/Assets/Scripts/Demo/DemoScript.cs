using Core.Storages;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    string KeyBool0 => "KeyBool0";
    string KeyInt0 => "KeyInt0";
    string KeyFloat0 => "KeyFloat0";
    string KeyString0 => "KeyString0";
    string KeyBool1 => "KeyBool1";
    string KeyInt1 => "KeyInt1";
    string KeyFloat1 => "KeyFloat1";
    string KeyString1 => "KeyString1";
    string KeyBool2 => "KeyBool2";
    string KeyInt2 => "KeyInt02";
    string KeyFloat2 => "KeyFloat2";
    string KeyString2 => "KeyString2";

    void Start()
    {
        var storage1 = new StorageStringKey();
        var storage2 = new StorageStringKey();
        var PlayerPrefsStorageProcessor = new PlayerPrefsStorageProcessor();
        var CustomFileStorageProcessor1 = new CustomFileStorageProcessor("0", "save");
        var CustomFileStorageProcessor2 = new CustomFileStorageProcessor("1", "save");

        PlayerPrefsStorageProcessor.Clear();
        CustomFileStorageProcessor1.Clear();
        CustomFileStorageProcessor2.Clear();

        //Init Some keys
        storage1.Get<bool>().Set(KeyBool0);
        storage1.Get<bool>().Set(KeyBool1, true);
        storage1.Get<bool>().Set(KeyBool2, false);
        storage1.Get<int>().Set(KeyInt0);
        storage1.Get<int>().Set(KeyInt1, 90);
        storage1.Get<int>().Set(KeyInt2, 0xff);
        storage1.Get<float>().Set(KeyFloat0);
        storage1.Get<float>().Set(KeyFloat1, .3231f);
        storage1.Get<float>().Set(KeyFloat2, 23232f);
        storage1.Get<string>().Set(KeyString0);
        storage1.Get<string>().Set(KeyString1, string.Empty);
        storage1.Get<string>().Set(KeyString2, "text, text. text!? 213");
        CustomFileStorageProcessor1.Save(storage1);
        PlayerPrefsStorageProcessor.Save(storage1);
        storage1.ClearAll();
        storage1.Get<string>().Set(KeyString2);
        storage1.Get<float>().Set(KeyFloat2);
        PlayerPrefsStorageProcessor.Load(storage1);

        {
            if (storage1.Get<string>().TryGet(KeyString2, out var value).Log())
            {
                Debug.Log(value.ToString());
            }
        }

        {
            if (storage1.Get<bool>().TryGet(KeyBool1, out var value).Log())
            {
                Debug.Log(value.ToString());
            }
        }

        {
            if (storage1.Get<float>().TryGet(KeyFloat2, out var value).Log())
            {
                Debug.Log(value.ToString());
            }
        }

    }
}
