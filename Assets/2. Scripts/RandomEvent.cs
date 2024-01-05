using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvent : MonoBehaviour
{
    [SerializeField]
    GameObject incounter1, incounter2, incounter3;

    int Random_event;

    List<GameObject> IncounterList = new List<GameObject>();

    private void Start()
    {
        IncounterList.Add(incounter1);
        IncounterList.Add(incounter2);
        IncounterList.Add(incounter3);
    }

    private void Update()
    {
        SpawnPlay();
    }

    void SpawnPlay()
    {
        bool keydown = Input.GetKeyUp(KeyCode.Space);

        if (keydown)
        {
            for (int i = 0; i < IncounterList.Count; i++)


            {
                Random_event = Random.Range(0, 3);

                switch (Random_event)
                {
                    case 0:
                        incounter1.SetActive(true);
                        Instantiate(incounter1);
                        IncounterList.Remove(incounter1);
                        break;
                    case 1:
                        incounter2.SetActive(true);
                        Instantiate(incounter2);
                        IncounterList.Remove(incounter2);
                        break;
                    case 2:
                        incounter3.SetActive(true);
                        Instantiate(incounter3);
                        IncounterList.Remove(incounter3);
                        break;
                }
            }
        }
    }
}
