using System;
using System.Collections;
using System.Threading;
using UniRx;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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
    public Camera subCamera;
    public string[] script;

    private float _fov = 0;
    private bool _waitButtonCallBack = false;
    private ushort _currentIdx = 0;
    private const string NextSequence = "^NEXT^";
    private const string EndSequence = "^END^";
    
    private CancellationTokenSource cts;

    private void Awake()
    {
    }

    private async void Start()
    {
        cts = new CancellationTokenSource();
        
        if (GameManager.Instance.isTutorial)
        {
            EncounterManager.Occur(new []{
                    "튜토리얼을 진행하시겠습니까?",
                    "진행",
                    "스킵"},
                new []
                {
                    (Action)(() =>
                    {
                        Debug.Log("accept");
                    }),
                    (Action)(() =>
                    {
                        Debug.Log("decline");
                        GameManager.Instance.isTutorial = false;
                        SceneManager.LoadScene("Lawsuit");
                        CoreController.ResetGame();
                    })
                }
            );
        }

        foreach (var o in objects.mergingArea)
        {
            o.SetActive(false);
        }


        Observable.EveryUpdate()
            .Where( x => Camera.main.fieldOfView != _fov)
            .Subscribe(x =>
            {
                _fov = Camera.main.fieldOfView;

                objects.root.transform.localScale
                    = (Vector3.one * 0.8f) * _fov / 60;
            });
        
        Observable.EveryUpdate()
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
            });
        
        await TutorialProcess();
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
    
    private void OpenMsg(ref ushort idx)
    {
        if(objects.root.activeSelf)
            objects.root.SetActive(false);
        objects.root.transform.position =
            objects.kohaku.transform.position + (Vector3.up * 0.5f + Vector3.forward * 15f * (_fov / 60));
        objects.root.SetActive(true);
        objects.textField.text = script[idx++ % script.Length ];
    }
    private void CloseMsg()
    {
        objects.textFieldAnimator.SetTrigger("Disappear");
    }

    async UniTask PrintText()
    {
        await WaitForCanvasClose();
        Thread.MemoryBarrier();
        //await WaitForLeave();
        while (true)
        {
            await WaitForArrive();
            var v = script[_currentIdx];
            if (v != NextSequence)
            {
                OpenMsg(ref _currentIdx);
                Thread.MemoryBarrier();
                var t1 = WaitForMouseInput();
                var t2 = WaitForLeave();
                var t3 = UniTask.Delay((int)animParams.duration * 1000);
                
                //화면 탈출이나 마우스 클릭이 발생할 때 까지 대기
                var i = await UniTask.WhenAny(t1, t2, t3);
                if (i == 0)
                {
                    t2.SuppressCancellationThrow();
                    t3.SuppressCancellationThrow();
                }
                else if (i == 1)
                {
                    t1.SuppressCancellationThrow();
                    t3.SuppressCancellationThrow();
                }
                else if (i == 2)
                {
                    t1.SuppressCancellationThrow();
                    t2.SuppressCancellationThrow();
                }
            }
            else
            {
                CloseMsg();
                _currentIdx++;
                break;
            }
        }
    }
    async UniTask WaitForArrive()
    {
        // 움직임이 멈출 때 까지 홀드
        await UniTask.WaitUntil(() => !UnityChanController.IsMoving,
            cancellationToken : cts.Token);
    }
    async UniTask WaitForLeave()
    {
        // 움직임이 시작될 때 까지 홀드
        await UniTask.WaitUntil(() => UnityChanController.IsMoving,
            cancellationToken : cts.Token);
        GameManager.CardCanvasOn = false;
        CloseMsg();
    }
    async UniTask WaitForMouseInput()
    {
        // 움직임이 시작될 때 까지 홀드
        await UniTask.WaitUntil(() =>
                (Input.GetMouseButtonDown(0) && !GameManager.CardCanvasOn),
            cancellationToken : cts.Token);
    }

    async UniTask WaitForMouseInput(bool isCheckUI)
    {
        // 움직임이 시작될 때 까지 홀드
        if (isCheckUI)
            await WaitForMouseInput();
        else
        {
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0),
                cancellationToken : cts.Token);
        }
        Debug.Log(true);
    }
    async UniTask WaitForCanvasClose()
    {
        await UniTask.WaitUntil(() => !GameManager.CardCanvasOn,
            cancellationToken : cts.Token);
    }
    async UniTask WaitForButtonCallBack()
    {
        await UniTask.WaitUntil(() =>_waitButtonCallBack,
            cancellationToken : cts.Token);
        _waitButtonCallBack = false;
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
            
            #region 클릭 || 화면 이탈 까지 대기 
            var t1 = WaitForMouseInput();
            var t2 = WaitForLeave();
            var t3 = UniTask.Delay(1000);
                
            //화면 탈출이나 마우스 클릭이 발생할 때 까지 대기
            var i = await UniTask.WhenAny(t1, t2, t3);
            if (i == 0)
            {
                t2.SuppressCancellationThrow();
                t3.SuppressCancellationThrow();
            }
            else if (i == 1)
            {
                t1.SuppressCancellationThrow();
                t3.SuppressCancellationThrow();
            }
            else if (i == 2)
            {
                t1.SuppressCancellationThrow();
                t2.SuppressCancellationThrow();
            }
            #endregion

            await PrintText();
            
            objects.buttons[0].interactable = true;
            
            objects.buttons[0].onClick.RemoveAllListeners();
            objects.buttons[0].onClick.AddListener(() =>
            {
                CardManager.CreateCard(1020);
                _waitButtonCallBack = true;
                CloseMsg();
                objects.buttons[0].onClick.RemoveAllListeners();
                objects.buttons[0].onClick.AddListener(() =>
                {
                    CardManager.CreateCard();
                });
            });
        });

        await WaitForButtonCallBack();
        
        #endregion
        
        //===================== 카드 병합 튜토리얼 ============================================

        await UniTask.Delay(2000);
        foreach (var area in objects.mergingArea)
        {
            area.SetActive(true);
        }
        var mainCam = Camera.main;
        mainCam.gameObject.SetActive(false);
        
        var targetPos = (objects.mergingArea[0].transform.position
                                        + objects.mergingArea[1].transform.position) / 2 + Vector3.up * 80;

        await UniTask.Create(async () =>
        {
            byte b = 0;
            while (true)
            {
                subCamera.transform.position =
                    Vector3.Lerp(subCamera.transform.position, targetPos, 0.2f);
                var distanceCheck = UniTask.Delay(20);

                if (Vector3.Distance(subCamera.transform.position, targetPos) < 0.1f || b >= 100)
                    break;
                await distanceCheck;

                b += 1;
            }
        });
        
        await UniTask.Delay(2000);
        subCamera.gameObject.SetActive(false);
        mainCam.gameObject.SetActive(true);

        await PrintText();

    }
}