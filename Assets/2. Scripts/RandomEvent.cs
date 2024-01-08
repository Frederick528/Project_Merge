using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvent : MonoBehaviour
{
    [SerializeField]
    GameObject incounter1, incounter2, incounter3;

    static List<GameObject> IncounterList = new List<GameObject>();

    private void Start()
    {
        IncounterList.Add(incounter1);
        IncounterList.Add(incounter2);
        IncounterList.Add(incounter3);
    }

    public static void SpawnPlay()
    {
        if (IncounterList.Count > 0)
        {
            GameObject selectedEvent = IncounterList[0]; 
            selectedEvent.SetActive(true);
            Instantiate(selectedEvent);
            IncounterList.RemoveAt(0); 
        }
    }
}
