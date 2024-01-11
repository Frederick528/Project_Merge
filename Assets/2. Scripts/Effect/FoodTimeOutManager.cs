using System;
using System.Collections.Generic;
using UnityEngine;

public class FoodTimeOutManager : MonoBehaviour
{
    public static Queue<GameObject> EffectQueue = new ();
    public static Queue<Card> DestroyQueue = new();
    public static FoodTimeOutManager Instance;
    public static GameObject Used;
    public GameObject source;

    private void Awake()
    {
        Instance ??= this;
        Used = Instantiate(new GameObject());
    }

    public static void Show(Card Target)
    {
        GameObject o;
        if (EffectQueue.Count == 0)
            o = Instantiate(Instance.source);
        else
            o = EffectQueue.Dequeue();

        o.transform.parent = Used.transform;
        o.transform.position = Target.transform.position += Vector3.up * 1;
        DestroyQueue.Enqueue(Target);
        o.SetActive(true);
    }
}