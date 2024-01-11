using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvent : MonoBehaviour
{
    [SerializeField]
    GameObject incounter1, incounter2, incounter3, incounter4, incounter5, 
               incounter6, incounter7, incounter8, incounter9, incounter10,
               incounter11, incounter12, incounter13, incounter14, incounter15;
    static int Random_event;

    static List<GameObject> IncounterList = new();

    private void Start()
    {
        IncounterList.Add(incounter1);
        IncounterList.Add(incounter2);
        IncounterList.Add(incounter3);
        IncounterList.Add(incounter4);
        IncounterList.Add(incounter5); 
        IncounterList.Add(incounter6);
        IncounterList.Add(incounter7);
        IncounterList.Add(incounter8);
        IncounterList.Add(incounter9);
        IncounterList.Add(incounter10);
        IncounterList.Add(incounter11);
        IncounterList.Add(incounter12);
        IncounterList.Add(incounter13);
        IncounterList.Add(incounter14);
        IncounterList.Add(incounter15);
    }

    public static void SpawnPlay()
    {
        if ((Turn.count > 5 && Turn.count < 79) && (Turn.count / 4) % 5 == 4)
        {
            return;
        }

        if (IncounterList.Count > 0)
        {
            Random_event = Random.Range(0, IncounterList.Count);

            GameObject selectedEvent = IncounterList[Random_event];
            selectedEvent.SetActive(true);
            var v = selectedEvent.GetComponentInChildren<RectTransform>();

            print(selectedEvent.name.ToCharArray()[^2] + "");
            v.gameObject.SetActive(true);
            //Instantiate(selectedEvent);
            //IncounterList.Remove(selectedEvent);
        }
    }
}