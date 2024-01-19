using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Bear : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody _rigid;
    private Vector3 _targetPos = default;
    private float _spd = 3260f;
    private bool _isMovable = true;
    
    public int hitPoint = 1;
    
    public Card Target = null;
    public int HitPoint => hitPoint;
    public bool IsDead => hitPoint <= 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (!TryGetComponent(out _anim) || !TryGetComponent(out _rigid))
        {
            Debug.Log("Failed to Load Component");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead)
        {
            OnDead();
            return;
        }
        try
        {
            _targetPos = new Vector3()
            {
                x = Target.transform.position.x,
                y = 0,
                z = Target.transform.position.z,
            };

            transform.LookAt(_targetPos); 
            
            if (_isMovable)
            {
               //this.transform.LookAt(Vector3.Lerp(this.transform.position, ));
                if (Vector3.Distance(this.transform.position, _targetPos) > 10f)
                    transform.position = Vector3.MoveTowards(this.transform.position, _targetPos,
                        Time.deltaTime * _spd * 20);
            }
        }
        catch (Exception e)
        {
            if (!IsDead)
            {
                _isMovable = true;
            }
            else
            {
                OnDead();
                return;
            }
            
            if (!SetTarget(out Target))
            {
                Debug.Log("추적할 대상이 없습니다.");
                Debug.Log("곰들이 한심하다는 듯이 바라보고 있습니다...");
                BearManager.BearLeave();
                BearManager._turnSkip.interactable = true;
            }
            
            if(!_anim.GetBool("Idle") && !IsDead)
            {
                _anim.SetBool("Idle", true);
                _anim.SetBool("Run Forward", false);
            }
           
        }
    }
    
    public void Init()
    {
        StartCoroutine(SetTarget());
        StartCoroutine(AnimationController());

        //_hitPoint = CoreController.TurnCnt + 1;
        hitPoint = 1;
    }

    private bool SetTarget(out Card target)
    {
        if (HitPoint <= 0)
        {
            target = null;
            return true;
        }
        var result = true;
        var v =
            from card in CardManager.Cards
            where card.ID < 2000
            orderby Vector3.Distance(this.transform.position, card.transform.position)
            select card;
        
        if (v.Any())
            target = v.ElementAt(0);
        else if (_isMovable)
        {
            target = null;
            result = false;
        }
        else
        {
            target = null;
        }
        
        return result;
    }
    IEnumerator SetTarget()
    {
        while (true)
        {
            if (IsDead)
            {
                yield break;
            }
            if (SetTarget(out Card target))
            {
                Target = target;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }
    IEnumerator AnimationController()
    {
        _spd = Random.Range(8, 20) * 0.1f;
        while (true)
        {
            if (IsDead)
            {
                OnDead();
                break;
            }
            if (Target == null)
            {
                _anim.SetBool("Idle", true);
                yield return new WaitForSeconds(0.1f);
                continue;
            }
            
            if (Vector3.Distance(this.transform.position, _targetPos) > 10f && _isMovable)
            {
                _anim.SetBool("Idle", false);
                if (!_anim.GetBool("Run Forward"))
                    _anim.SetBool("Run Forward", true);
            }
            
            else if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Run Forward") && _isMovable)
            {
                _anim.SetBool("Run Forward", false);
                _anim.SetBool("Idle", false);
                _anim.SetTrigger("Attack5");
                _isMovable = false;
                while (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) 
                    yield return new WaitForSeconds(0.1f);
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }

    
    //Animation Event Method ==================
    public void SetMovable()
    {
        _anim.SetBool("Run Forward", false);
        
        if (Target != null)
        {
            if (Target.transform.parent.TryGetComponent(out CardGroup cardGroup))
                CardManager.DestroyCard(cardGroup.RemoveCard(Target));
            else if (!CardManager.DestroyCard(Target))
            {
                Debug.Log("Failed to Destroy Card");
                _isMovable = true;
                return;
            }
        }
        
        Debug.Log("End Attack");
        
        Target = null;

        var t = new Task(() =>
        {
            Thread.Sleep(1000);
            _isMovable = !IsDead;
        });
        
        t.Start();
    }
    public void Die()
    {
        Destroy(this.gameObject);
    }
    //=============================================

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Card card) && other.transform.position.y > this.transform.position.y + 0.3f)
        {
            if (other.transform.parent.TryGetComponent(out CardGroup g))
                if (g.IndexOf(card) == 0)
                {
                    Debug.Log(true);
                    var c = g.RemoveCard(card);
                    Destroy(c.gameObject);

                    var col = this.GetComponent<Collider>();
                    col.enabled = false;
                    
                    this.hitPoint -= card.ID % 10 + 1;
                    if(IsDead)
                    {
                        OnDead();
                    }
                    
                    return;
                }
                else
                {
                    return;
                }
            if (card.ID > 2000)
            {
                Destroy(card);

                var col = this.GetComponent<Collider>();
                col.enabled = false;
                
                this.hitPoint -= card.ID % 10 + 1;
                if(IsDead)
                {
                    OnDead();
                }
            }
        }
    }

    private void OnDestroy()
    {
        BearManager.RemoveBear(this);
    }

    public void OnDead()
    {
        _isMovable = false;
        
        Target = null;
        _anim.SetBool("Run Forward", false);
        _anim.SetBool("Idle", false);
        if (!_anim.GetBool("Death"))
            _anim.SetBool("Death", true);
        
        if(BearManager.Instance.bearApear.gameObject.activeInHierarchy)
            BearManager.Instance.bearApear.gameObject.SetActive(false);
    }

    public bool Leave()
    {
        var result = true;
        try
        {
            Destroy(this.gameObject);
        }
        catch (Exception e)
        {
            result = false;
        }

        return result;
    }
}
