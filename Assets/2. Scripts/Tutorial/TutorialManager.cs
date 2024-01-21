using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

[Serializable]
public struct TutorialObjects
{
    public Button[] buttons;
    [Header("말풍선")]
    public GameObject root;
    public TMP_Text textField;
    public Animator textFieldAnimator;
    [Space(10)]
    public GameObject[] mergingArea;
    public StatUI statUI;
    public GameObject kohaku;
}

[Serializable]
public struct TextFieldAnimationParams
{
    [Header("단위 : sec")]
    public float duration;
}


public class TutorialManager : MonoBehaviour
{
    public TutorialObjects objects;
    public TextFieldAnimationParams animParams;
    public Camera mainCamera;
    public Camera subCamera;
    public string[] script;
    
    public static bool WaitButtonCallBack = false;
    

    private float _fov = 0;
    private ushort _currentIdx = 0;
    private const string NextSequence = "^NEXT^";
    private const string EndSequence = "^END^";

    private UniTask _tutorialProcessingTask;
    private CancellationTokenSource _cts;
    private CancellationTokenSource _printToken;
    private List<IDisposable> _observers = new ();

    private void Awake()
    {
    }

    private async void Start()
    {
        _cts = new CancellationTokenSource();
        _printToken = new CancellationTokenSource();
        
        if (GameManager.Instance.isTutorial)
        {
            EncounterManager.Occur(new []{
                    "숲속에서 30일 튜토리얼을 시작합니다. 원하지 않을 경우, [스킵] 버튼을 눌러주세요.",
                    "진행",
                    "스킵"},
                new []
                {
                    (Action)(async () =>
                    {
                        _tutorialProcessingTask = TutorialProcess();
                        await _tutorialProcessingTask;
                    }),
                    (Action)(() =>
                    {
                        Debug.Log("decline");
                        GameManager.Instance.isTutorial = false;
                        SceneManager.LoadScene("8. MergeScene");
                        CoreController.ResetGame();
                    })
                }
            );
        }

        foreach (var o in objects.mergingArea)
        {
            o.SetActive(false);
        }


        _observers.Add(Observable.EveryUpdate()
            .Where( x => Camera.main.fieldOfView != _fov)
            .Subscribe(x =>
            {
                _fov = Camera.main.fieldOfView;

                objects.root.transform.localScale
                    = (Vector3.one * 0.8f) * _fov / 60;
            }));
        
        _observers.Add(Observable.EveryUpdate()
            .Where( x => UnityChanController.IsMoving)
            .Subscribe(x =>
            {
                if (UnityChanController.IsMoving)
                {
                    objects.root.transform.position = 
                        objects.kohaku.transform.position + (Vector3.up * 0.5f + Vector3.forward * 15f * (_fov / 60));
                    //CloseMsg();
                    //GameManager.CardCanvasOn = false;
                }
            }));
        
        objects.buttons[1].gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        foreach (var observer in _observers)
        {
            observer.Dispose();
        }
        
        _printToken.Cancel();
        _cts.Cancel();
        _printToken.Dispose();
        _cts.Dispose();

    }

    private void OpenMsg(string content)
    {
        if(objects.root.activeSelf)
            objects.root.SetActive(false);
        objects.root.transform.position =
            objects.kohaku.transform.position + (Vector3.up * 0.5f + Vector3.forward * 15f * (_fov / 60));
        objects.root.SetActive(true);
        objects.textField.text = content;
    }
    
    private async UniTask OpenMsg(ushort idx)
    {
        if(objects.root.activeSelf)
            objects.root.SetActive(false);
        objects.root.transform.position =
            objects.kohaku.transform.position + (Vector3.up * 15f + Vector3.forward * 15f * (_fov / 60));
        objects.root.SetActive(true);

        //var v = WaitForButtonCallBack();
        
        var str = script[idx % script.Length];
        
        objects.textField.text = "";
        //Debug.Log(_printToken.IsCancellationRequested);
        foreach (var chr in str)
        {
            if (_printToken.Token.IsCancellationRequested)
                break;
            objects.textField.text += chr;
            await UniTask.Delay(50,cancellationToken: _printToken.Token);
            //값을 리턴 받았을 경우 ==> 모종의 이유로 UniTask List에서 완료된 Task가 나온 경우
            // 진행하던 작업을 중단
        }
    }
    private void CloseMsg()
    {
        objects.textFieldAnimator.SetTrigger("Disappear");
    }

