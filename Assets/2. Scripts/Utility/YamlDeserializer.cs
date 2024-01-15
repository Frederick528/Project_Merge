#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using YamlDotNet.Serialization;

public class YamlDeserializer
{
    public static PictorialData saveData = new ();
    public static void Serialize<T>(string path, T value)
    {
        var builder = new SerializerBuilder().Build();

        FileStream stream;

        if (File.Exists(path))
        {
            File.Delete(path);
            stream = File.Create(path);
        }
        else
        {
            stream = File.Create(path);
        }

        using (var writer = new StreamWriter(stream))
        {
            builder.Serialize(writer, value);
        }
        
        
        stream.Dispose();
    }
    
    public static T DeSerialize<T>(string path) where T : Dictionary<object, object>
    {
        T result = null;
        FileStream stream;

        if (File.Exists(path))
        {
#if UNITY_EDITOR
       
            File.Delete(path);
            if(saveData.dict != null)
            {
                saveData.dict.Clear();
                var v =
                    from key in CardDataDeserializer.Keys
                    select new KeyValuePair<int, bool>(key, false);
            
                foreach (var keyValuePair in v)
                {
                    saveData.dict.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }
            else
            {
                saveData.dict = new Dictionary<int, bool>(
                    from key in CardDataDeserializer.Keys
                    select new KeyValuePair<int, bool>(key, false)
                );
            }
            
            Serialize(path , saveData);
#endif
            
            stream = File.OpenRead(path);
        }
        else if (saveData.dict == null)
        {
            saveData.dict = new Dictionary<int, bool>(
                from key in CardDataDeserializer.Keys
                select new KeyValuePair<int, bool>(key, false)
            );
                
            
            Debug.Log(Application.persistentDataPath);
            Debug.Log(saveData.dict.Count);

            Serialize(path , saveData);
            stream = File.OpenRead(path);
            
        }
        else if (saveData.dict.Count == 0)
        {
            Debug.Log(true);
            var v =
                from key in CardDataDeserializer.Keys
                select new KeyValuePair<int, bool>(key, false);
            
            foreach (var keyValuePair in v)
            {
                saveData.dict.Add(keyValuePair.Key, keyValuePair.Value);
            }
                
            
            Debug.Log(Application.persistentDataPath);

            Serialize(path , saveData);
            stream = File.OpenRead(path);
        }
        else
        {
            Serialize(path , saveData);
            stream = File.OpenRead(path);
        }

        Deserializer deserializer = new Deserializer();
        
        using (var reader = new StreamReader(stream))
        {
            //첫줄 제거
            //Debug.Log();
            reader.ReadLine();
            result  = deserializer.Deserialize(reader) as T;
        }
        
        stream.Dispose();

        return result;
    }
}

public struct PictorialData
{
    public static string defaultFilePath = Application.persistentDataPath + "/Pictorial.yaml";
    public Dictionary<int, bool> dict;

    public void Init()
    {
        var v = YamlDeserializer.DeSerialize<Dictionary<object, object>>(defaultFilePath);
        var x = from row in v
            select new KeyValuePair<int, bool>
                (Convert.ToInt32(row.Key), row.Value.ToString().ToLower() == "true");
        dict = new Dictionary<int, bool>(x);
    }
    
    public void Add(int key, bool value)
    {
        dict ??= new Dictionary<int, bool>();

        if (!dict.Keys.Contains(key))
        {
            if (!dict.TryAdd(key, value))
                throw new Exception("값 추가에 실패 했습니다. 키와 값을 다시 확인해 주세요");
        }
        else if (!Modify(key, value))
        {
            throw new Exception("값 수정에 실패 했습니다. 키와 값을 다시 확인해 주세요");
        }
    }
    public bool Modify(int key, bool value)
    {
        dict ??= new Dictionary<int, bool>();
        var result = true;

        if (!dict.Keys.Contains(key))
            result = false;
        else
            dict[key] = value;
        
        return result;
    }

    public bool GetValue(int key)
    {
        if(dict == null)
            Init();
        if (!dict.Keys.Contains(key))
            throw new Exception($"키에 해당하는 값이 없습니다. : {key}");

        return dict[key];
    }
}