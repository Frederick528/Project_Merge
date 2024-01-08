using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    private static List<Card> _cards = new ();
    private static GameObject _ogCard;
    public static CardManager Instance;
    public static GameObject[] Areas; // 0 - Merge | 1 - Export
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
            
        return cardInstance;
    }
    public static Card CreateCard(int level, int type )
    {
        var result = CreateCard();
        result.cardType = (Card.CardType)type;
        result.Init(level);
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
    

    private void OnDestroy()
    {
        Instance = null;
    }
}
