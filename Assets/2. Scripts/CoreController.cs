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
    private static CoreController _instance;
    public static int TurnCnt => _core.TurnCnt;
    public static int Date => _core.TurnCnt / 4;

    public static bool IsNightTime => _core.IsNightTime;
    public TMP_Text Hungriness;
    public TMP_Text Thirst;
    public TMP_Text Turn;
    public TMP_Text AP;

    private void Awake()
    {
        _core ??= new GameCore();
        _instance ??= this;
    }

    void Start()
    {
        _core.InitGame();
        Turn.text = _core.TurnCnt + "";
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
        _instance.Turn.text = _core.TurnCnt + "";

        //if (_core.IsDawn)
        //{
        //    EncounterManager.Occur();
        //}
        /*else */if (_core.IsMorning)
        {
            CardManager.ExpirationDateCheck();
        }

        if (_core.IsDayTime)
        {
            if (Random.Range(0, 10) > 7)
            {
                BearManager.Dispense();
            }
        }
        else
        {
            BearManager.BearLeave();
        }
    }
    private void OnDestroy()
    {
        _core.EndGame();
        _instance = null;
    }

    #region ModifyStatus
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
    #endregion

    public void CreateCard()
    {
        if (ModifyAP(-1))
            CardManager.CreateCard();
        else
            Debug.Log("AP가 부족합니다.");
    }

    public void SetDifficulty(ushort value)
    {
        _core.Difficulty = value;
    }
}
