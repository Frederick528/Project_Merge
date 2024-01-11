using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FoodTimeOutManager
{
    public static readonly Queue<GameObject> EffectQueue = new ();
    public static GameObject source;

    public static void Show(Card Target)
    {
        GameObject o;
        if (EffectQueue.Count == 0)
            o = GameObject.Instantiate(source);
        else
            o = EffectQueue.Dequeue();


        o.transform.position = Target.transform.position += Vector3.up * 1;
        o.SetActive(true);
        //if (o.TryGetComponent())
    }
}