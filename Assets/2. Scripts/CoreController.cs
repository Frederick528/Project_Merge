using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

//relay between GameCore and UnityAPI
public class CoreController : MonoBehaviour
{
    // Start is called before the first frame update
    private static GameCore _core;
    private static CoreController _instance;

    public static GameCore Core => _core;

    public static int TurnCnt => _core.TurnCnt;
    public static int Date => _core.TurnCnt / 4;
    public static int bearFlag = 0;

    public static bool IsDawn => _core.IsDawn;
    public static bool IsMorning => _core.IsMorning;
    public static bool IsDayTime => _core.IsDayTime;
    public static bool IsNightTime => _core.IsNightTime;

    public static string Time => $"{_core.Date + 1} 일차 " + (IsDawn ? "새벽" : IsDayTime ? "점심" : IsMorning ? "아침" : "저녁") ;
    public static CoreController Instance => _instance;

    public static int ArtifactAddHunger = 0;
    public static int ArtifactAddThirst = 0;

    public static int ArtifactSubHunger = 0;
    public static int ArtifactSubThirst = 0;

    //private static bool overflowHunger;
    //private static bool overflowThirst;

    private static int addTempHungerFluctuation;
    private static int addTempThirstFluctuation;

    private static int tempHungerFluctuation;
    private static int tempThirstFluctuation;

    public StatUI StatUICanvas;
    public Image Clock;
    public Light Light;
    public GameObject Notice;
    public TMP_Text Turn;

    #region ReactiveProperty
    //public static readonly ReactiveProperty<float> Difficulty = new ();
    public static readonly ReactiveProperty<int> HungerDifficulty = new ();
    public static readonly ReactiveProperty<int> ThirstDifficulty = new ();
    
    public static readonly ReactiveProperty<int> HungerFluctuation = new ();
    public static readonly ReactiveProperty<int> ThirstFluctuation = new ();

    private static readonly ReactiveProperty<int> _hunger = new ();
    private static readonly ReactiveProperty<int> _thirst = new ();
    private static readonly ReactiveProperty<int> _ap = new();
    #endregion

    private void Awake()
    {
        _core ??= new GameCore();
        _instance ??= this;

        if (SceneManager.GetActiveScene().name != "Tutorial")
        {
            if (StatUICanvas.gameObject is { activeSelf: true, activeInHierarchy: false })
            {
                StatUICanvas = Instantiate(StatUICanvas);
                StatUICanvas.gameObject.SetActive(false);
            }
        }
    }

