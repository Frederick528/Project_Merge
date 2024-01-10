using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    #region Members
    private Animator m_Animator;

    #endregion Members


    #region Methods
    void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void StartAnimation()
    {

    }

    public void EndAnimation()
    {
        transform.gameObject.SetActive(false);
    }

    #endregion Methods
}
