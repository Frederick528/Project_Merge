using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Encyclopedia : MonoBehaviour
{
    public Button toggle;
    public EncStruct structure;

    public Canvas encyclopediaCanvas;

    private void Awake()
    {
        encyclopediaCanvas.worldCamera = GameObject.Find("UI_Camera").GetComponent<Camera>();
        toggle.onClick.AddListener(() =>
        {
            structure.root.SetActive(!structure.root.activeSelf);

            if (structure.root.activeSelf)
            {
                var Q = new Queue<Transform>();
                foreach (Transform transform in structure.content.transform)
                {
                    if (!transform.Equals(structure.content.transform))
                    {
                        Q.Enqueue(transform);
                    }
                }

                for (int i = 0; i < Q.Count;)
                {
                    Destroy(Q.Dequeue().gameObject);
                }

                var v = Instantiate(structure.row);
                foreach (var keyValueFair in YamlDeserializer.saveData.dict)
                {
                    var temp = Instantiate(structure.template, v.transform);

                    var btn = temp.gameObject.AddComponent<Button>();
                    btn.interactable = keyValueFair.Value;
                    
                    btn.onClick.AddListener(() =>
                    {
                        if (GameManager.CardCanvasOn) return;
                        MouseRightClick.Instance.ShowCardInfo(keyValueFair.Key);
                    });

                    var t = temp.GetComponentInChildren<TMP_Text>();
                    t.text = keyValueFair.Key + "";
                    
                    if (v.transform.childCount >= 6)
                    {
                        var tfm = v.GetComponent<RectTransform>();
                        tfm.SetParent(structure.content.transform, true);
                        tfm.localPosition = Vector3.zero;
                        tfm.localScale = Vector3.one;
                        v.SetActive(true);
                        v = Instantiate(structure.row);
                    }
                }
                
                var tf = v.GetComponent<RectTransform>();
                tf.SetParent(structure.content.transform, true);
                tf.localPosition = Vector3.zero;
                tf.localScale = Vector3.one;
                v.SetActive(true);
            }
        });
    }
}

[Serializable]
public struct EncStruct
{
    public GameObject root;
    public GameObject content;
    public GameObject row;
    public Image template;
}
