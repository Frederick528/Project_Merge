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
            stream = File.OpenWrite(path);
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
            stream = File.OpenRead(path);
        }
        else if (saveData.dict == null)
        {
            Debug.Log(true);
            saveData.dict = new Dictionary<int, bool>(
                from key in CardDataDeserializer.Keys
                select new KeyValuePair<int, bool>(key, false)
            );
                
            
            Debug.Log(Application.persistentDataPath);

            stream = File.Create(path);
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