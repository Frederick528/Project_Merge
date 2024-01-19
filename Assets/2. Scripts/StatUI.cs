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
    public Canvas statCanvas;
    public bool Status => _status;

    private Animator _anim;
    private bool _status = true;
    public bool status => _status;// true 표시 / false 비표시

    private bool _isClickable = true;
    // Start is called before the first frame update

    private void OnEnable()
    {
        statCanvas.worldCamera = GameObject.Find("UI_Camera").GetComponent<Camera>();
        if (!statUI.Background.TryGetComponent(out Button btn))
        {
            btn = statUI.Background.gameObject.AddComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                if (!_isClickable) return;
                if (_status)
                {
                    Exit();
                }
                else
                {
                    Enter();
                }
                //btn.onClick.RemoveAllListeners();
            });
        }
        if (!statUI.showImage.TryGetComponent(out Button showBtn))
        {
            showBtn = statUI.showImage.gameObject.AddComponent<Button>();
            showBtn.onClick.AddListener(() =>
            {
                if (!_isClickable) return;
                if (_status)
                {
                    Exit();
                }
                else
                {
                    Enter();
                }
                //btn.onClick.RemoveAllListeners();
            });
        }
        else
        {
            Enter();
        }

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

    public void Enter()
    {
        if (!_status)
        {
            _anim ??= this.GetComponent<Animator>();
            _anim.SetTrigger("Enter");
            _isClickable = false;
        }
        _status = true;
    }
    public void Exit()
    {
        //비표시 중일 때 애니메이션 재생 x
        if (_status)
        {
            _anim ??= this.GetComponent<Animator>();
            _anim.SetTrigger("Exit");
            _isClickable = false;
        }
        _status = false;
    }

    public override void AnimEvt()
    {
        _isClickable = true;
        //base.AnimEvt();
    }

    [Serializable]
    public struct StatUIGroup
    {
        // 0 == Hunger / 1 == Thirst / 2 == AP
        public TMP_Text[] Texts;
        public Image[] Hunger;
        // 0 == front / 1 == back
        // back - 감소 대기(보여주는 값)
        // front - 실제 감소(실제 스테이터스 값)
        // value = 잃은 값, 1- (max - crnt / max) (값이 1이 되면 꽉참.)

        // 0 == main / 1 == expected / 2 == fluctuation
        public Image[] Thirst;
        public Image Background;
        public Image showImage;
    }
}