    void Start()
    {
        _core.InitGame();
        _ap.Value = _core.Status.curAp;
        _hunger.Value = (int)_core.Status.curHunger;
        _thirst.Value = (int)_core.Status.curThirst;
        if (!GameManager.Instance.isTutorial)
            StatUICanvas.gameObject.SetActive(true);

        if (!GameManager.Instance.isTutorial)
            Turn.text = _core.TurnCnt + "";
        for (int i = 0; i < 2 && !GameManager.Instance.isTutorial; i++)
        {
            CardManager.CreateCard();
        }

        //세이브 파일이 없을 경우 새로 딕셔너리를 만들어 거기서 생성
        if (!GameManager.Instance.isTutorial)
        {
            HungerDifficulty.Subscribe(x =>
            {
                //StatUICanvas.gameObject.SetActive(true);
                StatUICanvas.statUI.Hunger[1].fillAmount = 1 - (x / _core.Status.maxHunger);
                StatManager.instance.inGameHunger[1].fillAmount = 1 - (x / _core.Status.maxHunger);

                // if (x == 0) return;
                //     _instance.StatUICanvas.gameObject.SetActive(true);
                // _core.Difficulty = (ushort)x;
                // _instance.StatUICanvas.statUI.Hunger[1].fillAmount += x/ _core.Status.maxHunger;
                // _instance.StatUICanvas.statUI.Thirst[1].fillAmount += x/ _core.Status.maxThirst;
            });
            ThirstDifficulty.Subscribe(x =>
            {
                //StatUICanvas.gameObject.SetActive(true);
                StatUICanvas.statUI.Thirst[1].fillAmount = 1 - (x / _core.Status.maxThirst);
                StatManager.instance.inGameThirst[1].fillAmount = 1 - (x / _core.Status.maxThirst);
            });
            HungerFluctuation.Subscribe(x =>
            {
                StatUICanvas.statUI.Hunger[2].fillAmount = 1 - (x / _core.Status.maxHunger);
                StatManager.instance.inGameHunger[2].fillAmount = 1 - (x / _core.Status.maxHunger);

            });
            ThirstFluctuation.Subscribe(x =>
            {
                StatUICanvas.statUI.Thirst[2].fillAmount = 1 - (x / _core.Status.maxThirst);
                StatManager.instance.inGameThirst[2].fillAmount = 1 - (x / _core.Status.maxThirst);
            });
            _hunger.Subscribe(x =>
            {
                //StatUICanvas.gameObject.SetActive(true);
                var v = /*1 - ((_core.Status.maxHunger - x) / _core.Status.maxHunger)*/x / _core.Status.maxHunger;
                StatUICanvas.statUI.Hunger[0].fillAmount = v;
                StatManager.instance.inGameHunger[0].fillAmount = v;
                StatUICanvas.statUI.Texts[0].text = _core.Status.curHunger + "";
            });
            _thirst.Subscribe(x =>
            {
                //StatUICanvas.gameObject.SetActive(true);
                StatUICanvas.statUI.Thirst[0].fillAmount =
                    /*1 - ((_core.Status.maxThirst - x) / _core.Status.maxThirst)*/x / _core.Status.maxHunger;
                StatManager.instance.inGameThirst[0].fillAmount = x / _core.Status.maxThirst;
                StatUICanvas.statUI.Texts[1].text = _core.Status.curThirst + "";
                //     if (x != 0)
                //         _instance.StatUICanvas.gameObject.SetActive(true);
                //     var v = (_core.Status.maxHunger - x) / _core.Status.maxHunger;
                //     _instance.StatUICanvas.statUI.Hunger[0].fillAmount = v;
                //     _instance.StatUICanvas.statUI.Texts[0].text = _core.Status.curHunger + "";
                // });
                // _thirst.Subscribe(x =>
                // {
                //     if (x != 0)
                //         _instance.StatUICanvas.gameObject.SetActive(true);
                //     _instance.StatUICanvas.statUI.Thirst[0].fillAmount =
                //        (_core.Status.maxHunger - x) / _core.Status.maxHunger;
                //     _instance.StatUICanvas.statUI.Texts[1].text = _core.Status.curThirst + "";
            });
            _ap.Subscribe(x =>
            {
                // if (x != 0)
                //     StatUICanvas.gameObject.SetActive(true);
                //
                _instance.StatUICanvas.statUI.Texts[2].text =
                    $"[  {x} / {_core.Status.maxAp}  ]";
                StatManager.instance.text.text = 
                    $"{x} / {_core.Status.maxAp}";
            });
        }
    }

    private void Update()
    {
    }
    public void TurnChange()
    {
        if (_instance.StatUICanvas.status)
        {
            if (!GameManager.Instance.isTutorial)
                _instance.StatUICanvas.Exit();
        }

        if (GameManager.Instance.isTutorial)
        {
            var lAinm = _instance.Light.GetComponent<Animator>();
            foreach (var param in lAinm.parameters)
            {
                if(param.type == AnimatorControllerParameterType.Trigger)
                    lAinm.ResetTrigger(param.name);
            }
            
            lAinm.SetTrigger("Dawn");
            TutorialManager.WaitButtonCallBack = true;
            return;
        }

        var merchant = CardManager.Cards.Select(x => x);
        if (merchant.Count(x => x.ID >= 5000) > 0)
        {
            CardManager.DestroyCard(
                merchant.Select(x => x).Where(x => x.ID > 5000));
        }
        
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
                
                BearManager.Notice("공격 가능한 수단이 없어\n곰들이 음식을 모조리 먹어치웠습니다.");
            }
            else
            {
                Debug.Log("턴을 종료 할 수 없습니다.");
                BearManager.Notice("곰들이 아직 남아 있어\n턴을 종료 할 수 없습니다!");
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
            return;
        }
        _instance.Turn.text = _core.TurnCnt + "";

