using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    private static List<Card> _cards = new ();
    private static GameObject _ogCard;
    
    public static CardManager Instance;
    public static GameObject[] Areas; // 0 - Merge | 1 - Export | 2 - Sort
    public static List<Card> Cards => _cards;
    public static Queue<Card> CreateQueue = new ();
    public Button sortBtn;

    public GameObject newerCardEffect;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance ??= this;
        var v = GameObject.Find("Sort");
        if (v != null)
            v.TryGetComponent<Button>(out sortBtn);
    }

    void Start()
    {
        Areas = GameObject.FindGameObjectsWithTag("Merge");
    }
    public static Card CreateCard()
    {
        var v = CreateCard(false);
        return v;
    }
    
    public static Card CreateCard(bool isOnMerge)
    {
        Card cardInstance;
        
        _ogCard ??= Resources.Load<GameObject>("Prefabs/RedCard");

        cardInstance = Instantiate(_ogCard, Instance.transform).GetComponent<Card>();
        cardInstance.cardType = (Card.CardType)Random.Range(0, Enum.GetValues(typeof(Card.CardType)).Length - 2);
        cardInstance.Init(0);
        _cards.Add(cardInstance);
        
        if (!isOnMerge)
        {
            CameraCtrl.MoveToLerp(new Vector3()
            {
                x = 0,
                y = Camera.main.transform.position.y,
                z = 80
            }, 50);
            CreateQueue.Enqueue(cardInstance);
        }
        else
        {
            if(cardInstance.TryGetComponent(out Animator anim))
                Destroy(anim);
            cardInstance.transform.localPosition = CardManager.Areas[1].transform.localPosition + Vector3.up * 10;
        }


        return cardInstance;
    }

    public static Card CreateCard(int level, int type, bool isOnMerge = false)
    {
        var result = CreateCard(isOnMerge);
        result.cardType = (Card.CardType)type;
        result.Init(level);
        return result;
    }
    public static Card CreateCard(int ID, bool isOnMerge = false)
    {
        var result = CreateCard(isOnMerge);
        result.Init(ID, out bool res);
        result.ID = ID;
        return result;
    }

    public static bool DestroyCard(Card target)
    {
        var result = true;
        try
        {
            if (!target.transform.parent.TryGetComponent(out CardGroup cardGroup))
            {
                _cards.Remove(target);
                Destroy(target.gameObject);
            }
            else
            {
                var v = cardGroup.RemoveCard(target);
                Cards.Remove(v);
                Destroy(v.gameObject);  
            }
        }
        catch (Exception e)
        {
            print(e);
            result = false;
        }

        return result;
    }
    
    public static bool DestroyCard(IEnumerable<Card> targets)
    {
        var result = true;
        try
        {
            var v = _cards.Where(targets.Contains);
            for (int i = 0; i < v.Count();)
            {
                var c = v.ElementAt(i);
                if (c.transform.parent.TryGetComponent(out CardGroup group))
                {
                    group.RemoveCard(c);
                }
                _cards.Remove(c);
                
                Destroy(c.gameObject);
            }
        }
        catch (Exception e)
        {
            result = false;
            Debug.Log(e);
        }

        return result;
    }

    public static void ExpirationDateCheck()
    {
        //var deleteQueue = new Queue<Card>();
        foreach (var card in _cards)
        {
            if (!card.Lapse())
            {
                //deleteQueue.Enqueue(card);
                FoodTimeOutManager.Show(card);
                Debug.Log("유통기한이 만료되어 파괴 되었습니다.");
            }
        }
    }

    public static void SortCard()
    {
        foreach (Transform child in Instance.transform)
        {
            if (child.transform.TryGetComponent(out CardGroup cardGroup))
            {
                if (cardGroup.Count == 0) Destroy(cardGroup.gameObject);
                while (true)
                {
                    if (cardGroup.Count <= 2 )
                    {
                        if(cardGroup.Count <= 0)
                            Destroy(cardGroup);
                        else
                            cardGroup.RemoveCard(0);
                        break;
                    }
                    else
                    {
                        cardGroup.RemoveCard(0);
                    }
                }
            }
        }
        
        
        var idList = _cards.Select( x  => x.ID).Distinct().OrderBy(x => x);
        //var defPos = CardManager.Areas[2].transform.localPosition - Areas[2].transform.localScale / 3 ;
        var margin = Vector3.right * 15;
        var row = 0;
        
        var targetPos = margin * 2 + Vector3.up;
        targetPos = new Vector3()
        {
            x = targetPos.x,
            y = Camera.main.transform.position.y,
            z = targetPos.z
        };
        
        CameraCtrl.MoveToLerp(targetPos, 50);
        
        for (var i = 0 ; i < idList.Count(); i++)
        {
            var id = idList.ElementAt(i);
            var tPos =  margin * (i % 5) + Vector3.back * (row * 25) + Vector3.up;
            var v = _cards.Where(x => x.ID == id).Select(x => x);
            //2장 이상 있을 때 
            if (v.Count() != 1)
            {
                if (!v.ElementAt(0).transform.parent.TryGetComponent(out CardGroup cardGroup))
                {
                    var temp = new GameObject("CardGroup");
                    temp.transform.SetParent(CardManager.Instance.transform);
                    cardGroup = temp.AddComponent<CardGroup>();
                }
                
                Card.MoveToLerp(cardGroup.gameObject, tPos);
                
                foreach (var card in v)
                {
                    if(!cardGroup.Contains(card))
                        cardGroup.AddCard(card);
                }

            }
            else
            {
                Card.MoveToLerp(v.ElementAt(0).gameObject, tPos);
            }

            if ((i + 1) % 5 == 0 && i != 0)
                row += 1;
        }
    }

    public static bool TryGetCardsByID(int id, out Card[] cards)
    {
        var result = true;

        var arr = Cards.Where(x => x.ID == id).Select(x => x);

        if (arr.Count() < 2)
        {
            cards = null;
            result = false;
        }
        else
        {
            cards = arr.ToArray();
        }
        
        return result;
    }

   

    private void OnDestroy()
    {
        Instance = null;
    }
}
