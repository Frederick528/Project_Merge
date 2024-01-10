using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    private static List<Card> _cards = new ();
    private static GameObject _ogCard;
    
    public static CardManager Instance;
    public static GameObject[] Areas; // 0 - Merge | 1 - Export | 2 - Sort

    // Start is called before the first frame update
    private void Awake()
    {
        Instance ??= this;
    }

    void Start()
    {
        Areas = GameObject.FindGameObjectsWithTag("Merge");
    }
    public static Card CreateCard()
    {
        _ogCard ??= Resources.Load<GameObject>("Prefabs/RedCard");

        var cardInstance = Instantiate(_ogCard, Instance.transform).GetComponent<Card>();
        cardInstance.cardType = (Card.CardType)Random.Range(0, Enum.GetValues(typeof(Card.CardType)).Length);;
        cardInstance.Init(0);
        _cards.Add(cardInstance);
        
        //SortCard();
        
        return cardInstance;
    }
    public static Card CreateCard(int level, int type )
    {
        var result = CreateCard();
        result.cardType = (Card.CardType)type;
        result.Init(level);
        return result;
    }

    public static Card CreateCard(int ID)
    {
        var result = CreateCard();
        result.Init(ID, out bool res);
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
                GC.SuppressFinalize(target.gameObject);
            }
            else
            {
                cardGroup.RemoveCard(target);
            }
        }
        catch (Exception e)
        {
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
                Debug.Log(i);
                var c = v.ElementAt(i);
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
        var deleteQueue = new Queue<Card>();
        foreach (var card in _cards)
        {
            if (!card.Lapse())
            {
                deleteQueue.Enqueue(card);
                Debug.Log("유통기한이 만료되어 파괴 되었습니다.");
            }
        }

        DestroyCard(deleteQueue);
    }

    public static void SortCard()
    {
        var idList = _cards.Select( x  => x.ID).Distinct().OrderBy(x => x);
        //var defPos = CardManager.Areas[2].transform.localPosition - Areas[2].transform.localScale / 3 ;
        var margin = Vector3.right * 15;
        var row = 0;
        for (var i = 0 ; i < idList.Count(); i++)
        {
            var id = idList.ElementAt(i);
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
                
                cardGroup.transform.position = margin * (i % 5) + Vector3.back * (row * 25) + Vector3.up;
                
                foreach (var card in v)
                {
                    if(!cardGroup.Cards.Contains(card))
                        cardGroup.AddCard(card);
                }

            }
            else
            {
                v.ElementAt(0).transform.position = margin * (i % 5) + Vector3.back * (row * 25)  +  Vector3.up;
            }
            Debug.Log(row);

            if ((i + 1) % 5 == 0 && i != 0)
                row += 1;
        }
    }
    

    private void OnDestroy()
    {
        Instance = null;
    }
}
