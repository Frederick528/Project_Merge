using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvent : MonoBehaviour
{
    [SerializeField]
    GameObject incounter1, incounter2, incounter3;

    static int Random_event; // static���� ����

    static List<GameObject> IncounterList = new List<GameObject>(); // static���� ����

    private void Start()
    {
        IncounterList.Add(incounter1);
        IncounterList.Add(incounter2);
        IncounterList.Add(incounter3);
    }

    public static void SpawnPlay() // static���� ����
    {
        if (IncounterList.Count > 0)
        {
            Random_event = Random.Range(0, IncounterList.Count);

            GameObject selectedEvent = IncounterList[Random_event];
            selectedEvent.SetActive(true);
            Instantiate(selectedEvent);
            IncounterList.Remove(selectedEvent);
        }
    }
}