        var lightAnim = _instance.Light.GetComponent<Animator>();
        foreach (var param in lightAnim.parameters)
        {
            if(param.type == AnimatorControllerParameterType.Trigger)
                lightAnim.ResetTrigger(param.name);
        }

        if (GameManager.Instance.ArtifactDict[9000])
        {
            CardManager.CreateCard(1020);
        }
        if (GameManager.Instance.ArtifactDict[9001])
        {
            float rand = Random.Range(0, 0.99f);
            if (rand < 0.5f) CardManager.CreateCard(2010);
            else CardManager.CreateCard(2020);
        }
        if (GameManager.Instance.ArtifactDict[9002])
        {
            CardManager.CreateCard(1010);
        }
        if (GameManager.Instance.ArtifactDict[9011])
        {
            float rand = Random.Range(0, 0.99f);
            if (rand < 0.25f) CardManager.CreateCard(1011);
            else if (rand < 0.5f) CardManager.CreateCard(1021);
            else if (rand < 0.75f) CardManager.CreateCard(2011);
            else CardManager.CreateCard(2021);
        }

        _core.HungerDifficulty = 0;
        _core.ThirstDifficulty = 0;

        if (GameManager.Instance.ArtifactDict[9009] && !GameManager.Instance.ArtifactDict[9005])
        {
            _core.ThirstDifficulty += ArtifactSubThirst;
            if (!_core.IsMorning)
                ModifyThirst(-ArtifactSubThirst);
            if (!_core.IsDawn)
            {
                ThirstDifficulty.Value += _core.ThirstDifficulty;
                ThirstFluctuation.Value += _core.ThirstDifficulty;
            }
        }
        if (GameManager.Instance.ArtifactDict[9010] && !GameManager.Instance.ArtifactDict[9006])
        {
            _core.HungerDifficulty += ArtifactSubHunger;
            if (!_core.IsMorning)
                ModifyHunger(-ArtifactSubHunger);
            if (!_core.IsDawn)
            {
                HungerDifficulty.Value += _core.HungerDifficulty;
                HungerFluctuation.Value += _core.HungerDifficulty;
            }
        }

        if (_core.IsDawn)
        {
            _core.HungerDifficulty += (int)(Mathf.Log10(Date + 2) * 10 + 10);
            _core.ThirstDifficulty += (int)(Mathf.Log10(Date + 2) * 10 + 10);
            HungerDifficulty.Value += _core.HungerDifficulty;
            HungerFluctuation.Value += _core.HungerDifficulty;
            ThirstDifficulty.Value += _core.ThirstDifficulty;
            ThirstFluctuation.Value += _core.ThirstDifficulty;
            

            lightAnim.SetTrigger("Dawn");
            BearManager.BearLeave();
            //EncounterManager.Occur();
        }
        else if (_core.IsMorning)
        {
            //_core.HungerDifficulty = 0;
            //_core.ThirstDifficulty = 0;
            CardManager.ExpirationDateCheck();
            
            lightAnim.SetTrigger("Morning");
        }
        if (_core.IsDayTime)
        {
            
            lightAnim.SetTrigger("DayTime");
        }
        else if (_core.IsNightTime)
        {
            bearFlag = Random.Range(0, 10);
            lightAnim.SetTrigger("Night");
        }

        ModifyFluctuation(ArtifactAddHunger, ArtifactAddThirst);

