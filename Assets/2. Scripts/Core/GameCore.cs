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
    private Stat _status;
    public int TurnCnt => _turnCnt;
    public bool IsGameStarted => _isGameStarted;
    public Stat Status => _status;
    
    
    
    //낮인지 밤인지 구분하기 위한 맴버
    public bool IsDayTime => _turnCnt % 2 == 0 ? true : false;
    
    public void InitGame()
    {
        //Start Game
        //It's Called on Start();
        _isGameStarted = true;
        _status = StatManager.instance.playerCtrl.stat;
        
        _status.curHunger = _status.maxHunger;
        _status.curThirst = _status.maxThirst;
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

        //밤일 경우
        if (!IsDayTime)
        {
            #region GameOverTrigger
                if (!(ModifyHunger(-1) || ModifyThirst(-1)))
                    EndGame();
            #endregion
            
            Debug.Log("It's Night Time");
        }
        
        _status.curAp = _status.maxAp;
        Debug.Log($"Turn : {TurnCnt}");
    }

    public bool ModifyAP(int amount)
    {
        var result = _status.curAp + amount > 0;
        if (result)
            _status.curAp += amount;
        else
            EndGame();
        return result;
    }
    public bool ModifyHunger(int amount)
    {
        var result = _status.curHunger + amount > 0;
        if (result)
            _status.curHunger += amount;
        return result;
    }
    public bool ModifyThirst(int amount)
    {
        var result = _status.curThirst + amount > 0;
        if (result)
            _status.curThirst += amount;
        return result;
    }
}
