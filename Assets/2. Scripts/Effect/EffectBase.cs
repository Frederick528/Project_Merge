using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBase : MonoBehaviour
{
    public virtual void AnimEvt()
    {
        this.gameObject.SetActive(false);
    }
}