        if (GameManager.Instance.ArtifactDict[9005] && !GameManager.Instance.ArtifactDict[9009])
        {
            //ModifyFluctuation(0, 1);
            if (ThirstDifficulty.Value == ThirstFluctuation.Value && ThirstDifficulty.Value != 0)
            {
                ThirstFluctuation.Value -= 1;
            }
            else
            {
                ThirstDifficulty.Value -= (ThirstDifficulty.Value <= _core.ThirstDifficulty) ? 0 : 1;
            }
        }
        if (GameManager.Instance.ArtifactDict[9006] && !GameManager.Instance.ArtifactDict[9010])
        {
            //ModifyFluctuation(1, 0);
            if (HungerDifficulty.Value == HungerFluctuation.Value && HungerDifficulty.Value != 0)
            {
                HungerFluctuation.Value -= 1;
            }
            else
            {
                HungerDifficulty.Value -= (HungerDifficulty.Value <= _core.HungerDifficulty) ? 0 : 1;
            }
        }
        _hunger.Value = (int)_core.Status.curHunger;
        _thirst.Value = (int)_core.Status.curThirst;
        _ap.Value = _core.Status.curAp;
        _instance.Clock.gameObject.SetActive(true);

        StatUICanvas.statUI.Texts[5].text = (_core.HungerDifficulty != 0) ? (-_core.HungerDifficulty).ToString() : "";
        StatUICanvas.statUI.Texts[6].text = (_core.ThirstDifficulty != 0) ? (-_core.ThirstDifficulty).ToString() : "";

