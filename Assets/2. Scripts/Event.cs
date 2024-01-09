using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Event : MonoBehaviour
{
    public GameObject incounter,fixincounter, result1, result2,result3, blockUI, closebtn, openbtn;

    [SerializeField]
    Button nextBtn;

    public static int Quest = 0;

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
        if (result3 != null)
            result3.SetActive(false);
        blockUI.SetActive(false);
        closebtn.SetActive(false);
        GameManager.cardCanvasOn = false;
        nextBtn.interactable = true;
    }

    public void IncounterClose()
    {
        incounter.SetActive(false);
        fixincounter.SetActive(false);
        blockUI.SetActive(false);
        closebtn.SetActive(false);
        openbtn.SetActive(true);
        GameManager.cardCanvasOn = false;
    }

    public void IncounterOpen() 
    {
        incounter.SetActive(true);
        fixincounter.SetActive(true);
        blockUI.SetActive(true);
        closebtn.SetActive(true);
        openbtn.SetActive(false);
        GameManager.cardCanvasOn = true;
    }

    public void NoQuest()
    {
        incounter.SetActive(false);
        result1.SetActive(true);
        Quest += 1;
    }


}
