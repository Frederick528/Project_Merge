using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using IEnumerable = System.Collections.IEnumerable;

public class BearManager : MonoBehaviour
{
    private static GameObject _bearPrefab = null;
    private static List<Bear> _bears = new ();
    private static BearManager _instance;
    private static Bear _crntBearRef;

    public static List<Bear> Bears => _bears;
    public static BearManager Instance => _instance;
    public static int Count => _bears.Count;

    public Canvas bearApear;

    private void Awake()
    {
        _bearPrefab ??= Resources.Load<GameObject>("Prefabs/Enemy/Bear");
        _instance ??= this;
    }

    public static void Dispense(int count)
    {
        if (Instance.Equals(null))
            throw new Exception("BearManager 가 없습니다.");
            
        _bearPrefab ??= Resources.Load<GameObject>("Prefabs/Enemy/Bear");
        
        for (int i = 0; i < count; i++)
        {
            Debug.Log("곰 출현!");
            var bearInstance = Instantiate(_bearPrefab, Instance.transform).GetComponent<Bear>();
            bearInstance.transform.position = Vector3.left * 160;
            bearInstance.Init();
            _crntBearRef = bearInstance;
            _bears.Add(bearInstance);
        }

        // 프리팹이 생성 되어있는지 확인 후 안 되어 있으면 새로 생성
        Instance.bearApear =
            !Instance.bearApear.gameObject.activeInHierarchy ?
                Instantiate(Instance.bearApear).GetComponent<Canvas>() :
                Instance.bearApear;
        
        foreach (Transform child in Instance.bearApear.transform)
        {
            if (child.TryGetComponent(out Image image))
            {
                if (child.TryGetComponent(out Button comp))
                {
                    comp.onClick.RemoveAllListeners();
                }
                else
                {
                    comp = image.AddComponent<Button>();
                }
                
                comp.onClick.AddListener(() =>
                {
                    Camera.main.transform.position = new Vector3()
                    {
                        x = _crntBearRef.transform.position.x,
                        y = Camera.main.transform.position.y,
                        z = _crntBearRef.transform.position.z
                    };

                    Instance.bearApear.gameObject.SetActive(false);
                });
            }
        }
        
        Instance.bearApear.gameObject.SetActive(true);
    }

    public static void Dispense()
    {
        Dispense(1);
    }

    public static void BearLeave()
    {
        if (_bears.Count == 0)
            return;
        if(Instance.bearApear.gameObject.activeInHierarchy)
            Instance.bearApear.gameObject.SetActive(false);
        foreach (var bear in _bears)
        {
            bear.Leave();
        }
        _bears.Clear();
        Debug.Log("곰들이 떠나갑니다..");
    }

    public static void BearLeave(Bear bear)
    {
        try
        {
            bear.Leave();
            _bears.Remove(bear);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
