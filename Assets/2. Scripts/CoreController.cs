using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

//relay between GameCore and UnityAPI
public class CoreController : MonoBehaviour
{
    // Start is called before the first frame update
    private static GameCore _core;

    public static bool IsDayTime => _core.IsDayTime;
    public TMP_Text Hungriness;
    public TMP_Text Thirst;
    public TMP_Text AP;

    private void Awake()
    {
        _core ??= new GameCore();
    }

    void Start()
    {
        _core.InitGame();
        
        for (int i = 0; i < 2; i++)
        {
            CardManager.CreateCard();
        }
    }

    private void Update()
    {
        Hungriness.text = _core.Status.curHunger + "";
        Thirst.text = _core.Status.curThirst + "";
        AP.text = _core.Status.curAp + "";
    }
    public static void TurnChange()
    {
        _core.TurnChange();
    }
    private void OnDestroy()
    {
        _core.EndGame();
    }
    
    public static bool ModifyAP()
    {
        return ModifyAP(1);
    }
    public static bool ModifyAP(int amount)
    {
        return _core.ModifyAP(amount);
    }
    public static bool ModifyHunger()
    {
        return ModifyHunger(1);
    }
    public static bool ModifyHunger(int amount)
    {
        return _core.ModifyHunger(amount);
    }
    public static bool ModifyThirst()
    {
        return ModifyThirst(1);
    }
    public static bool ModifyThirst(int amount)
    {
        return _core.ModifyThirst(amount);
    }

    public void CreateCard()
    {
        ModifyAP(-1);
        CardManager.CreateCard();
    }
}
