using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using YamlDotNet.Serialization;

public class YamlDeserializer
{
    public void Serialize<T>(string path, T value)
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
    
    public T DeSerialize<T>(string path) where T : class 
    {
        T result = null;
        var builder = new DeserializerBuilder().Build();

        FileStream stream;

        if (File.Exists(path))
        {
            stream = File.OpenRead(path);
        }
        else
        {
            throw new Exception($"파일이 존재 하지 않습니다 :  { path }");
        }

        using (var reader = new StreamReader(stream))
        {
            result = builder.Deserialize(reader, typeof(T)) as T;
        }
        
        stream.Dispose();

        return result;
    }
}

public struct PictorialData
{
    public Dictionary<int, bool> dict;
    
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
}