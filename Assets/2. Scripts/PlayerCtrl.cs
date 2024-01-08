using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public Stat stat;

    // Start is called before the first frame update
    private void Awake()
    {
        stat = Stat.SetUnitStatus(UnitCode.player);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            stat.curHunger -= 10f;
            stat.curThirst -= 10f;
        }

        if (stat.curHunger <= 0 || stat.curThirst <= 0)
        {
            
        }
    }
}
