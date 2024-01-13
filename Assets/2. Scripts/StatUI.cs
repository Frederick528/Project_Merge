using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : EffectBase
{
    //PlayerCtrl playerCtrl;
    // public Slider hungerSlider;
    // public Slider changeHungerSlider;
    // public Slider thirstSlider;
    // public Slider changeThirstSlider;

    public StatUIGroup statUI;

    private Animator _anim;
    // Start is called before the first frame update

    private void OnEnable()
    {
        if (!statUI.Background.TryGetComponent(out Button btn))
        {
            btn = statUI.Background.gameObject.AddComponent<Button>();
        }
        
        btn.onClick.AddListener(() =>
        {
            Exit();
            btn.onClick.RemoveAllListeners();
        });
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            // hungerSlider.value = playerCtrl.stat.curHunger;
            // thirstSlider.value = playerCtrl.stat.curThirst;
            // changeHungerSlider.value = playerCtrl.stat.curHunger - CoreController.Difficulty;
            // changeThirstSlider.value = playerCtrl.stat.curThirst - CoreController.Difficulty;
        }
        catch
        {
            return;
        }
        finally
        {
            // hungerSlider.value = playerCtrl.stat.curHunger;
            // thirstSlider.value = playerCtrl.stat.curThirst;
            
        }
    }

    public void Exit()
    {
        _anim ??= this.GetComponent<Animator>();
        _anim.SetTrigger("Exit");
    }

    [Serializable]
    public struct StatUIGroup
    {
        // 0 == Hunger / 1 == Thirst / 2 == AP
        public TMP_Text[] Texts;
        public Image[] Hunger;
        // 0 == front / 1 == back
        // back - 감소 대기
        // front - 실제 감소
        // value = 잃은 값, 1- (max - crnt / max) (값이 1이 되면 꽉참.)
        public Image[] Thirst;
        public Image Background;
    }
}
