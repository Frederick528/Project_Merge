using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Event : MonoBehaviour
{
    public GameObject incounter,fixincounter, result1, result2,result3, nextturn, closebtn, openbtn;

    public int i = 0;

    void Update()
    {
        
    }
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

    public void Result3()
    {
        incounter.SetActive(false);
        result3.SetActive(true);
    }

    public void Close()
    {
        result1.SetActive(false);
        result2.SetActive(false);
        closebtn.SetActive(false);
    }

    public void IncounterClose()
    {
        incounter.SetActive(false);
        fixincounter.SetActive(false);
        nextturn.SetActive(false);
        closebtn.SetActive(false);
        openbtn.SetActive(true);
    }

    public void IncounterOpen() 
    {
        incounter.SetActive(true);
        fixincounter.SetActive(true);
        nextturn.SetActive(true);
        closebtn.SetActive(true);
        openbtn.SetActive(false);
    }

    void NoQuestRoute()
    {
        incounter.SetActive(false);
        result1.SetActive(true);
        i += 5;
    }

    
}
