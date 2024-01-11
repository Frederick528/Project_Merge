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

    /// <summary>
    /// 플레이어와 같은 캐릭터
    /// </summary>
    /// <param name="unitCode"></param>
    /// <param name="name">캐릭터 이름</param>
    /// <param name="maxAp">행동력</param>
    /// <param name="maxHunger">배고픔</param>
    /// <param name="maxThirst">갈증</param>
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

    /// <summary>
    /// Stat 셋팅
    /// </summary>
    /// <param name="unitCode">유닛코드(플레이어, 음식)</param>
    /// <returns>stat</returns>
    public static Stat SetUnitStatus(UnitCode unitCode)
    {
        Stat stat = null;

        switch (unitCode)
        {
            case UnitCode.player:
                stat = new Stat(unitCode, "플레이어", 5, 100f, 100f);
                break;
        }
        return stat;
    }
}
