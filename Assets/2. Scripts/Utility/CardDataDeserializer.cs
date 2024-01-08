using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ExcelDataReader;
using UnityEngine;

public class CardDataDeserializer
{
    private static Dictionary<int, CardData> _dictionary = null;
    private static List<int[]> _craftRules = new ();

    public static List<int[]> CraftRules => _craftRules;
    // Start is called before the first frame update

    private static Dictionary<int, CardData> CreateDictionary()
    {
        string fileName = "/0.8.xlsx";
        string filePath = Application.dataPath + fileName;

        Dictionary<int, CardData> cardDictionary = new Dictionary<int, CardData>();
        
        using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            using (var reader =  ExcelReaderFactory.CreateReader(stream))
            {
                var table = reader.AsDataSet().Tables;

                for (int i = 0; i < table.Count; i++)
                {
                    for (int j = 2; j < table[i].Rows.Count; j++)
                    {
                        var data = new CardData ();
                        var row = table[i].Rows[j];

                        data.KR = row[1].ToString();
                        data.EN = row[2].ToString();
                        if(Int32.TryParse(row[3].ToString(), out data.Date))
                            data.Date = data.Date == 0 ? Int32.MaxValue : data.Date;
                        if (!Int32.TryParse(row[4].ToString(), out data.Hunger))
                            data.Hunger = Int32.MinValue;
                        if (!Int32.TryParse(row[5].ToString(), out data.Thirst))
                            data.Thirst = Int32.MinValue;
                        data.Descript = row[6].ToString();
                        data.Effect = row[7].ToString();
                        data.Info = row[8].ToString();
                        
                        if(row[9].ToString() != "null")
                        {
                            var v = row[9].ToString().Split("+".Trim());
                            Array.Resize(ref v, v.Length + 1);
                            v[^1] = row[10].ToString();
                            
                            var craft = new int[v.Length];
                            for (int k = 0; k < v.Length; k++)
                            {
                                Int32.TryParse(v[k], out craft[k]);
                            }

                            if (!_craftRules.Contains(craft))
                            {
                                _craftRules.Add(craft);
                            }
                        }
                        data.CraftEffect = row[11].ToString();
                        
                        cardDictionary.Add(Convert.ToInt32(row[0].ToString()), data);
                        
                        //Debug.Log(row[0] + " " + row[11]);
                    }
                }
            }
        }

        foreach (var i in CraftRules)
        {
            var str = "";
            foreach (var c in i)
            {
                str += c + " ";
            }
            Debug.Log(str);
        }
        
        return cardDictionary;
    }

    public static bool TryGetData(int key, out CardData output)
    {
        _dictionary ??= CreateDictionary();
        var result = true;
        output = _dictionary[key];
        return result;
    }
}
 