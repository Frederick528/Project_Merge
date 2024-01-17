using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopController : MonoBehaviour
{
    public GameObject shopUI;
    public GameObject curseBtn;
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!GameManager.CardCanvasOn)
            {
                GameManager.CardCanvasOn = true;    
                shopUI.SetActive(true);
                return;
            }
            if (shopUI.activeSelf)
            {
                GameManager.CardCanvasOn = false;
                shopUI.SetActive(false);
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            curseBtn.SetActive(!curseBtn.activeSelf);
        }
    } 
}