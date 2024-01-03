using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mergeable : MonoBehaviour
{
    //해당 오브젝트의 단계. 초기 단계 - 0 / 2 단계 - 1 / 3단계 - 2 ...
    public int level;
    private bool _isInitialized = false;

    public void Init(int level)
    {
        this.level = level;
        _isInitialized = true;
    }

    private void initCheck()
    {
        if (!_isInitialized) throw new Exception("this Mergeable Object has not Initialized.");
    }
    
    // 실제로 합쳤을 때 실행될 메소드
    private void OnMergeEnter()
    {
        initCheck();
        OnMerge();
    }

    // 합쳤을 때 동작할 내용 구현
    protected abstract void OnMerge();

    public void OnMouseUp()
    {
        var arr = new Collider2D[50];
        if (Physics2D.OverlapCircleNonAlloc(transform.position, 1f, arr) > 0) 
            foreach (var v in arr)
            {
                try
                {
                    var comp = v.GetComponent<Mergeable>();

                    if (this.level == comp.level && !this.Equals(comp))
                    {
                        //TODO
                        Debug.Log(true);
                        OnMergeEnter();
                        return;
                    }

                }
                catch (Exception e)
                {
                    continue;
                }
            }
        //this.transform.position = _defPos;
    }
}
