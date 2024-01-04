using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameCore
{
    public static Status Stats;
    public static int TurnCnt;
    
    private static bool _isGameStarted = true;

    public static bool IsGameStarted = false;
    
    public static void InitGame()
    {
        //Start Game
        Stats = new Status() { Hp = 100 };
        _isGameStarted = true;
    }

    public static void EndGame()
    {
        //When the game ends, call this method.
        if (!_isGameStarted)
            throw new Exception("The Game is not Started");
        _isGameStarted = false;
    }

    public static void TurnChange()
    {
        if (!_isGameStarted)
            throw new Exception("The Game is not Started");
        TurnCnt++;
        
        //TODO
        if(Interlocked.Exchange(ref Stats.Hp, Stats.Hp -= 1) <= 0)
            EndGame();
        
        Debug.Log($"Turn : {TurnCnt}");
    }

    public struct Status
    {
        public ushort param1;
        public int param2;
        public float param3;
        public double param4;

        public int Hp;


    }
}
