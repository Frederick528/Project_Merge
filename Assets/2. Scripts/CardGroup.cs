using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using UnityEngine;

public class CardGroup : MonoBehaviour
{
    public List<Card> Cards = new();

    private static int _defRenderQueue = 2000;
    public int Count => Cards.Count;

    public void AddCard(Card card)
    {
        Cards.Add(card);
        card.transform.SetParent(this.transform);
        card.transform.localPosition = Vector3.zero;
        Sort();
    }
    public void InsertCard(Card card)
    {
        Cards.Insert(0, card);
        card.transform.SetParent(this.transform);
        card.transform.localPosition = Vector3.zero;
        Sort();
    }
    public void AddCardRange(IEnumerable<Card> cards)
    {
        Cards.AddRange(cards);
        foreach (var card in cards)
        {
            card.transform.SetParent(this.transform);
            card.transform.localPosition = Vector3.zero;
        }
        Sort();
    }
    public Card RemoveCard(Card card)
    {
        Cards.Remove(card);
        card.transform.SetParent(CardManager.Instance.transform);
        card.GetComponent<MeshRenderer>().material.renderQueue = _defRenderQueue;
        
        if(Cards.Count == 1)
        {
            Cards[0].transform.SetParent(CardManager.Instance.transform);
            Cards[0].GetComponent<MeshRenderer>().material.renderQueue = _defRenderQueue;
            Thread.MemoryBarrier();
            Destroy(this.gameObject);
        }
        else
        {
            Sort();
        }

        return card;
    }

    public Card RemoveCard(int index)
    {
        return RemoveCard(Cards[index]);
    }

    public void Sort()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].transform.localPosition = ( Vector3.back * 2f + Vector3.up * 0.5f) * i  + Vector3.down * 0.5f;
            Cards[i].GetComponent<MeshRenderer>().material.renderQueue = _defRenderQueue + i;
            Cards[i]._rigid.isKinematic = false;
        }
    }
}