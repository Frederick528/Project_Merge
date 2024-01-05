using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    public static List<Card> Cards = new ();
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
        _ogCard ??= Resources.Load<GameObject>("Prefabs/Card");

        var cardInstance = Instantiate(_ogCard, Instance.transform).GetComponent<Card>();
        cardInstance.cardType = (Card.CardType)Random.Range(0, Enum.GetValues(typeof(Card.CardType)).Length);;
        cardInstance.Init(0);
        Cards.Add(cardInstance);
            
        return cardInstance;
    }
    public static Card CreateCard(int level, int type )
    {
        var result = CreateCard();
        result.cardType = (Card.CardType)type;
        result.Init(level);
        return result;
    }
    
    public static bool DestroyCard(IEnumerable<Card> targets)
    {
        var result = true;
        try
        {
            var v = Cards.Where(targets.Contains);
            for (int i = 0; i < v.Count();)
            {
                var c = v.ElementAt(i);
                Cards.Remove(c);
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

    private void OnDestroy()
    {
        Instance = null;
    }
}
