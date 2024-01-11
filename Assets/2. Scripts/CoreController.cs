using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

//relay between GameCore and UnityAPI
public class CoreController : MonoBehaviour
{
    // Start is called before the first frame update
    private static GameCore _core;
    private static CoreController _instance;
    public static int TurnCnt => _core.TurnCnt;
    public static int Date => _core.TurnCnt / 4;

    public static float Difficulty;

    public static bool IsNightTime => _core.IsNightTime;
    public Light Light;
    public GameObject Notice;
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
        if(BearManager.Count > 0 )
        {
            Debug.Log("곰들이 아직 남아있습니다.");

            var v = from c in CardManager.Cards
                where c.ID is > 1999 and < 2999
                select c;
            
            if(v.Count() == 0)
            {
                Debug.Log("하지만 공격 가능한 수단이 없습니다.");
                
                var foods = from c in CardManager.Cards
                    where c.ID is < 1999 and > 999
                    select c;

                CardManager.DestroyCard(foods);
                
                Debug.Log("곰들이 남은 음식들을 모조리 먹어치웠습니다.");
            }
            else
            {
                Debug.Log("턴을 종료 할 수 없습니다.");
                return;
            }
            
        }
        
        if (!_core.TurnChange())
        {
            // 게임 오버
            _instance.Notice = _instance.Notice.activeInHierarchy ? _instance.Notice : Instantiate(_instance.Notice);
            _instance.Notice.SetActive(true);
            var button = _instance.Notice.GetComponentInChildren<Button>();
            button.onClick.AddListener(
                () =>
                {
                    _core.InitGame();
                    _instance.Notice.SetActive(false);
                });

        }
        _instance.Turn.text = _core.TurnCnt + "";

        if (_core.IsDawn)
        {
            _core.Difficulty = (ushort)(1 * (Date+1));
            Difficulty = _core.Difficulty;
            BearManager.Notice("새벽이 밝았습니다!");
            //EncounterManager.Occur();
        }
        else if (_core.IsMorning)
        {
            _core.Difficulty = 0;
            Difficulty = _core.Difficulty;
            CardManager.ExpirationDateCheck();
            BearManager.Notice("좋은 아침을 맞이하라!");
        }

        if (_core.IsDayTime)
        {
            if (Random.Range(0, 10) > 7 || _core.Date == 1)
            {
                BearManager.Dispense();
            }
            BearManager.Notice("나잇타임... 데이타임!");
        }
        else if (_core.IsNightTime)
        {
            BearManager.Notice("황혼이 저뭅니다!");
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
