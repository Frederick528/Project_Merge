using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager<T> : MonoBehaviour where T : MonoBehaviour
{
    public readonly Queue<T> objQueue = new ();
    private GameObject _source;
    // Start is called before the first frame 

    public void SetReference(string path)
    {
       // _source = Resources.Load<GameObject>(path);
    }
}
