using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        _cards.Remove(card);
        card.transform.SetParent(CardManager.Instance.transform);
        card.GetComponent<MeshRenderer>().material.renderQueue = _defRenderQueue;
        
        if(_cards.Count <= 1)
        {
            _cards[0].transform.SetParent(CardManager.Instance.transform);
            _cards[0].GetComponent<MeshRenderer>().material.renderQueue = _defRenderQueue;
            Thread.MemoryBarrier();
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
        Debug.Log(true);
        if (autoDestroyCardGroup) return RemoveCard(card);
        _cards.Remove(card);
        card.transform.SetParent(CardManager.Instance.transform);
        card.GetComponent<MeshRenderer>().material.renderQueue = _defRenderQueue;
        
        Sort();
        return card;
    }

    public Card RemoveCard(int index)
    {
        try
        {
            return RemoveCard(_cards[index]);

        }
        catch (Exception e)
        {
            Debug.Log(index);
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
            _cards[i].transform.localPosition = (Vector3.back * 2f + Vector3.up * 0.15f) * i;
            _cards[i].GetComponent<MeshRenderer>().material.renderQueue = _defRenderQueue + i;
            _cards[i]._rigid.isKinematic = false;
        }
    }
}