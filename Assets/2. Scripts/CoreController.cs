using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    public static int bearFlag = 0;

    public static bool IsDawn => _core.IsDawn;
    public static bool IsMorning => _core.IsMorning;
    public static bool IsDayTime => _core.IsDayTime;
    public static bool IsNightTime => _core.IsNightTime;

    public static string Time => $"{_core.Date + 1} 일차 " + (IsDawn ? "새벽" : IsDayTime ? "점심" : IsMorning ? "아침" : "저녁") ;
    
    public Image Clock;
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

        //세이브 파일이 없을 경우 새로 딕셔너리를 만들어 거기서 생성
        
        YamlDeserializer.saveData.Init();
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

        var lightAnim = _instance.Light.GetComponent<Animator>();
        foreach (var param in lightAnim.parameters)
        {
            if(param.type == AnimatorControllerParameterType.Trigger)
                lightAnim.ResetTrigger(param.name);
        }
        
        if (_core.IsDawn)
        {
            _core.Difficulty = (ushort)(1 * (Date+1));
            Difficulty = _core.Difficulty;
            
            
            lightAnim.SetTrigger("Dawn");
            //EncounterManager.Occur();
        }
        else if (_core.IsMorning)
        {
            _core.Difficulty = 0;
            Difficulty = _core.Difficulty;
            CardManager.ExpirationDateCheck();
            
            lightAnim.SetTrigger("Morning");
        }
        if (_core.IsDayTime)
        {
            bearFlag = Random.Range(0, 10);
            
            lightAnim.SetTrigger("DayTime");
        }
        else if (_core.IsNightTime)
        {
            BearManager.BearLeave();
            
            lightAnim.SetTrigger("Night");
        }
        
        _instance.Clock.gameObject.SetActive(true);
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