        StatUICanvas.statUI.Texts[3].text = $"Day : {Date + 1}";
        StatUICanvas.statUI.Texts[4].text = $"{(IsDawn ? "새벽" : IsDayTime ? "점심" : IsMorning ? "아침" : "저녁")}";
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
            var result = _core.ModifyAP(amount);
            if(result)
                _ap.Value += amount;
            return result;
        }
        public static bool ModifyHunger()
        {
            return ModifyHunger(1);
        }
        public static bool ModifyHunger(int amount)
        {
            var result = _core.ModifyHunger(amount);
            if (result)
                _hunger.Value += ((_hunger.Value + amount) > _core.Status.maxHunger) ? (int)_core.Status.maxHunger - _hunger.Value : amount;
            //Debug.Log(_hunger.Value);
            return result;
        }
        public static bool ModifyThirst()
        {
            return ModifyThirst(1);
        }
        public static bool ModifyThirst(int amount)
        {
            var result = _core.ModifyThirst(amount);
            if (result)
                _thirst.Value += ((_thirst.Value + amount) > _core.Status.maxThirst) ? (int)_core.Status.maxThirst - _thirst.Value : amount;
            //Debug.Log(_thirst.Value);
            return result;
        }
        public static void ModifyDifficulty(int hungerValue, int thirstValue)
        {
            HungerDifficulty.Value = ((HungerDifficulty.Value - hungerValue - ArtifactAddHunger) < _core.HungerDifficulty) ? _core.HungerDifficulty : (HungerDifficulty.Value - hungerValue);
            ThirstDifficulty.Value = ((ThirstDifficulty.Value - thirstValue - ArtifactAddThirst) < _core.ThirstDifficulty) ? _core.ThirstDifficulty : (ThirstDifficulty.Value - thirstValue);
        }
        public static void ModifyFluctuation(int hungerFluctuation, int thirstFluctuation)
        {
            tempHungerFluctuation = HungerFluctuation.Value;
            tempThirstFluctuation = ThirstFluctuation.Value;
            addTempHungerFluctuation = (HungerFluctuation.Value + ArtifactAddHunger <= _core.HungerDifficulty) ? 0 :
                (HungerFluctuation.Value + ArtifactAddHunger - hungerFluctuation < _core.HungerDifficulty) ? (_core.HungerDifficulty - (HungerFluctuation.Value + ArtifactAddHunger)) : - hungerFluctuation;    // 추가할 값
            addTempThirstFluctuation = (ThirstFluctuation.Value + ArtifactAddThirst <= _core.ThirstDifficulty) ? 0 :
                (ThirstFluctuation.Value + ArtifactAddThirst - thirstFluctuation < _core.ThirstDifficulty) ? (_core.ThirstDifficulty - (ThirstFluctuation.Value + ArtifactAddThirst)) : - thirstFluctuation;    // 추가할 값
            HungerFluctuation.Value = (HungerFluctuation.Value + ArtifactAddHunger - hungerFluctuation < _core.HungerDifficulty) ? _core.HungerDifficulty - ArtifactAddHunger : (HungerFluctuation.Value - hungerFluctuation);  // 값
            ThirstFluctuation.Value = (ThirstFluctuation.Value + ArtifactAddThirst - thirstFluctuation < _core.ThirstDifficulty) ? _core.ThirstDifficulty - ArtifactAddThirst : (ThirstFluctuation.Value - thirstFluctuation);  // 값
            ModifyHunger(-addTempHungerFluctuation);
            ModifyThirst(-addTempThirstFluctuation);

            //if (-addTempHungerFluctuation < hungerFluctuation)
            //    overflowHunger = true;
            //else
            //    overflowHunger = false;
            //if (-addTempThirstFluctuation < thirstFluctuation)
            //    overflowThirst = true;
            //else
            //    overflowThirst = false;

        }
        public static void ModifyFluctuation(bool boolean)  // 증감 수치 돌려놓기 / 먹기 버튼만 false
        {
            if (boolean)
            {
                HungerFluctuation.Value = tempHungerFluctuation;
                ThirstFluctuation.Value = tempThirstFluctuation;
                ModifyHunger(addTempHungerFluctuation);
                ModifyThirst(addTempThirstFluctuation);
            }
            //TempHungerFluctuation.Value = HungerDifficulty.Value;
            //TempThirstFluctuation.Value = ThirstDifficulty.Value;
        }

        public static void HungerStatChange(int hungerValue)  // 배고픔 수치 변경
        {
            if (ModifyHunger(hungerValue))
            {
                HungerFluctuation.Value = (HungerFluctuation.Value + ArtifactAddHunger - hungerValue < _core.HungerDifficulty) ? _core.HungerDifficulty - ArtifactAddHunger : (HungerFluctuation.Value - hungerValue);
                ModifyDifficulty(hungerValue, 0);
            }
            else { GameOverNotice(); }
            
        }
        public static void ThirstStatChange(int thirstValue)  // 갈증 수치 변경
        {
            if (ModifyThirst(thirstValue))
            {
                ThirstFluctuation.Value = (ThirstFluctuation.Value + ArtifactAddThirst - thirstValue < _core.ThirstDifficulty) ? _core.ThirstDifficulty - ArtifactAddThirst : (ThirstFluctuation.Value - thirstValue);
                ModifyDifficulty(0, thirstValue);
            }
            else { GameOverNotice(); }
        }

    #endregion

    public void CreateCard()
    {
        if (ModifyAP(-1))
        {
            if (GameManager.Instance.ArtifactDict[9008])
            {
                float rand = Random.Range(0, 0.99f);
                if (rand < 0.05f)
                {
                    CardManager.CreateCard();
                }
            }
            CardManager.CreateCard();

        }
        else
        {
            Debug.Log("AP가 부족합니다.");
            BearManager.Notice("행동력이 부족합니다\n" +
                               "행동력은 턴의 경과에 따라\n" +
                               "자연스럽게 회복됩니다.");
        }
    }

    public void SetDifficulty(ushort value)
    {
        //_core.Difficulty = value;
    }

    internal static void ResetGame()
    {
        _core.EndGame();
        _core.InitGame();
    }

    public static void GameOverNotice()
    {
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
}
