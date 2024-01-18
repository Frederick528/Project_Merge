using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExcelDataReader;
using UnityEngine;

public class TextDataDeserializer
{
    private static Dictionary<int, string> _dictionary = null;
    // Start is called before the first frame update

    private static Dictionary<int, string> CreateDictionary()
    {
        string fileName = "/TextData.xlsx";
        string filePath = Application.dataPath + fileName;

        Dictionary<int, string> cardDictionary = new Dictionary<int, string>();
        
        using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            using (var reader =  ExcelReaderFactory.CreateReader(stream))
            {
                var table = reader.AsDataSet().Tables;

                for (int i = 0; i < table.Count; i++)
                {
                    for (int j = 1; j < table[i].Rows.Count; j++)
                    {
                        var row = table[i].Rows[j];

                        cardDictionary.Add(j - 1, row[0].ToString());
                    }
                }
            }
        }
        
        return cardDictionary;
    }

    public static bool TryGetData(int key, out string output)
    {
        _dictionary ??= CreateDictionary();
        var result = true;
        output = _dictionary[key];
        return result;
    }
}