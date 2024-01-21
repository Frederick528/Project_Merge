using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private Animator anim;
    private ShopController shopController;
    // Start is called before the first frame update
    private void Start()
    {
        shopController = GameObject.Find("ShopCanvas").GetComponent<ShopController>();
    }

    private void OnEnable()
    {
        GetComponentInChildren<TMP_Text>().text = CoreController.Time ;
        anim ??= GetComponent<Animator>();
        
        if(!GameManager.Instance.isTutorial)
            BearManager._turnSkip.interactable = false;

        SoundManager.instance.Play("Sounds/Effect/NextTurnSound");
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
            shopController.SetRandomButtonImg();
            BearManager.Notice("상인이 찾아온것 같습니다");
        }
        if (CoreController.IsNightTime)
        {
            if (GameManager.Instance.ArtifactDict[9004])
            {
                if (CoreController.bearFlag > 5)
                {
                    BearManager.Dispense();
                    SoundManager.instance.Play("Sounds/Effect/BearAppearSound", Sound.Bear);
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
                    SoundManager.instance.Play("Sounds/Effect/BearAppearSound", Sound.Bear);
                }
                else
                {
                    BearManager.Notice("나잇타임... 데이타임!");
                }
            }
            
            BearManager.Notice("곰들이 찾아왔습니다.");
        }
    }
}
