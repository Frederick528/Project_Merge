using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    PlayerCtrl playerCtrl;
    public Slider hungerSlider;
    public Slider thirstSlider;
    // Start is called before the first frame update
    void Start()
    {
        playerCtrl = FindObjectOfType<PlayerCtrl>();
        hungerSlider.maxValue = playerCtrl.stat.maxHunger;
        thirstSlider.maxValue = playerCtrl.stat.maxThirst;
    }

    // Update is called once per frame
    void Update()
    {
        hungerSlider.value = playerCtrl.stat.curHunger;
        thirstSlider.value = playerCtrl.stat.curThirst;
    }
}
