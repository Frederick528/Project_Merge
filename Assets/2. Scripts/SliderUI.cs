using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    PlayerCtrl playerCtrl;
    public Slider hungerSlider;
    public Slider changeHungerSlider;
    public Slider thirstSlider;
    public Slider changeThirstSlider;
    // Start is called before the first frame update

    void Start()
    {
        playerCtrl = StatManager.instance.playerCtrl;
        hungerSlider.maxValue = playerCtrl.stat.maxHunger;
        thirstSlider.maxValue = playerCtrl.stat.maxThirst;
    }

    // Update is called once per frame
    void Update()
    {
        hungerSlider.value = playerCtrl.stat.curHunger;
        changeHungerSlider.value = playerCtrl.stat.curHunger - CoreController.Difficulty;
        thirstSlider.value = playerCtrl.stat.curThirst;
        changeThirstSlider.value = playerCtrl.stat.curThirst - CoreController.Difficulty;
    }
}
