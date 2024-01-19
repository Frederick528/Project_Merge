using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    private CardData _data;
    private Animator _anim;
    
    public CardData Data => _data;
    public CardType cardType;
    public int ID;
    // Start is called before the first frame update

    private void OnEnable()
    {
        var a = this.GetComponent<Animator>();
        if(CardManager.Instance.sortBtn != null)
            CardManager.Instance.sortBtn.interactable = false;
        //Destroy(a);
    }

    public void Init(int level)
    {
        base.Init(level);

        ID = level;
        
        switch (cardType)
        {
            case CardType.Food:
                ID += 1010;
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>($"Prefabs/Materials/Food/{ID}");
                break;
            case CardType.Water:
                ID += 1020;
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>($"Prefabs/Materials/Water/{ID}");
                break;
            case CardType.Wood:
                ID += 2010;
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>($"Prefabs/Materials/Wood/{ID}");
                break;
            case CardType.Stone:
                ID += 2020;
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>($"Prefabs/Materials/Stone/{ID}");
                break;
            case CardType.Combination:
                ID = 3000;
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>($"Prefabs/Materials/Combination/{ID}");
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
        
        if (!GameManager.Instance.isTutorial )
            InitCheck();

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
                    Resources.Load<Material>($"Prefabs/Materials/Food/{ID}");
                cardType = CardType.Food;
                break;
            case 102:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>($"Prefabs/Materials/Water/{ID}");
                cardType = CardType.Water;
                break;
            case 201:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>($"Prefabs/Materials/Wood/{ID}");
                cardType = CardType.Wood;
                break;
            case 202:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>($"Prefabs/Materials/Stone/{ID}");
                cardType = CardType.Stone;
                break;
            case 300:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>($"Prefabs/Materials/Combination/{ID}");
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
        
        if (!GameManager.Instance.isTutorial )
            InitCheck();    
    }

    private void InitCheck()
    {
        if (!YamlDeserializer.saveData.GetValue(this.ID))
        {
            Debug.Log("카드를 새로이 획득 했습니다!");
            YamlDeserializer.saveData.Modify(this.ID, true);
            YamlDeserializer.Serialize(PictorialData.defaultFilePath, YamlDeserializer.saveData);

            Instantiate(CardManager.Instance.newerCardEffect, this.transform).transform.localPosition += Vector3.up * 2f;
        }
        
        var v = from card in CardManager.Cards
            where card.ID % 10 == this.ID % 10
            select card;
        
        //Debug.Log(ID);
        if (v.Count() >= 8 && (!YamlDeserializer.saveData.GetValueFromLimit(ID % 10) && !GameManager.Instance.isTutorial))
        {
            if (!YamlDeserializer.saveData.ModifyLimit(this.ID % 10, true))
            {
                Debug.Log("failed to serialize");
            }
            YamlDeserializer.Serialize(PictorialData.defaultFilePath, YamlDeserializer.saveData);
        }
        else
        {
           // Debug.Log(v.Count());
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    
    private CardGroup CreateParent(Transform targetPos)
    {
        var emptyParent = new GameObject("CardGroup");
        emptyParent.transform.SetParent(CardManager.Instance.transform);
        emptyParent.transform.localPosition = targetPos.transform.localPosition;
        var temp = emptyParent.AddComponent<CardGroup>();
  
        return temp;
    }
    
    private bool CheckRulesForGroup(CardGroup tg)
    {
        var IDList = tg.Cards.Select(x => x.ID)/*.Distinct()*/;
        var result = false;
        for (int idx = 0; idx < IDList.Count(); idx++)
        {
            var id = IDList.ElementAt(idx);
            var row = tg.Cards
                .Where(x =>(x.ID == id && x.ID < 3000) &&
                           (YamlDeserializer.saveData.GetValueFromLimit((x.ID) % 10)) ||
                           GameManager.Instance.isTutorial)
                .Select(x => x).ToList();

            //홀수 일 경우 짝수로 
            if (row.Count() % 2 == 1)
            {
                tg.RemoveCard(row[^1]);
                row.Remove(row[^1]);
            }
            
            for (int i = 0; i < row.Count;)
            {
                //0번 삭제
                var v = tg.RemoveCard(row[0]);
                var cardInstance = CardManager.CreateCard(v.level + 1, (int)v.cardType, true);

                //생성 하자마자 animator 삭제
                if (cardInstance.TryGetComponent(out Animator anim))
                    Destroy(anim);

                cardInstance.transform.position =
                    CardManager.Areas[1].transform.position + Vector3.up * 2;

                CardManager.DestroyCard(v);
                CardManager.DestroyCard(tg.RemoveCard(row[1]));

                row.Remove(row[0]);
                //1번 -> 0번 
                //다시 0번 삭제
                row.Remove(row[0]);
                result = true;
            }
        }

        if (GameManager.Instance.isTutorial)
        {
            TutorialManager.WaitButtonCallBack = true;
        }
        return result;
    }


    public override void OnMouseUp()
    {
        _rigid.isKinematic = false;
        if (this.transform.parent.TryGetComponent(out CardGroup hg))
        {
            if (hg.IndexOf(this).Equals(hg.Count - 1))
            {
                hg.RemoveCard(this);
                return;
            }
        }
        
        var results = Physics.OverlapSphere(transform.position, 7f);
        CardManager.Instance.sortBtn.interactable = false;

        var bears = from v in results
            where v.TryGetComponent(out Bear b)
            select v.GetComponent<Bear>();

        
        // if (bears.Count() != 0)
        // {
        //     var destroyTarget = this;
        //     if (hg != null)
        //     {
        //         // var oEm = from c in hg.Cards
        //         //     where c.ID is >= 2000 and < 3000
        //         //     orderby c.ID
        //         //     select c;
        //         // var v = new List<Card>(oEm);
        //         //카드 그룹이 있는데, 카드 그룹의 마지막 인덱스가 아닐 경우 리턴
        //         if (hg.IndexOf(this) != hg.Count - 1)
        //             return;
        //         Debug.Log(true);
        //         destroyTarget = hg.RemoveCard(this);
        //     }
        //
        //     if (destroyTarget.ID is >= 2000 and < 3000)
        //     {
        //         bears.ElementAt(0).hitPoint -= destroyTarget.ID % 10 + 1;
        //         if (bears.ElementAt(0).IsDead)
        //         {
        //             bears.ElementAt(0).OnDead();
        //         }
        //         CardManager.DestroyCard(destroyTarget);
        //     }
        //     
        //     return;
        // }
        for (int i = 1; i < results.Length; i++)
        {
            var target = results[i].gameObject;
            if (target.transform.parent == null) continue;
            if (target.Equals(this.gameObject)) continue;
            if (!target.transform.TryGetComponent(out Card c)) continue;

            if (hg != null)
            {
                if (!target.transform.parent.TryGetComponent(out CardGroup tg)) //카드 그룹 + 카드 
                {
                    OnMerge(this.gameObject, target.gameObject);
                    return;
                }
                if (tg.Equals(hg))// 같은 카드 그룹 내, 밑에서 처리
                {
                    Debug.Log("예측 성공");
                    //nothing todo
                }
            }
            if (target.transform.parent.TryGetComponent(out CardGroup g))
            {
                //카드 그룹 째로 내려놓았을 때
                
                if (g.Equals(hg))
                {
                    if(g.IndexOf(c) == 1)
                        OnMerge(this.gameObject, g.gameObject);
                    continue;
                }
                OnMerge(this.gameObject, g.gameObject);
                return;
            }
            
            if (this.ID == c.ID && this.ID < 3000)
            {
                OnMerge(this.gameObject, target);
                return;
            }
            foreach (var rule in CardDataDeserializer.CraftRules)
            {
                if (rule.Contains(this.ID) && rule.Contains(c.ID))
                {
                    Debug.Log(true);
                    OnMerge(this.gameObject, c.gameObject, true);
                    return;
                }
            }
            //
            var emptyParent = CreateParent(target.transform);
            emptyParent.AddCardRange(new [] {c, this});
        }
        CardManager.Instance.sortBtn.interactable = true;
    }
    protected override void OnMouseDrag()
    {
        if (GameManager.CardCanvasOn) return;
        float distance = Camera.main.WorldToScreenPoint(transform.position).z;
        var mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 78);
        var _temp = Camera.main.ScreenToWorldPoint(mousePos);
        var crntPos = new Vector3()
        {
            x = _temp.x,
            y = 7,
            z = _temp.z 
        };

        if (this.transform.parent.TryGetComponent(out CardGroup cardGroup))
        {
            if (cardGroup.IndexOf(this) == 0)
            {
                cardGroup.transform.position = crntPos;
            }

            else if (cardGroup.IndexOf(this) == cardGroup.Count - 1)
                this.transform.position = crntPos;

            //     else
            //     {
            //         if (_tempGroup == null)
            //         {
            //             var temp = new GameObject("CardGroup");
            //             temp.transform.SetParent(CardManager.Instance.transform, true);
            //             _tempGroup = temp.AddComponent<CardGroup>();
            //             for (int i = cardGroup.IndexOf(this); i < cardGroup.Count;)
            //             {
            //                 var c = cardGroup.RemoveCard(i);
            //                 _tempGroup.AddCard(c);
            //             }
            //             cardGroup.Sort();
            //         }
            //     }
            // }
        }
        else
        {
            this.transform.position = crntPos;
        }
    }
    protected override void OnMouseDown()
    {
        if (GameManager.CardCanvasOn) return;

        if (transform.parent.TryGetComponent(out CardGroup g))
        {
            var idx = g.IndexOf(this);
            if (idx != 0)
            {
                if (idx != g.Count - 1 )
                {
                    List<Card> targets = new();
                    CardGroup temp = CreateParent(this.transform);
                    for (int i = g.IndexOf(this); i < g.Count;)
                    {
                        var v = g.RemoveCard(i);
                        temp.AddCard(v);
                    }

                    temp.Sort();
                }
                else
                {
                    g.RemoveCard(this);
                }
            }
        }

        // if (transform.parent.TryGetComponent(out CardGroup cardGroup))
        // {
        //     cardGroup.transform.position = new Vector3()
        //     {
        //         x = cardGroup.transform.position.x,
        //         y = 5,
        //         z = cardGroup.transform.position.z,
        //     };
        // }
        // else
        // {
        //     this.transform.position = new Vector3()
        //     {
        //         x = transform.position.x,
        //         y = 5,
        //         z = transform.position.z,
        //
        //     };
        // }
        base.OnMouseDown();
    }

    protected override void OnMerge(GameObject t1, GameObject t2)
    {
        OnMerge(t1, t2, false);
    }

    private void OnMerge(GameObject t1, GameObject t2, bool ignoreLimit)
    {
        OnMergeEnter();
        
        if (!t1.TryGetComponent(out Card handled) && !t2.TryGetComponent(out Card field))
            return;
        if( handled.ID % 10 > 6)
            return;

        CardManager.Areas ??= GameObject.FindGameObjectsWithTag("Merge");
        if (CardManager.Areas.Length == 0)
        {
            CardManager.Areas = GameObject.FindGameObjectsWithTag("Merge");
            if (CardManager.Areas.Length == 0) return;
        }
        
        float refXMin = CardManager.Areas[0].transform.position.x - CardManager.Areas[0].transform.lossyScale.x / 1.8f;
        float refXMax = CardManager.Areas[0].transform.position.x + CardManager.Areas[0].transform.lossyScale.x / 1.8f;
        float refZMin = CardManager.Areas[0].transform.position.z - CardManager.Areas[0].transform.lossyScale.z / 1.8f;
        float refZMax = CardManager.Areas[0].transform.position.z + CardManager.Areas[0].transform.lossyScale.z / 1.8f;

        var flag = new[]
        {
            (t2.transform.position.x > refXMin && t2.transform.position.x < refXMax),
            (t2.transform.position.z > refZMin && t2.transform.position.z < refZMax)
        };
        
        //target
        if (t2.transform.TryGetComponent(out CardGroup tg)) // a + card group
        {
            if (flag[0] && flag[1]) //put card group onto merging area
            {
                if ( !CheckRulesForGroup(tg))
                {
                    BearManager.Notice($"상위 티어의 카드를 해금하기 위해선\n" +
                                       $"같은 티어의 카드를 8장 이상 모아야합니다.\n" +
                                       $"현재 {level + 1}티어 카드 {CardManager.Cards.Select(x => x).Where(x => x.level == this.level).Count()}장");
                    Debug.Log("규칙 오류 or Merge 조건 미달성");
                }
            } 
            else if (t1.transform.parent.TryGetComponent(out CardGroup hg)) // 카드 그룹 + 카드 그룹 || 그룹 + 카드
            {
                // 해당 카드 그룹의 하위 카드들에서 중복 호출 방지
                if (hg.IndexOf(handled) == 0)
                {
                    //서로 다른 카드 그룹
                    if (!tg.Equals(hg))
                    {
                        for (int i = 0; i < hg.Cards.Count;)
                        {
                            tg.AddCard(hg.RemoveCard(hg.Cards[i]));
                        }
                    }
                    else
                    {
                        // 머지 영역 밖에서 카드 그룹을 내려 놓았을 때 -- cardGroup onto card - 카드 그룹 끼리 병합
                        print("뭔가 잘못됨");
                    }
                }
            }
            // else if (tg.Cards.IndexOf(handled) != 0) //어차피 0만 들어옴
            //     tg.RemoveCard(handled);
            else if (handled != null) //put card onto card group
            {
                //어차피 t1은 이 메소드를 호출하는 객체 일테니 이게 false가 될 일은 없음 .
                tg.AddCard(handled);
            }
        }
        else if (t2.TryGetComponent(out Card card)) // a + card 
        {
            if (flag[0] && flag[1]) // Merge
            {
                if (t1.transform.parent.TryGetComponent(out CardGroup hg)) // put card group onto card;
                {
                    hg.InsertCard(card);
                    
                    if ( !CheckRulesForGroup(hg))
                    {
                        Debug.Log("규칙 오류 or Merge 조건 미달성");
                        BearManager.Notice($"상위 티어의 카드를 해금하기 위해선\n" +
                                           $"같은 티어의 카드를\n" +
                                           $"8장 이상 모아야합니다.\n" +
                                           $"현재 {level + 1}티어 카드 {CardManager.Cards.Select(x => x).Where(x => x.level == this.level).Count()}장");
                    }
                }
                else if (handled != null) //put card onto card
                {
                    if (GameManager.Instance.isTutorial)
                    {
                        TutorialManager.WaitButtonCallBack = true;
                    }
                    Debug.Log(true);
                    if (YamlDeserializer.saveData.GetValueFromLimit(handled.ID % 10) || ignoreLimit)
                    {
                        if ( handled.cardType != CardType.Combination)
                        {
                            if (handled.ID == card.ID)
                            {
                                var c = CardManager.CreateCard(this.level + 1, (int)this.cardType, true);
                                c.transform.position = CardManager.Areas[1].transform.position + Vector3.up * 2;
                                CardManager.DestroyCard(new[] { card, handled });
                                
                                
                            }
                            else if (card.cardType != CardType.Combination)
                            {
                                foreach (var rule in CardDataDeserializer.CraftRules)
                                {
                                    if (rule.Contains(handled.ID) && rule.Contains(card.ID))
                                    {
                                        var c = CardManager.CreateCard(rule[^1], true);
                                        var tPos = CardManager.Areas[1].transform.localPosition + Vector3.up * 2;
                                        c.transform.localPosition = tPos;
                                        CardManager.DestroyCard(new[] { card, handled });
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        Debug.Log("failed " + ignoreLimit );
                        BearManager.Notice($"상위 티어의 카드를 해금하기 위해선\n" +
                            $"같은 티어의 카드를\n" +
                            $"8장 이상 모아야합니다.\n" +
                            $"현재 {level + 1}티어 카드 {CardManager.Cards.Select(x => x).Count(x => x.level == this.level)}장");
                    }
                }
            }
            else if (t1.transform.parent.TryGetComponent(out CardGroup hg)) // put card group onto card -- 작동;
            {
                hg.InsertCard(card);
            }
            else if (handled != null) //put card onto card -- 작동 안함 OnMouseUp에서 직접 처리.
            {
                var emptyParent = CreateParent(card.transform);
                emptyParent.AddCardRange(new [] {card, handled});
            }
        }
        
        CardManager.Instance.sortBtn.interactable = true;
        return; 
    }
    public bool Lapse()
    {
        var result = true;
        
        _data.Date -= 1;
        if (_data.Date <= 0)
            result = false;
        
        return result;
    }

    public void AnimEvt()
    {
        var a = this.GetComponent<Animator>();
        CardManager.CreateQueue.Dequeue();
        CardManager.Instance.sortBtn.interactable =
            CardManager.CreateQueue.Count == 0;
        Destroy(a);
    }

    //카드 분해 기능
    public void OnDecomposition(out Card[] createdCards)
    {
        if (this.level == 0)
        {
            createdCards = null;
            return;
        }
        
        CardManager.DestroyCard(this);
        
        var v = new Card [2];
        v[0] = CardManager.CreateCard(this.level - 1, Random.Range(0, 4));
        v[1] = CardManager.CreateCard(this.level - 1, Random.Range(0, 4));
        
        createdCards = v;
    }

    public static void MoveToLerp(GameObject targetObj, Vector3 targetPos)
    {
        GameManager.Instance.StartCoroutine(Move(targetObj, targetPos));
    }
    static IEnumerator Move(GameObject targetObj, Vector3 targetPos)
    {
        while (true)
        {
            try
            {
                targetObj.transform.position =
                    Vector3.Lerp(targetObj.transform.position, targetPos, 0.4f);

                if (Vector3.Distance(targetObj.transform.position, targetPos) <= 1f)
                {
                    targetObj.transform.position = targetPos;
                    break;
                }
            }
            catch
            {
                break;
            }
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }
}
