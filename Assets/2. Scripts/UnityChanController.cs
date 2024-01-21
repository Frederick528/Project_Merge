using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityChan;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

[Serializable]
public struct TalkSystem
{
    public GameObject root;
    public float freq;
    public int rate;
    
    private GameObject _speaker;
    private TMP_Text _tf;
    private Image _tfImage;
    private Animator _anim;
    private CancellationToken _printToken;

    public void SetSpeaker(GameObject o) => _speaker = o;

    public async UniTask OpenMsg(string str, CancellationToken cancellationToken)
    {
        _printToken = cancellationToken;

        var v = root = GameObject.Instantiate(root);
        
        _tf ??= root.GetComponentInChildren<TMP_Text>();
        _tfImage ??= root.GetComponentInChildren<Image>();
        _anim ??= root.GetComponentInChildren<Animator>();
        
        _tf.text = "";
        ModifyTextFieldSize(cancellationToken);
        
        foreach (var chr in str)
        {
            if (_printToken.IsCancellationRequested)
                break;
            _tf.text += chr;
            await UniTask.Delay(150, cancellationToken: _printToken);
            //값을 리턴 받았을 경우 ==> 모종의 이유로 UniTask List에서 완료된 Task가 나온 경우
            // 진행하던 작업을 중단
        }
        
        // 마우스를 클릭하거나 1초가 지날 때까지 기다렸다가 종료
        var t1 = UniTask.Delay(1000, cancellationToken: _printToken);
        var t2 = UniTask.WaitUntil(() => Input.GetMouseButton(0), cancellationToken: _printToken);

        if (await UniTask.WhenAny(t1, t2) == 0)
            t2.SuppressCancellationThrow();
        else
            t1.SuppressCancellationThrow();
        
        await CloseMsg();
        
        GameManager.Destroy(v);
    }
    
    private async UniTask CloseMsg()
    {
        _anim.SetTrigger("Disappear");
        await UniTask.Delay(1000, cancellationToken: _printToken);
    }

    public async UniTask ModifyTextFieldSize(CancellationToken cancellationToken)
    {
        var mainCam = Camera.main;
        var defScale = root.transform.localScale;
        while (true)
        {
            if (cancellationToken.IsCancellationRequested)
                break;
            root.transform.localScale = defScale * mainCam.fieldOfView / 120;
            root.transform.position = _speaker.transform.position + Vector3.up * 18 + Vector3.forward * 10;
            await UniTask.Delay(20, cancellationToken: cancellationToken);
        }
    }
}

public class UnityChanController : Draggable
{
    // Start is called before the first frame update
    private SpringManager _spring;
    private IKLookAt _iKController;
    private Rigidbody _rigid;
    private Camera _mainCam;
    private Animator _anim;
    private bool _isMouseDown = false;
    private float[] curTime = new [] {0f, 0f};

    private CancellationTokenSource _faceToken;
    
    [SerializeField] private float period = 1f;

    public Transform eyeLine;
    public Transform lookPos;
    public AnimationCurve curve;
    public AnimationCurve curveStanding;

    [FormerlySerializedAs("talker")] public TalkSystem talkSystem;
    
    public static bool IsMoving;

    private void Awake()
    {
        _spring = GetComponent<SpringManager>();
        _iKController = GetComponent<IKLookAt>();
        _rigid = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();

        _iKController.lookAtObj = lookPos;
        _rigid.interpolation = RigidbodyInterpolation.Interpolate;
        _faceToken = new();
        
        talkSystem.SetSpeaker(this.gameObject);
    }
    
    private async void Start()
    {
        FaceAI();
        if (!GameManager.Instance.isTutorial)
            Talking();
    }

