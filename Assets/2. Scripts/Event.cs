using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Event : MonoBehaviour
{
    public GameObject incounter, result1, result2, close, turnbutton;
   public void Result1()
    {
        incounter.SetActive(false);
        result1.SetActive(true);
    }

    public void Result2()
    { 
        incounter.SetActive(false);
        result2.SetActive(true);
    }

    public void Close()
    {
        result1.SetActive(false);
        result2.SetActive(false);
        turnbutton.SetActive(true);
    }
}
