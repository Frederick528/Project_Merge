using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ReadSpreadSheet : MonoBehaviour
{
    private static string dataGS;
    private static Dictionary<int, ArtifactData> dataDtct = null;
    public static IEnumerator LoadData(string address, string range, ulong sheetID)
    {
        using (UnityWebRequest www = UnityWebRequest.Get($"{address}/export?format=csv&range={range}&gid={sheetID}"))
        {
            yield return www.SendWebRequest();
            dataGS = www.downloadHandler.text;


            //if (www.isDone)
            //{
            //    CreateDB();
            //}
        }
    }
    private static Dictionary<int, ArtifactData> CreateDB()
    {
        string[] rows = dataGS.Split("\n");
        Dictionary<int, ArtifactData> artifactDB = new Dictionary<int, ArtifactData>();
        foreach (string row in rows)
        {
            string[] cells = row.Split(",");
            var data = new ArtifactData();
            data.Name = cells[1];
            data.Description = cells[2];
            data.Type = Boolean.TryParse(cells[3], out bool result);    //TryParse = no error / Parse & Convert = error

            artifactDB.Add(Convert.ToInt32(cells[0].ToString()), data);
        }
        return artifactDB;
    }
    public static bool TryGetData(int key, out ArtifactData data)
    {
        dataDtct ??= CreateDB();
        var result = true;
        data = dataDtct[key];
        return result;
    }


}