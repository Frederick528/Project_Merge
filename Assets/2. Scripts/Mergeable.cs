using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Mergeable : Draggable
{
    //음식 카드의 경우 100번대, 아닐 경우 200번대
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
    protected void OnMergeEnter(GameObject t1, GameObject t2)
    {
        initCheck();
        OnMerge(t1, t2);
    }

    // 합쳤을 때 동작할 내용 구현
    protected abstract void OnMerge(GameObject t1, GameObject t2);

    public abstract void OnMouseUp();
}
