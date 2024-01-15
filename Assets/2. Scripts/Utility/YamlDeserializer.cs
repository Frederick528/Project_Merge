#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        
        stream.Write(Encoding.UTF8.GetBytes("---\n"));
        
        
        using (var writer = new StreamWriter(stream))
        {
            builder.Serialize(writer, value);
        }
        
        //stream.Write(Encoding.UTF8.GetBytes("..."));
        
        stream.Dispose();
    }
    
    public static List<Dictionary<object, object>> DeSerialize(string path)
    {
        List<Dictionary<object, object>> result = new List<Dictionary<object, object>> ();
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
            
            saveData.limit = new Dictionary<int, bool>(
                from key in CardDataDeserializer.Keys
                where key / 10 == 101
                select new KeyValuePair<int, bool>(key % 10, false)
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
            
            var w = new Dictionary<int, bool>(
                from key in CardDataDeserializer.Keys
                where key / 10 == 101
                select new KeyValuePair<int, bool>(key % 10, false)
            );
            
            foreach (var keyValuePair in v)
            {
                saveData.dict.Add(keyValuePair.Key, keyValuePair.Value);
            }

            foreach (var keyValuePair in w)
            {
                saveData.limit.Add(keyValuePair.Key, keyValuePair.Value);
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
            var dict = deserializer.Deserialize(reader) as Dictionary<object, object>;
            foreach (var keyValueFair in dict)
            {
                result.Add(keyValueFair.Value as Dictionary<object, object>);
            }
        }
        
        stream.Dispose();

        return result;
    }
}

public struct PictorialData
{
    public static string defaultFilePath = Application.persistentDataPath + "/Pictorial.yaml";
    public Dictionary<int, bool> dict;
    public Dictionary<int, bool> limit;

    public void Init()
    {
        limit ??=  new Dictionary<int, bool>(
            from x in CardDataDeserializer.Keys
            where x / 10 == 101
            select new KeyValuePair<int, bool>(x % 10, false)
        );
        dict = new Dictionary<int, bool>(
            from key in CardDataDeserializer.Keys
            select new KeyValuePair<int, bool>(key, false)
        );
        
        //var v = YamlDeserializer.DeSerialize<Dictionary<object, object>>(defaultFilePath);
        var v = YamlDeserializer.DeSerialize(defaultFilePath);
        Debug.Log(v.Count);
        // var x = from row in v
        //     select new KeyValuePair<int, bool>
        //         (Convert.ToInt32(row.Key), row.Value.ToString().ToLower() == "true");
        // dict = new Dictionary<int, bool>(x);
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
    
    public bool ModifyLimit(int key, bool value)
    {
        limit ??= new Dictionary<int, bool>();
        var result = true;

        if (!limit.Keys.Contains(key))
            result = false;
        else
            limit[key] = value;
        
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
    public bool GetValueFromLimit(int key)
    {
        if(limit == null)
            Init();
        if (!limit.Keys.Contains(key))
            throw new Exception($"키에 해당하는 값이 없습니다. : {key}");

        return limit[key];
    }
}