    async UniTask PrintText()
    {
        var task = HighLight(objects.kohaku.transform, 45, false, _cts.Token);
        await WaitForCanvasClose();
        Thread.MemoryBarrier();
        //await WaitForLeave();
        while (true)
        {
            await WaitForArrive();
            var v = script[_currentIdx];
            if (v != NextSequence)
            {
                if (v == EndSequence)
                {
                    var op = SceneManager.LoadSceneAsync("7. MergeScene");
                    OnDestroy();
                    await op;
                    return;
                }
                
                var t = OpenMsg(_currentIdx);
                //objects.kohaku.transform.rotation = Quaternion.Euler(Vector3.down * 180);
                
                List<UniTask> list = new();
                Thread.MemoryBarrier();
                var temp = new CancellationTokenSource();
                Thread.MemoryBarrier();
                
                list.Add(WaitForMouseInput(temp.Token));
                list.Add(WaitForLeave(temp.Token));
                list.Add(UniTask.Delay((int)animParams.duration * 1000, cancellationToken: temp.Token));
                
                using (null)
                {
                    Thread.MemoryBarrier();
                    await UniTask.Delay(1500, cancellationToken: temp.Token);

                    //화면 탈출이나 마우스 클릭이 발생할 때 까지 대기
                    Thread.MemoryBarrier();
                    var id = await UniTask.WhenAny(list);
                    if (t.Status == UniTaskStatus.Pending)
                    {
                        t.SuppressCancellationThrow();
                    }

                    if (id == 1)
                        _currentIdx -= (ushort)(_currentIdx == 0 ? 0 : 1);
                    
                    temp.Cancel();
                    _printToken.Cancel();
                    
                    Thread.MemoryBarrier();
                    //이전 토큰이 가지고 있던 내용 초기화
                    _printToken.Dispose();
                    temp.Dispose();
                    
                    Thread.MemoryBarrier();
                    //리셋
                    _printToken = new CancellationTokenSource();
                    Thread.MemoryBarrier();
                }
            }
            else
            {
                CloseMsg();
                _currentIdx++;
                break;
            }
            _currentIdx++;
        }
        await task;
        subCamera.depth = mainCamera.depth - 1;
    }
    async UniTask WaitForArrive()
    {
        // 움직임이 멈출 때 까지 홀드
        await UniTask.WaitUntil(() => !UnityChanController.IsMoving,
            cancellationToken : _cts.Token);
    }
    async UniTask WaitForLeave(CancellationToken token)
    {
        // 움직임이 시작될 때 까지 홀드
        await UniTask.WaitUntil(() => UnityChanController.IsMoving,
            cancellationToken : token);
        GameManager.CardCanvasOn = false;
        CloseMsg();
    }
    async UniTask WaitForMouseInput(CancellationToken token)
    {
        // 움직임이 시작될 때 까지 홀드
        await UniTask.WaitUntil(() =>
                (Input.GetMouseButtonDown(0) && !GameManager.CardCanvasOn),
            cancellationToken : token);
    }

