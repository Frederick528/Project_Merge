using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
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
    public int ID;
    // Start is called before the first frame update
    public void Init(int level)
    {
        base.Init(level);

        ID = level;
        
        switch (cardType)
        {
            case CardType.Food:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/DarkGreen");
                ID += 1010;
                break;
            case CardType.Water:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/Blue");
                ID += 1020;
                break;
            case CardType.Wood:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>($"Prefabs/Materials/Wood_{level}");
                ID += 2010;
                break;
            case CardType.Stone:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/Purple");
                ID += 2020;
                break;
            case CardType.Combination:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/White");
                ID = 3000;
                break;
            default:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/Black");
                break;
        }

        if (!CardDataDeserializer.TryGetData(ID, out _data))
            Debug.Log("데이터를 불러오는 도중에 문제가 발생했습니다." +
                      $"\n카드 ID : {ID}");

        this.GetComponentInChildren<TMP_Text>().text = _data.KR;
    }
    public void Init(int ID, out bool temp)
    {
        temp = true;
        this.ID = ID;
        level = ID % 10;
        
        switch (ID / 10)
        {
            case 101:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/DarkGreen");
                cardType = CardType.Food;
                break;
            case 102:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/Blue");
                cardType = CardType.Water;
                break;
            case 201:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>($"Prefabs/Materials/Wood_{level}");
                cardType = CardType.Wood;
                break;
            case 202:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/Purple");
                cardType = CardType.Stone;
                break;
            case 300:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/White");
                cardType = CardType.Combination;
                level = 5;
                break;
            default:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/Black");
                break;
        }
        
        if (!CardDataDeserializer.TryGetData(ID, out _data))
            Debug.Log("데이터를 불러오는 도중에 문제가 발생했습니다." +
                      $"\n카드 ID : {ID}");

        this.GetComponentInChildren<TMP_Text>().text = _data.KR;
    }
    
    // Update is called once per frame
    void Update()
    {
        base.Update();
    }


    public override void OnMouseUp()
    {
        if (GameManager.CardCanvasOn) return;
        var result = Physics.OverlapSphere(transform.position, 7f);
        var mergeTarget = new List<Card>();
        //리스트 복사
        var craftRules = new List<int[]>(CardDataDeserializer.CraftRules);
        mergeTarget.Add(this);
        
        for (int i = 0; i < result.Length; i++)
        {
            if (result[i].gameObject.Equals(this.gameObject))
                continue;


            if (!result[i].transform.parent.TryGetComponent(out CardGroup g11) || !this.transform.parent.TryGetComponent(out CardGroup g12))
            {
                if (result[i].TryGetComponent(out Card card))
                {
                    if (card.ID == this.ID)
                    {
                        OnMergeEnter(this.gameObject, result[i].gameObject);
                        return;
                    }
                    foreach (var rule in CardDataDeserializer.CraftRules)
                    {
                        if (rule.Contains(this.ID) && rule.Contains(card.ID))
                        {
                            Debug.Log(rule[^1]);
                            var cardInstance = CardManager.CreateCard(rule[^1]);
                            CardManager.DestroyCard(new[] { this, card });

                            //cardInstance.transform.localScale = Vector3.one;
                            cardInstance.transform.position =
                                CardManager.Areas[1].transform.position + Vector3.up * 2f;
                        }
                    }
                    OnMergeEnter(this.gameObject, result[i].gameObject);
                    return;
                }
            }
            else
            {
                if(result[i].transform.parent.TryGetComponent(out CardGroup g1) && this.transform.parent.TryGetComponent(out CardGroup g2))
                    if(g1.Equals(g2))
                        continue;
                if(result[i].TryGetComponent(out Card card))
                {
                    OnMergeEnter(this.gameObject, card.gameObject);
                    return;
                }
                else
                    continue;
            }
        }
        
        if (_tempGroup != null)
        {
            _tempGroup = null;
        }
        else if (transform.parent.TryGetComponent(out CardGroup cardGroup))
        {
            if (cardGroup.IsLastElement(this))
            {
                cardGroup.RemoveCard(this);
            }
        }
        
        this._rigid.isKinematic = false;
    }

    protected override void OnMouseDrag()
    {
        if (GameManager.CardCanvasOn) return;
        float distance = Camera.main.WorldToScreenPoint(transform.position).z;
        var mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 78);
        var crntPos = Camera.main.ScreenToWorldPoint(mousePos);
        if (this.transform.parent.TryGetComponent(out CardGroup cardGroup))
        {
            if(cardGroup.IndexOf(this) == 0)
                cardGroup.transform.position = crntPos;
            
            else if (cardGroup.IndexOf(this) == cardGroup.Count - 1)
                this.transform.position = crntPos;

            else
            {
                if (_tempGroup == null)
                {
                    var temp = new GameObject("CardGroup");
                    temp.transform.SetParent(CardManager.Instance.transform);
                    _tempGroup = temp.AddComponent<CardGroup>();
                    for (int i = cardGroup.IndexOf(this); i < cardGroup.Count;)
                    {
                        var c = cardGroup.RemoveCard(i);
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
        if (GameManager.CardCanvasOn) return;

        if (transform.parent.TryGetComponent(out CardGroup cardGroup))
        {
            cardGroup.transform.position = new Vector3()
            {
                x = cardGroup.transform.position.x,
                y = 3,
                z = cardGroup.transform.position.z,

            };
        }
        else
        {
            this.transform.position = new Vector3()
            {
                x = transform.position.x,
                y = 3,
                z = transform.position.z,

            };
        }
        
        base.OnMouseDown();
    }



    protected override void OnMerge(GameObject t1, GameObject t2)
    {
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
                if (cardGroup[0].IndexOf(destroyTarget[1]) == 0)
                {
                    //카드 그룹 전체를 이동
                    for (int i = 0; i < cardGroup[0].Count;)
                    {
                        cardGroup[1].AddCard(cardGroup[0].RemoveCard(i));
                    }
                }
                else
                {
                    Debug.Log(true);
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
            
            if (cardGroup[0].IndexOf(this) == 0 && destroyTarget[1] != null)
            {
                //빈 카드와 CardGroup을 결합
                cardGroup[0].InsertCard(destroyTarget[0]);
            }
            else if (!cardGroup[0].IsLastElement(this))
            {
                // CardGroup에 들어있는걸 빼서 다른 빈 카드와 결합
                cardGroup[0].RemoveCard(this);
                targetParent.AddCardRange(destroyTarget);
            }
            else
            {
                cardGroup[0].RemoveCard(this);
                cardGroup[0].gameObject.name = "";
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
            
            float refXMin = CardManager.Areas[0].transform.position.x - CardManager.Areas[0].transform.lossyScale.x / 2;
            float refXMax = CardManager.Areas[0].transform.position.x + CardManager.Areas[0].transform.lossyScale.x / 2;
            float refZMin = CardManager.Areas[0].transform.position.z - CardManager.Areas[0].transform.lossyScale.z / 2;
            float refZMax = CardManager.Areas[0].transform.position.z + CardManager.Areas[0].transform.lossyScale.z / 2;
            
            if (t2.transform.position.x > refXMin && t2.transform.position.x < refXMax)
            {    
                if (t2.transform.position.z > refZMin && t2.transform.position.z < refZMax)
                {
                    //병합 분기
                    if (destroyTarget[0].ID == destroyTarget[1].ID)
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

    public bool Lapse()
    {
        var result = true;
        
        _data.Date -= 1;
        if (_data.Date <= 0)
            result = false;
        
        return result;
    }

    //카드 분해 기능
    protected void OnDecomposition()
    {
        CardManager.DestroyCard(this);
        CardManager.CreateCard(this.level - 1, Random.Range(0, 5));
        CardManager.CreateCard(this.level - 1, Random.Range(0, 5));
    }
}
