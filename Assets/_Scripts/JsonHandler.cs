using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class JsonHandler : MonoBehaviour
{
    public static void SerializeData<T>(T data, string jsonName)
    {
        string jsonString = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/" + jsonName + ".json", jsonString);
    }

    public static T DeserializeData<T>(string json)
    {
        string jsonPath = Application.persistentDataPath + "/" + json + ".json";

        if (!File.Exists(jsonPath))
        {
            Debug.Log("Could not find a .json with given name");
            return default;
        }

        string jsonContent = File.ReadAllText(jsonPath);
        return JsonUtility.FromJson<T>(jsonContent);
    }

    public void ArrayToJson<T>(T[] array, string jsonName)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        string jsonString = JsonUtility.ToJson(wrapper,true);

        File.WriteAllText(Application.persistentDataPath + "/" + jsonName + ".json", jsonString);
    }

    public T[] FromJsonToArray<T>(string json)
    {
        string jsonPath = Application.persistentDataPath + "/" + json + ".json"; 
        if (!File.Exists(jsonPath))
        {
            Debug.Log("Could not find a .json with given name");
            return default;
        }

        string jsonContent = File.ReadAllText(jsonPath);
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(jsonContent);
        return wrapper.Items;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }


}

