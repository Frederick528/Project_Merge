using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//relay between GameCore and UnityAPI
public class CoreController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameCore _core;
    public TMP_Text text;

    private void Awake()
    {
        _core ??= new GameCore();
    }

    void Start()
    {
        _core.InitGame();
    }

    private void Update()
    {
        //text.text = GameCore.Stats.Strave + "";
    }

    public void TurnChange()
    {
        _core.TurnChange();
    }

    private void OnDestroy()
    {
        _core.EndGame();
    }
}
