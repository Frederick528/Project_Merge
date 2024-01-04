using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitCode
{
    player
}
public class Stat
{
    public UnitCode unitCode { get; }   // 바꿀 수 없게 get만
    public string name { get; set; }
    public int maxAp { get; set; }    // Active Point (행동력)
    public int curAp { get; set; }
    public float maxHunger { get; set; }
    public float curHunger { get; set; }
    public float maxThirst { get; set; }
    public float curThirst { get; set; }


    public Stat()
    {

    }

    public Stat(UnitCode unitCode, string name, int maxAp, float maxHunger, float maxThirst)
    {
        this.unitCode = unitCode;
        this.name = name;
        this.maxAp = maxAp;
        curAp = maxAp;
        this.maxHunger = maxHunger;
        curHunger = maxHunger;
        this.maxThirst = maxThirst;
        curThirst = maxThirst;
    }

    public Stat SetUnitStatus(UnitCode unitCode)
    {
        Stat stat = null;

        switch (unitCode)
        {
            case UnitCode.player:
                stat = new Stat(unitCode, "플레이어", 10, 100f, 100f);
                break;
        }
        return stat;
    }
}
