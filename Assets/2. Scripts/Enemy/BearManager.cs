using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using IEnumerable = System.Collections.IEnumerable;

public class BearManager : MonoBehaviour
{
    private static GameObject _bearPrefab = null;
    private static List<Bear> _bears = new ();
    private static BearManager _instance;
    private static Bear _crntBearRef;
    private static bool notice; 

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
        
        Notice("곰이 나타났다!\n\n파란 카드를 사용해 막아보자", () =>
        {
            for (int i = 0; i < count; i++)
            {
                var bearInstance = Instantiate(_bearPrefab, Instance.transform).GetComponent<Bear>();
                bearInstance.transform.position = Vector3.left * 160;
                bearInstance.Init();
                _crntBearRef = bearInstance;
                _bears.Add(bearInstance);
                
                Debug.Log("곰 출현!");
            }
            
            Camera.main.transform.position = new Vector3()
            {
                x = _crntBearRef.transform.position.x,
                y = Camera.main.transform.position.y,
                z = _crntBearRef.transform.position.z
            };

            global::Notice.Dispose();


        });
        
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

    public static bool Notice(string msg)
    {
        return Notice(msg, () => { global::Notice.Dispose(); });
    }
    
    
    public static bool Notice(string msg, Action onClickEvt)
    {
        if (notice) return false;
        notice = true;
        var result = true;
        
        if (Instance.bearApear.gameObject is { activeInHierarchy: false, activeSelf: true } )
        {
            Instance.bearApear =
                Instantiate(Instance.bearApear).GetComponent<Canvas>();
        }
        
        try
        {
            var textField = Instance.bearApear.GetComponentInChildren<TMP_Text>();
            var image = Instance.bearApear.GetComponentInChildren<Image>();
            textField.text = msg;

            if (!image.TryGetComponent(out Button btn))
            {
                btn = image.AddComponent<Button>();
            }

            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(new UnityAction(onClickEvt));
        }
        catch (Exception e)
        {
            result = false;
            Debug.Log(e);
        }
        
        if (!Instance.bearApear.gameObject.activeSelf)
        {
            Instance.bearApear.gameObject.SetActive(true);
        }
        notice = false;
        return result;
    }
}
