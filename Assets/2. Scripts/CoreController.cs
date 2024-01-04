using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//relay between GameCore and UnityAPI
public class CoreController : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text text;
    void Start()
    {
        GameCore.InitGame();
    }

    private void Update()
    {
        text.text = GameCore.Stats.Hp + "";
    }

    public void TurnChange()
    {
        GameCore.TurnChange();
    }

    private void OnDestroy()
    {
        GameCore.EndGame();
    }
}