    async UniTask WaitForMouseInput(bool isCheckUI, CancellationToken token)
    {
        // 움직임이 시작될 때 까지 홀드
        if (isCheckUI)
            await WaitForMouseInput(token: token);
        else
        {
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0),
                cancellationToken : token);
        }
        Debug.Log(true);
    }
    async UniTask WaitForCanvasClose()
    {
        await UniTask.WaitUntil(() => !GameManager.CardCanvasOn,
            cancellationToken : _cts.Token);
    }
    async UniTask WaitForButtonCallBack()
    {
        Thread.MemoryBarrier();
        WaitButtonCallBack = false;
        await UniTask.WaitUntil(() => WaitButtonCallBack,
            cancellationToken : _cts.Token);
        WaitButtonCallBack = false;
        Thread.MemoryBarrier();
    }

    async UniTask HighLight(Transform targetPos, CancellationToken token)
    {
        await HighLight(targetPos, 60, true, _cts.Token);
    }
    
    async UniTask HighLight(Transform target, int fov, CancellationToken token)
    {
        subCamera.fieldOfView = fov;
        mainCamera.fieldOfView = fov;
        
        subCamera.transform.position = mainCamera.transform.position;
        subCamera.depth = mainCamera.depth + 1;
        
        byte b = 0;
        while (true)
        {
            if (token.IsCancellationRequested) break;
            var distanceCheck = UniTask.Delay(20, cancellationToken: token);
            var targetPos = new Vector3(target.position.x, 80, target.position.z);
            subCamera.transform.position =
                Vector3.Lerp(subCamera.transform.position, targetPos, 0.2f);

            if (Vector3.Distance(subCamera.transform.position, targetPos) < 0.1f || b >= 100)
                break;
            await distanceCheck;

            b += 1;
        }
            
        await UniTask.Delay(2000, cancellationToken: token);
    }
    
    async UniTask HighLight(Transform targetPos, int fov, bool isBackMainCam, CancellationToken token)
    {
        await HighLight(targetPos, fov, _cts.Token);
        if (isBackMainCam)
        {
            subCamera.depth = mainCamera.depth - 1;
        }
        else
        {
            mainCamera.transform.position = subCamera.transform.position;
        }
    }

    async UniTask TutorialProcess()
    {
        await PrintText();
        
        //===================== 카드 생성 튜토리얼 ============================================

        #region CreateCard

        objects.buttons[0].gameObject.SetActive(true);
        objects.buttons[0].interactable = true;
        objects.buttons[0].onClick.RemoveAllListeners();
        
        objects.buttons[0].onClick.AddListener(async () =>
        {
            CardManager.CreateCard(1020);
            objects.buttons[0].interactable = false;

            await WaitForArrive();
            Thread.MemoryBarrier();
            
            var temp = new CancellationTokenSource();
            Thread.MemoryBarrier();
            
            #region 클릭 || 화면 이탈 까지 대기 
            var t1 = WaitForMouseInput(temp.Token);
            var t2 = WaitForLeave(temp.Token);
            var t3 = UniTask.Delay(1000, cancellationToken: temp.Token);

            Thread.MemoryBarrier();
            //화면 탈출이나 마우스 클릭이 발생할 때 까지 대기
            await UniTask.WhenAny(t1, t2, t3);
            Thread.MemoryBarrier();
            temp.Cancel();
            #endregion

            await PrintText();
            
            objects.buttons[0].interactable = true;
            
            objects.buttons[0].onClick.RemoveAllListeners();
            objects.buttons[0].onClick.AddListener(() =>
            {
                CardManager.CreateCard(1020);
                WaitButtonCallBack = true;
                CloseMsg();
                objects.buttons[0].interactable = false;
                objects.buttons[0].onClick.RemoveAllListeners();
                objects.buttons[0].onClick.AddListener(() =>
                {
                    CardManager.CreateCard();
                });
            });
        });

        await UniTask.Delay(200, cancellationToken: _cts.Token);

        await WaitForButtonCallBack();
        
        #endregion
        
        //===================== 카드 병합 튜토리얼 ============================================

        #region 카드 병합 튜토리얼
        Thread.MemoryBarrier();
        await UniTask.Delay(2000);
        foreach (var area in objects.mergingArea)
        {
            area.SetActive(true);
        }
        
        GameManager.CardCanvasOn = true;
        await HighLight(objects.mergingArea[0].transform, 60, true, _cts.Token);

        GameManager.CardCanvasOn = false;
        await PrintText();
        //카드가 합쳐질 때 까지 대기
        await WaitForButtonCallBack();
        await UniTask.Delay(1000);
        
        //가장 최근에 생성된 카드의 좌표를 가져옴
        await HighLight(CardManager.Cards[^1].transform, 60, true, _cts.Token);
        
        MouseRightClick.Instance.cardEatBtn.interactable = false;
        MouseRightClick.Instance.cardDecompositionBtn.interactable = false;
        
        await PrintText();
        await WaitForButtonCallBack();
        #endregion
        
        //===================== 스테이터스 튜토리얼 ============================================

        #region 스테이터스 튜토리얼
        
        objects.statUI = Instantiate(objects.statUI);
        objects.statUI.statUI.Texts[2].text = "[ 0 / 2 ]";
        objects.statUI.gameObject.SetActive(true);
        
        // objects.statUI = CoreController.Instance.StatUICanvas;
        // objects.statUI.statUI.Texts[2].text = "[ 0 / 2 ]";
        
        objects.statUI.Enter();

        await PrintText();
        var t = UniTask.Create(async () =>
        {
            objects.statUI.statUI.Hunger[0].fillAmount = 0f;
            objects.statUI.statUI.Thirst[0].fillAmount = 0f;
            objects.statUI.statUI.Hunger[1].fillAmount = 0f;
            objects.statUI.statUI.Thirst[1].fillAmount = 0f;
            for (int i = 0; i < 20; i++)
            {
                objects.statUI.statUI.Hunger[0].fillAmount += 0.01f;
                objects.statUI.statUI.Thirst[0].fillAmount += 0.01f;
                
                for (int j = 0; j < 2; j++)
                {
                    objects.statUI.statUI.Texts[j].text = (100 - (i + 1)) + "";
                }

                await UniTask.Delay(100);
            }
        });
        await PrintText();
        await t;
        t = WaitForButtonCallBack();

        if (!objects.statUI.statUI.Background.TryGetComponent(out Button button))
        {
            button = objects.statUI.statUI.Background.gameObject.AddComponent<Button>();
        }
       
        button.onClick.AddListener(() =>
        {
            WaitButtonCallBack = true;
        });
        
        await t;
        #endregion
        
        //===================== 카드 사용 / 분해 튜토리얼 ============================================

        #region Using Card
        
        objects.statUI.Exit();
        MouseRightClick.Instance.cardEatBtn.interactable = true;
        MouseRightClick.Instance.cardEatBtn.onClick.AddListener(() =>
        {
            if (Resources.Load<Sprite>($"Images/{MouseRightClick.CurrentRef.cardType}/{MouseRightClick.CurrentRef.ID}") != null)    // 나중에 if문은 빼야 함.
            {
                MouseRightClick.Instance.cardImage.sprite = Resources.Load<Sprite>($"Images/{MouseRightClick.CurrentRef.cardType}/{MouseRightClick.CurrentRef.ID}");
            }

            var Data = MouseRightClick.CurrentRef.Data;
            
            objects.statUI.statUI.Texts[0].text = (int.Parse(objects.statUI.statUI.Texts[0].text) + Data.Hunger)+ "" ;
            objects.statUI.statUI.Texts[1].text = (int.Parse(objects.statUI.statUI.Texts[1].text) + Data.Thirst)+ "" ;
            
            objects.statUI.statUI.Hunger[1].fillAmount += 0.01f * Data.Hunger;
            objects.statUI.statUI.Thirst[1].fillAmount += 0.01f * Data.Hunger;
            
            if (MouseRightClick.CurrentRef.transform.parent.TryGetComponent(out CardGroup cardGroup))
                CardManager.DestroyCard(cardGroup.RemoveCard(MouseRightClick.CurrentRef));
            else
                CardManager.DestroyCard(MouseRightClick.CurrentRef);
            //Destroy(hit.collider.gameObject);
            MouseRightClick.Instance.CanvasClose(false);
            EffectManager.instance.eatCardImg = MouseRightClick.Instance.cardImage.sprite;
            EffectManager.instance.cardContents = MouseRightClick.CurrentRef;
            GameObject eatCard = Instantiate(EffectManager.instance.eatEffect, MouseRightClick.Instance.effectUICanvas);
            
            objects.statUI.Enter();
            Thread.MemoryBarrier();
            MouseRightClick.Instance.cardEatBtn.interactable = false;

            WaitButtonCallBack = true;
        });

        await PrintText();
        await WaitForButtonCallBack();
        
        MouseRightClick.Instance.cardDecompositionBtn.interactable = true;
        Thread.MemoryBarrier();
        MouseRightClick.Instance.cardDecompositionBtn.onClick.AddListener(() =>
        {
            MouseRightClick.CurrentRef.OnDecomposition(out Card[] cards);
            CardManager.DestroyCard(MouseRightClick.CurrentRef);
            //if (cardContents.transform.parent.TryGetComponent(out CardGroup cardGroup))
            //    CardManager.DestroyCard(cardGroup.RemoveCard(cardContents));
            //else
            //    CardManager.DestroyCard(cardContents);
            MouseRightClick.Instance.CanvasClose(true);
            MouseRightClick.Instance.cardDecompositionBtn.interactable = false;
            Thread.MemoryBarrier();
            WaitButtonCallBack = true;
        });
        
        await PrintText();

        GameManager.CardCanvasOn = true;
        var c = CardManager.CreateCard(2021);
        MouseRightClick.Instance.cardDecompositionBtn.interactable = true;
        await HighLight(c.transform, _cts.Token);
        Thread.MemoryBarrier();
        GameManager.CardCanvasOn = false;
        
        
        await PrintText();
        await WaitForButtonCallBack();

        #endregion
        
        //===================== 턴 튜토리얼 ============================================


        objects.buttons[1].gameObject.SetActive(true);
        objects.buttons[1].interactable = false;
        objects.statUI.Exit();
        await PrintText();
        
        objects.statUI.statUI.Texts[2].text = "[ 2 / 2 ]";
        
        Thread.MemoryBarrier();
        objects.buttons[1].interactable = true;
        Thread.MemoryBarrier();
        
        await WaitForButtonCallBack();
        
        objects.statUI.Enter();
        await PrintText();
        objects.buttons[0].interactable = true;
        
        SceneManager.LoadScene("7. MergeScene");
        
        
    }
}