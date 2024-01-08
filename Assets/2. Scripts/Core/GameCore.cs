using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameCore
{
    private enum TimeStatus
    {
        Morning, Day, Night, Dawn
    }
    //낮 밤 상관 없이 턴이 끝날 때 마다 턴 카운트가 오르며, 짝수 (0,2...)가 낮, 홀수가 밤.
    private int _turnCnt;
    private bool _isGameStarted = true;
    private Stat _status;
    public int TurnCnt => _turnCnt;
    public bool IsGameStarted => _isGameStarted;
    public Stat Status => _status;

    public ushort Difficulty = 1;
    
    
    //낮인지 밤인지 구분하기 위한 맴버
    public bool IsMorning => _turnCnt % 4 == (int)TimeStatus.Morning ? true : false;
    public bool IsDayTime => _turnCnt % 4 == (int)TimeStatus.Day ? true : false;
    public bool IsNightTime => _turnCnt % 4 == (int)TimeStatus.Night ? true : false;
    public bool IsDawn => _turnCnt % 4 == (int)TimeStatus.Dawn ? true : false;
    
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

        //새벽 -> 아침인 상황에서
        if (IsMorning)
        {
            #region GameOverTrigger
                if (!(ModifyHunger(-5 * Difficulty) && ModifyThirst(-5 * Difficulty)))
                    EndGame();
            #endregion
            
            Debug.Log("It's Night Time");
        }
        //밤일 때
        if(!IsNightTime)
        {
            _status.curAp = _status.maxAp;
        }
        Debug.Log($"Turn : {TurnCnt}");
    }

    public bool ModifyAP(int amount)
    {
        var result = _status.curAp + amount > 0;
        if (result)
            _status.curAp += amount;
        //else
        //    EndGame();
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
