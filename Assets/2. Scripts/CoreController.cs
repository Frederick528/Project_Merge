using System.Linq;
using TMPro;
using UnityEngine;
using UniRx;
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
    public static int bearFlag = 0;

    public static bool IsDawn => _core.IsDawn;
    public static bool IsMorning => _core.IsMorning;
    public static bool IsDayTime => _core.IsDayTime;
    public static bool IsNightTime => _core.IsNightTime;

    public static string Time => $"{_core.Date + 1} 일차 " + (IsDawn ? "새벽" : IsDayTime ? "점심" : IsMorning ? "아침" : "저녁") ;

    public StatUI StatUICanvas;
    public Image Clock;
    public Light Light;
    public GameObject Notice;
    public TMP_Text Turn;

    #region ReactiveProperty
    public static readonly ReactiveProperty<float> Difficulty = new ();
    
    private static readonly ReactiveProperty<int> _hunger = new ();
    private static readonly ReactiveProperty<int> _thirst = new ();
    private static readonly ReactiveProperty<int> _ap = new();
    #endregion

    private void Awake()
    {
        _core ??= new GameCore();
        _instance ??= this;

        if (StatUICanvas.gameObject is { activeSelf: true, activeInHierarchy: false })
        {
            StatUICanvas = Instantiate(StatUICanvas);
            StatUICanvas.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        _core.InitGame();
        _ap.Value = _core.Status.curAp;
        Turn.text = _core.TurnCnt + "";
        for (int i = 0; i < 2; i++)
        {
            CardManager.CreateCard();
        }

        //세이브 파일이 없을 경우 새로 딕셔너리를 만들어 거기서 생성
        
        Difficulty.Subscribe(x =>
        {
            if (x == 0) return;
                StatUICanvas.gameObject.SetActive(true);
            _core.Difficulty = (ushort)x;
            StatUICanvas.statUI.Hunger[1].fillAmount += x/ _core.Status.maxHunger;
            StatUICanvas.statUI.Thirst[1].fillAmount += x/ _core.Status.maxThirst;
        });
        _hunger.Subscribe(x =>
        {
            if (x != 0)
                StatUICanvas.gameObject.SetActive(true);
            var v = (_core.Status.maxHunger - x) / _core.Status.maxHunger;
            StatUICanvas.statUI.Hunger[0].fillAmount = v;
            StatUICanvas.statUI.Texts[0].text = _core.Status.curHunger + "";
        });
        _thirst.Subscribe(x =>
        {
            if (x != 0)
                StatUICanvas.gameObject.SetActive(true);
            StatUICanvas.statUI.Thirst[0].fillAmount =
               (_core.Status.maxHunger - x) / _core.Status.maxHunger;
            StatUICanvas.statUI.Texts[1].text = _core.Status.curThirst + "";
        });
        _ap.Subscribe(x =>
        {
            // if (x != 0)
            //     StatUICanvas.gameObject.SetActive(true);
            //
            StatUICanvas.statUI.Texts[2].text = 
                $"[  {x} / {_core.Status.maxAp}  ]";
        });
    }

    private void Update()
    {
    }
    public static void TurnChange()
    {
        if (_instance.StatUICanvas.gameObject.activeSelf)
        {
            _instance.StatUICanvas.Exit();
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
            return;
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
            Difficulty.Value = (Mathf.Log10(Date + 2) * 10 + 10);
            
            
            lightAnim.SetTrigger("Dawn");
            //EncounterManager.Occur();
        }
        else if (_core.IsMorning)
        {
            _core.Difficulty = 0;
            Difficulty.Value = _core.Difficulty;
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

        _hunger.Value = (int)_core.Status.curHunger;
        _thirst.Value = (int)_core.Status.curThirst;
        _ap.Value = _core.Status.curAp;
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
                _hunger.Value += amount;
            Debug.Log(_hunger.Value);
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
                _thirst.Value += amount;
            Debug.Log(_thirst.Value);
            return result;
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
