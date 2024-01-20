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
        
        if(!GameManager.Instance.isTutorial)
            BearManager._turnSkip.interactable = false;
        
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
        if (GameManager.Instance.isTutorial)
        {
            yield break;
        }
        yield return new WaitForSecondsRealtime(0.5f);
        
        this.gameObject.SetActive(false);
        
        if (CoreController.IsDawn)
        {
            BearManager.Notice("새벽이 밝았습니다!", () =>
            {
                Notice.Dispose();

                GameManager.Instance.StartCoroutine(Turn.Instance.Encounter());
                
            });
        }
        if (CoreController.IsMorning)
        {
            BearManager.Notice("좋은 아침을 맞이하라!");
        }
        if (CoreController.IsDayTime)
        {
            var v = CardManager.CreateCard(5000);
        }
        if (CoreController.IsNightTime)
        {
            if (GameManager.Instance.ArtifactDict[9004])
            {
                if (CoreController.bearFlag > 5)
                {
                    BearManager.Dispense();
                }
                else
                {
                    BearManager.Notice("나잇타임... 데이타임!");
                }
            }
            else
            {
                if ( CoreController.bearFlag > 1)
                {
                    BearManager.Dispense();
                }
                else
                {
                    BearManager.Notice("나잇타임... 데이타임!");
                }
            }
            
            BearManager.Notice("황혼이 저뭅니다!");
        }
    }
}
