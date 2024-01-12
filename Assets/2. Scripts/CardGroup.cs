using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using UnityEngine;

public class CardGroup : MonoBehaviour
{
    private List<Card> _cards = new();
    private static int _defRenderQueue = 2000;
    public int Count => _cards.Count;
    public List<Card> Cards => _cards;
    
    public void AddCard(Card card)
    {
        // if (Cards.Contains(card))
        //     return;
        _cards.Add(card);
        card.transform.SetParent(this.transform);
        card.transform.localPosition = Vector3.zero;
        Sort();
    }
    public void InsertCard(Card card)
    {
        _cards.Insert(0, card);
        card.transform.SetParent(this.transform);
        card.transform.localPosition = Vector3.zero;
        Sort();
    }
    public void AddCardRange(IEnumerable<Card> cards)
    {
        _cards.AddRange(cards);
        foreach (var card in cards)
        {
            card.transform.SetParent(this.transform);
            card.transform.localPosition = Vector3.zero;
        }
        Sort();
    }
    public Card RemoveCard(Card card)
    {
        if (card.Equals(null)) return null;
        _cards.Remove(card);
        card.transform.SetParent(CardManager.Instance.transform, true);
        card.GetComponent<MeshRenderer>().material.renderQueue = _defRenderQueue;
        
        if(_cards.Count <= 1 )
        {
            if(_cards.Count != 0)
            {
                _cards[0].transform.SetParent(CardManager.Instance.transform, true);
                _cards[0].transform.localPosition = _cards[0].transform.localPosition += Vector3.up * 1.5f;
                _cards[0].GetComponent<MeshRenderer>().material.renderQueue = _defRenderQueue;
                Thread.MemoryBarrier();
            }
            Destroy(this.gameObject);
        }
        else
        {
            Sort();
        }

        return card;
    }
    public Card RemoveCard(Card card, bool autoDestroyCardGroup)
    {
        if (autoDestroyCardGroup) return RemoveCard(card);
        _cards.Remove(card);
        card.transform.SetParent(CardManager.Instance.transform, true);
        card.GetComponent<MeshRenderer>().material.renderQueue = _defRenderQueue;
        
        Sort();
        return card;
    }

    public Card RemoveCard(int index)
    {
        try
        {
            if(Count > 0)
                return RemoveCard(_cards[index]);
        }
        catch (Exception e)
        {
            Debug.Log(index);
            Debug.Log(e);
        }
        
        return null;
    }
    
    public bool Contains(Card card)
    {
        return _cards.Contains(card);
    }
    
    public int IndexOf(Card card)
    {
        return _cards.IndexOf(card);
    }

    public bool IsLastElement(Card card)
    {
        return card.Equals(_cards[^1]);
    }


    public void Sort()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            try
            {
                _cards[i].transform.localPosition = _cards[0].transform.localPosition +
                                                    (Vector3.back * 2f + Vector3.up * 0.12f) * i + Vector3.up * 0.12f;
                _cards[i].GetComponent<MeshRenderer>().material.renderQueue = _defRenderQueue + i;
                _cards[i]._rigid.isKinematic = false;
            }
            catch
            {
                
            }
        }
    }
}