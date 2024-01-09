using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject shopUI;

    
    void Start()
    {
        shopUI.SetActive(false);
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleShopUI();
        }
    }

    void ToggleShopUI()
    {
        shopUI.SetActive(!shopUI.activeSelf);
    }
}