    private void Update()
    {
        if (!_mainCam.gameObject.activeSelf)
            return;
        
        var target = _mainCam.transform.position;
        var targetPos = new Vector3(
            target.x,transform.position.y, target.z);
        var v = (_mainCam.fieldOfView / 30);
        var distance = Vector3.Distance(this.transform.position, targetPos);
        
        // if (distance > 4f * Mathf.Log(v * 2.8f, 2))
        // {
        // }
        // else
        // {
        //     this.transform.rotation = quaternion.Euler(Vector3.down * 180);
        // }

        //targetPos += Vector3.forward * (7 * v);
        
        if (_isMouseDown)
        {
            return;
        }
        if (_rigid.velocity.sqrMagnitude > 0.01f)
        {
            if (Physics.Raycast(transform.position, Vector3.down, 1.5f))
            {
                _anim.SetTrigger("Land");
            }
            return;
        }
        
        // 2.8 은 약  2^(3/2)
        if ( distance > 4f * Mathf.Log(v * 2.8f, 2))
        {
            var p = Vector3.zero;
            if (curTime[1] < 0.4f)
            {
                p = Vector3.Lerp(this.transform.position, targetPos, 0.1f * curveStanding.Evaluate(curTime[1]));
            }
            else
            {
                 p = Vector3.Lerp(this.transform.position, targetPos, 0.005f * Mathf.Log(v * 2.8f, 2));
            }
            curTime[1] += Time.deltaTime;
            curTime[0] -= curTime[0];

            
           
            var temp = (this.transform.position - p).magnitude > 0 ? (this.transform.position - p).magnitude : 0;
            var f = curve.Evaluate(temp / 1.5f);
            _anim.SetFloat("MoveSpd", 
                Mathf.Lerp(_anim.GetFloat("MoveSpd"), f, 0.2f));
            _rigid.MovePosition(p);
            
            if (Vector3.Distance(_iKController.lookAtObj.transform.position, eyeLine.position) > 0.01f)
                _iKController.lookAtObj.transform.position =
                    Vector3.Lerp(_iKController.lookAtObj.transform.position, eyeLine.position, f * 0.2f);

            IsMoving = true;
            
            transform.LookAt(Vector3.Lerp(new Vector3()
            {
                x = lookPos.position.x,
                y = targetPos.y,
                z = lookPos.position.z,
            }, targetPos, 0.4f));
        }
        else
        {
            curTime[0] += Time.deltaTime;
            if (curTime[0] > period)
                curTime[0] -= curTime[0];
            curTime[1] -= curTime[1];
            
            var f= _anim.GetFloat("MoveSpd");
            if(f > 0.001f)
                _anim.SetFloat("MoveSpd", 
                    Mathf.Lerp(f, 0 , 0.05f));
            
            _iKController.lookAtObj.transform.position =
                Vector3.Lerp(_iKController.lookAtObj.transform.position, 
                    Vector3.Lerp(eyeLine.position, target, 0.8f), 0.1f * curveStanding.Evaluate(curTime[0]));

            if (distance > 0.001f)
                IsMoving = false;
            
            
            transform.LookAt(Vector3.Lerp(new Vector3()
            {
                x = lookPos.position.x,
                y = targetPos.y,
                z = lookPos.position.z,
            }, targetPos, 0.2f));
        }
    }
    
    private void OnMouseDown()
    {
        base.OnMouseDown();
        _isMouseDown = true;
        IsMoving = false;
        _rigid.isKinematic = true;
        if (_rigid.velocity.magnitude > 0.001f) return;
        this.transform.position += Vector3.up * 5;
        _anim.ResetTrigger("Land");
        _anim.SetTrigger("Hang");
    }

    protected override void OnMouseDrag()
    {
        base.OnMouseDrag();
    }

    private void OnMouseUp()
    {
        _rigid.isKinematic = false;
        _isMouseDown = false;
        //_anim.SetBool("isHang", false);
        _rigid.velocity = Vector3.down * 5;
    }

    private async UniTaskVoid FaceAI()
    {
        if (GameManager.Instance.isTutorial)
            return;
        while (true)
        {
            if (!GameManager.CardCanvasOn)
            {
                if (_isMouseDown)
                {
                    //emotionController.OnCallChangeFace("surprise@sd_hmd");
                    SetAnim("surprise@sd_hmd");
                }
                else if (IsMoving)
                {
                    SetAnim("default@sd_hmd");
                }
                else
                {
                    SetAnim("smile@sd_hmd");
                }
            }
            
            
            if (_faceToken.IsCancellationRequested)
                break;
            await UniTask.Delay(100, cancellationToken: _faceToken.Token);
        }
    }

    private async UniTaskVoid Talking()
    {
        var idx = 0;
        while (true)
        {
            if (!GameManager.CardCanvasOn)
            {
                if (Random.Range(0, 100) < talkSystem.rate)
                {
                    var v = new CancellationTokenSource();
                    if (TextDataDeserializer.TryGetData(idx %= 15 , out string str))
                    {
                        await talkSystem.OpenMsg(str, v.Token);
                        v.Cancel();
                        v.Dispose();
                    }
                }
                if (_faceToken.IsCancellationRequested)
                    break;
            }
            idx++;
            await UniTask.Delay((int)(talkSystem.freq * 1000), cancellationToken: _faceToken.Token);
        }
        
    }

    public void SetAnim(string animName)
    {
        _anim.CrossFade(animName, 0);
    }

    private void OnDestroy()
    {
        _faceToken.Cancel();
        _faceToken.Dispose();
    }
}
