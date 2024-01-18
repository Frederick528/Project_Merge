using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExcelDataReader;
using UnityEngine;

public class TutorialDataDeserializer
{
    private static Dictionary<int, string[]> _dictionary = new ();

    private static void Init()
    {
        string fileName = "/Tutorial.xlsx";
        string filePath = Application.dataPath + fileName;
        
        using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            
            using (var reader =  ExcelReaderFactory.CreateReader(stream))
            {
                var table = reader.AsDataSet().Tables;

                for (int i = 0; i < table.Count; i++)
                {
                    for (int j = 0; j < table[i].Rows.Count; j++)
                    {
                        var row = table[i].Rows[j];
                        if (Int32.TryParse(row[0].ToString(), out int idx))
                        {
                            _dictionary.Add( idx,
                                new [] {row[1].ToString(), row[2].ToString(), row[3].ToString()}
                            );
                        }
                    }
                }
            }
        }
    }

    public static bool TryGetData(int key, out string[] row)
    {
        var result = true;
        if (_dictionary.Count == 0)
            Init();

        if (_dictionary.Keys.Contains(key))
        {
            row = _dictionary[key];
        }
        else
        {
            Debug.Log(key);
            foreach (var vailed in _dictionary.Keys)
            {
                Debug.Log("vailed : " + vailed);
            }
            row = new[] { "" };
            result =  false;
        }

        return result;
    }
}