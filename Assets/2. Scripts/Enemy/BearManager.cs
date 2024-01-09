using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearManager : MonoBehaviour
{
    private static GameObject _bearPrefab = null;
    private static List<Bear> _bears = new ();
    private static BearManager _instance;

    public static List<Bear> Bears => _bears;

    public static BearManager Instance => _instance;

    private void Awake()
    {
        _bearPrefab ??= Resources.Load<GameObject>("Prefabs/Enemy/Bear");
        _instance ??= this;
    }

    public static void Dispense(int count)
    {
        _bearPrefab ??= Resources.Load<GameObject>("Prefabs/Enemy/Bear");
        
        for (int i = 0; i < count; i++)
        {
            
            _bears.Add(Instantiate(_bearPrefab, Instance.transform, true).GetComponent<Bear>());
        }
    }

    public static void Dispense()
    {
        Dispense(1);
    }
}
