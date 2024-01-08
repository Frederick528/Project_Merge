using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Card : Entity
{
    public enum CardType
    {
        Food,
        Water,
        Wood,
        Stone,
        Combination
    }

    private static CardGroup _tempGroup;
    private CardData _data;

    public CardData Data => _data;
    public CardType cardType;
    public int _id;
    // Start is called before the first frame update
    public void Init(int level)
    {
        base.Init(level);

        _id = level;
        
        switch (cardType)
        {
            case CardType.Food:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/DarkGreen");
                _id += 1010;
                break;
            case CardType.Water:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/Blue");
                _id += 1020;
                break;
            case CardType.Wood:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>($"Prefabs/Materials/Wood_{level}");
                _id += 2010;
                break;
            case CardType.Stone:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/Purple");
                _id += 2020;
                break;
            case CardType.Combination:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/White");
                _id = 3000;
                break;
            default:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/Black");
                break;
        }

        if (!CardDataDeserializer.TryGetData(_id, out _data))
            Debug.Log("데이터를 불러오는 도중에 문제가 발생했습니다." +
                      $"\n카드 ID : {_id}");

        this.GetComponentInChildren<TMP_Text>().text = _data.KR;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void OnMouseUp()
    {
        var result = Physics.OverlapSphere(transform.position, 5f);
        
        if (result.Length > 2)
            foreach (var v in result)
            {
                try
                {
                    if (v.TryGetComponent(out Mergeable comp))
                    {
                        if (!this.Equals(comp))
                        {
                            //TODO
                            if (comp.transform.parent.TryGetComponent(out CardGroup g1))
                            {
                                if (transform.parent.TryGetComponent(out CardGroup g2))
                                {
                                    //서로 다른 두 CardGroup 간의 결합
                                    if (!g1.Equals(g2))
                                    {
                                        OnMergeEnter(this.gameObject, comp.gameObject);
                                        return;
                                    }
                                    //서로 같은 CardGroup에 속해 있는 카드들 그냥 무시
                                    else
                                    {
                                        continue;
                                    }
                                }
                                //CardGroup을 빈 카드에 결합 시킴
                                else
                                {
                                    OnMergeEnter(this.gameObject, comp.gameObject);
                                    return;
                                }
                            }
                            //빈 카드 끼리의 결합
                            else
                            {
                                OnMergeEnter(this.gameObject, comp.gameObject);
                                return;
                            }
                        
                        }
                    }

                }
                catch
                {
                    continue;
                }
            }
        
        if (_tempGroup != null)
        {
            _tempGroup = null;
        }
        else if (transform.parent.TryGetComponent(out CardGroup cardGroup))
        {
            if (cardGroup.Cards.IndexOf(this) == cardGroup.Count - 1)
            {
                cardGroup.RemoveCard(this);
            }
        }

        this._rigid.isKinematic = false;
    }

    protected override void OnMouseDrag()
    {
        float distance = Camera.main.WorldToScreenPoint(transform.position).z;
        var mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 78);
        var crntPos = Camera.main.ScreenToWorldPoint(mousePos);
        if (this.transform.parent.TryGetComponent(out CardGroup cardGroup))
        {
            if(cardGroup.Cards.IndexOf(this) == 0)
                cardGroup.transform.position = crntPos;
            
            else if (cardGroup.Cards.IndexOf(this) == cardGroup.Cards.Count - 1)
                this.transform.position = crntPos;

            else
            {
                if (_tempGroup == null)
                {
                    var temp = new GameObject("CardGroup");
                    temp.transform.SetParent(CardManager.Instance.transform);
                    _tempGroup = temp.AddComponent<CardGroup>();
                    for (int i = cardGroup.Cards.IndexOf(this); i < cardGroup.Cards.Count;)
                    {
                        var c = cardGroup.RemoveCard(cardGroup.Cards[i]);
                        _tempGroup.AddCard(c);
                    }
                }
                else
                {
                    _tempGroup.transform.position = crntPos;
                }
            }
        }
        else
        {
            this.transform.position = crntPos;
        }
    }
    protected override void OnMouseDown()
    {
        base.OnMouseDown();
    }



    protected override void OnMerge(GameObject t1, GameObject t2)
    {
        
        Debug.Log("OnMerge");
        //t1 => 잡고 있던 카드 || t2 => 바닥에 있던 카드
        
        var destroyTarget = new[] { t2.GetComponent<Card>(), t1.GetComponent<Card>() };
        var cardGroup = new CardGroup[2];
        var flag1 =  t1.transform.parent.TryGetComponent(out cardGroup[0]);
        var flag2 =  t2.transform.parent.TryGetComponent(out cardGroup[1]);
        
        // t1 부모 확인 -> 부모가 CardGroup 인 경우, 즉 t2가 이미 다른 카드와 합쳐져 있던 경우 -> t1의 부모를 t2의 부모로 설정
        //                -> 아닌 경우 -> 새로 CardGroup을 생성해 t1과 t2의 부모로 설정 
        
        var createParent = new Func<CardGroup>(() =>
        {
            var emptyParent = new GameObject("CardGroup");
            emptyParent.transform.SetParent(CardManager.Instance.transform);
            emptyParent.transform.localPosition = t2.transform.localPosition;
            var temp = emptyParent.AddComponent<CardGroup>();
  
            return temp;
        });

        if (flag1 && flag2)
        {
            //한 CardGroup에서 다른 곳으로 이동.
            if(!cardGroup[0].Equals(cardGroup[1]))
            {
                if (cardGroup[0].Cards.IndexOf(destroyTarget[1]) == 0)
                {
                    //카드 그룹 전체를 이동
                    for (int i = 0; i < cardGroup[0].Cards.Count;)
                    {
                        cardGroup[1].AddCard(cardGroup[0].RemoveCard(i));
                    }
                }
                else
                {
                    //한 장만 이동
                    cardGroup[0].RemoveCard(this);
                    cardGroup[1].AddCard(this);
                }
            }
            else
            {
                cardGroup[0].Sort();
            }
            
        }
        else if (flag1)
        {
            var targetParent = createParent();
            
            if (cardGroup[0].Cards.IndexOf(this) == 0 && destroyTarget[1] != null)
            {
                //빈 카드와 CardGroup을 결합
                cardGroup[0].InsertCard(destroyTarget[0]);
            }
            else if (cardGroup[0].Cards.IndexOf(this) != cardGroup[0].Cards.Count - 1)
            {
                // CardGroup에 들어있는걸 빼서 다른 빈 카드와 결합
                cardGroup[0].RemoveCard(this);
                targetParent.AddCardRange(destroyTarget);
            }
            else
            {
                cardGroup[0].RemoveCard(this);
                var emptyParent = createParent();
                emptyParent.AddCardRange(destroyTarget);
            }
            
        }
        else if (flag2)
        {
            // 빈 카드를 카드 그룹으로
            cardGroup[1].AddCard(this);
        }
        else
        {
            CardManager.Areas ??= GameObject.FindGameObjectsWithTag("Merge");
            
            float refXMin = CardManager.Areas[0].transform.position.x - CardManager.Areas[0].transform.localScale.x / 2;
            float refXMax = CardManager.Areas[0].transform.position.x + CardManager.Areas[0].transform.localScale.x / 2;
            float refZMin = CardManager.Areas[0].transform.position.z - CardManager.Areas[0].transform.localScale.z / 2;
            float refZMax = CardManager.Areas[0].transform.position.z + CardManager.Areas[0].transform.localScale.z / 2;
            
            if (t2.transform.position.x > refXMin && t2.transform.position.x < refXMax)
            {    
                if (t2.transform.position.z > refZMin && t2.transform.position.z < refZMax)
                {
                    //병합 분기
                    if ((destroyTarget[0].cardType == destroyTarget[1].cardType) && (destroyTarget[0].level == destroyTarget[1].level) && destroyTarget[0].level < MaxLevel)
                    {

                        var cardInstance = CardManager.CreateCard(level + 1, (int)cardType);
                        CardManager.DestroyCard(destroyTarget);

                        //cardInstance.transform.localScale = Vector3.one;
                        cardInstance.transform.position = CardManager.Areas[1].transform.position + Vector3.up * 2f;
                    }

                    Debug.Log("Merge Successed");
                    return;
                }
            }
            
            // 빈 카드 끼리 결합
            var emptyParent = createParent();
            emptyParent.AddCardRange(destroyTarget);

        }
    }

    //카드 분해 기능
    protected void OnDecomposition()
    {
        CardManager.DestroyCard(this);
        CardManager.CreateCard(this.level - 1, Random.Range(0, 5));
        CardManager.CreateCard(this.level - 1, Random.Range(0, 5));
    }
}
