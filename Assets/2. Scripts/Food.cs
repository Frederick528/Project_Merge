using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float expirationDate;
    public float hunger;
    public float thirst;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            expirationDate -= 1;
        }

        if (expirationDate <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void EatFood()
    {
        StatManager.instance.playerCtrl.stat.curHunger += hunger;
        StatManager.instance.playerCtrl.stat.curThirst += thirst;
        Destroy(gameObject);
    }
}
