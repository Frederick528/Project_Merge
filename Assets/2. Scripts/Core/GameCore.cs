using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameCore
{
    //낮 밤 상관 없이 턴이 끝날 때 마다 턴 카운트가 오르며, 짝수 (0,2...)가 낮, 홀수가 밤.
    private int _turnCnt;
    
    private bool _isGameStarted = true;

    public int TurnCnt => _turnCnt;
    public bool IsGameStarted => _isGameStarted;
    
    //낮인지 밤인지 구분하기 위한 맴버
    public bool IsDayTime => _turnCnt % 2 == 0 ? true : false;
    
    public void InitGame()
    {
        //Start Game
        //Stats = new Status() { Strave = 100 };
        _isGameStarted = true;
    }

    public void EndGame()
    {
        //When the game ends, call this method.
        if (!_isGameStarted)
            throw new Exception("The Game is not Started");
        _isGameStarted = false;
    }

    public void TurnChange()
    {
        if (!_isGameStarted)
            throw new Exception("The Game is not Started");
        _turnCnt++;
        
        //if(Interlocked.Exchange(ref Stats.Strave, Stats.Strave -= 1) <= 0)
        if(false)
            EndGame();
        
        Debug.Log($"Turn : {TurnCnt}");
    }
}
