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
<<<<<<< HEAD

    public static bool Notice(string msg)
    {
        global::Notice.isImportant = false;
        return Notice(msg, () =>
        {
            global::Notice.Dispose();
            _turnSkip.interactable = true;
        });
    }
    
    
    public static bool Notice(string msg, Action onClickEvt)
    {
        _turnSkip.interactable = false;
        if (notice) return false;
        notice = true;
        var result = true;
        
        if (Instance.bearApear.gameObject is { activeInHierarchy: false, activeSelf: true } )
        {
            Instance.bearApear =
                Instantiate(Instance.bearApear).GetComponent<Canvas>();
            Debug.Log(true);
        }
        else if (!Instance.bearApear.gameObject.activeSelf)
        {
            Instance.bearApear.gameObject.SetActive(true);
            Debug.Log(true);
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
            btn.onClick.AddListener(() =>
            {
                onClickEvt();
            });
            
        }
        catch (Exception e)
        {
            result = false;
            Debug.Log(e);
        }
        
        notice = false;
        return result;
    }

    public static void RemoveBear(Bear bear)
    {
        try
        {
            _bears.Remove(bear);
        }
        catch (Exception e)
        {
            
        }
    }
=======
>>>>>>> parent of ece03ec (Merge branch 'main' into Taein)
}
