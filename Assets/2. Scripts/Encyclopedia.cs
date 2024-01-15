using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Encyclopedia : MonoBehaviour
{
    public Button toggle;
    public EncStruct structure;

    private void Awake()
    {
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
                        Debug.Log(keyValueFair.Key);
                    });

                    var t = temp.GetComponentInChildren<TMP_Text>();
                    t.text = keyValueFair.Key + "";
                    
                    if (v.transform.childCount >= 6)
                    {
                        v.transform.SetParent(structure.content.transform);
                        v.SetActive(true);
                        v = Instantiate(structure.row);
                    }
                }
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
