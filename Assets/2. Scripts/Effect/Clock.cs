using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    private void Start()
    {
    }

    private void OnEnable()
    {
        GetComponentInChildren<TMP_Text>().text = CoreController.Time ;
        anim ??= GetComponent<Animator>();
        
        if (CoreController.IsDawn)
        {
            anim.SetTrigger("Dawn");
        }
        if (CoreController.IsMorning)
        {
            anim.SetTrigger("Morning");
        }
        if (CoreController.IsDayTime)
        {
            anim.SetTrigger("DayTime");
        }
        if (CoreController.IsNightTime)
        {
            anim.SetTrigger("NightTime");
        }
    }

    public void AnimEvt()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        
        this.gameObject.SetActive(false);
        
        if (CoreController.IsDawn)
        {
            BearManager.Notice("새벽이 밝았습니다!");
        }
        if (CoreController.IsMorning)
        {
            BearManager.Notice("좋은 아침을 맞이하라!");
        }
        if (CoreController.IsDayTime)
        {
            if ( CoreController.bearFlag > 3)
            {
                BearManager.Dispense();
            }
            else
            {
                BearManager.Notice("나잇타임... 데이타임!");
            }
        }
        if (CoreController.IsNightTime)
        {
            BearManager.Notice("황혼이 저뭅니다!");
        }
    }
}
