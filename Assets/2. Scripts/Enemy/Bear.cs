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
    private float _spd;
    private bool _isMovable = true;
    private int _hitPoint = 2;
    
    public Card Target = null;
    public int HitPoint => _hitPoint;
    
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
        try
        {
            _targetPos = new Vector3()
            {
                x = Target.transform.position.x,
                y = 0,
                z = Target.transform.position.z,
            };
            
            if (_isMovable)
            {
                this.transform.LookAt(_targetPos);
                if (Vector3.Distance(this.transform.position, _targetPos) > 10f)
                    transform.position = Vector3.MoveTowards(this.transform.position, _targetPos, 0.05f * _spd);
            }
        }
        catch (Exception e)
        {
            return;
        }
    }
    
    private void OnEnable()
    {
        StartCoroutine(SetTarget());
        StartCoroutine(AnimationController());
    }

    IEnumerator SetTarget()
    {
        while (true)
        {
            var v =
                from card in CardManager.Cards
                where card.ID < 2000
                orderby Vector3.Distance(this.transform.position, card.transform.position)
                select card;
            
            
            yield return new WaitForSeconds(0.3f);

            if (!v.Any()) continue;
            Target = v.ElementAt(0);
            
        }
    }

    IEnumerator AnimationController()
    {
        _spd = Random.Range(8, 20) * 0.1f;
        while (true)
        {
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

    
    //Animation Event Method
    public void SetMovable()
    {
        _anim.SetBool("Run Forward", false);
        if (Target != null)
        {
            if (Target.transform.parent.TryGetComponent(out CardGroup cardGroup))
                CardManager.DestroyCard(cardGroup.RemoveCard(Target));
            else
                CardManager.DestroyCard(Target);
        }

        Target = null;

        var t = new Task(() =>
        {
            Thread.Sleep(1000);
            _isMovable = true;
        });
        
        t.Start();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Card card) && other.transform.position.y > this.transform.position.y + 0.2f)
        {
            if (card.ID > 2000)
            {
                if((this._hitPoint -= card.ID % 10 + 1) < 0)
                {
                    _isMovable = false;
                    _anim.SetBool("Run Forward", false);
                    _anim.SetBool("Idle", false);
                    if(_anim.GetBool("Death"))
                        _anim.SetBool("Death", true);
                    
                    Debug.Log($"Remain HitPoint : {HitPoint} ");
                }
                CardManager.DestroyCard(card);
            }
        }
    }
}
