using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopController : MonoBehaviour
{
    public GameObject shopUI;
    public TextMeshProUGUI goldText;
    public Button buyButton;

    [SerializeField]
    private int goldAmount = 0;

    void Start()
    {
        //shopUI.SetActive(false);
        UpdateGoldText();
        buyButton.onClick.AddListener(BuyItem);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            IncreaseGold(100);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!GameManager.CardCanvasOn)
            {
                GameManager.CardCanvasOn = true;    
                shopUI.SetActive(true);
                //ToggleShopUI(true);
                return;
            }
            if (shopUI.activeSelf)
            {
                GameManager.CardCanvasOn = false;
                shopUI.SetActive(false);
                //ToggleShopUI(true);
                return;
            }
        }
    }

    //void ToggleShopUI(bool setActive)
    //{
    //    shopUI.SetActive(setActive);
    //}

    void IncreaseGold(int amount)
    {
        goldAmount += amount;
        UpdateGoldText();
    }

    void UpdateGoldText()
    {
        goldText.text = "Gold: " + goldAmount.ToString();
    }

    void BuyItem() 
    {
        if (goldAmount >= 50) 
        {
            goldAmount -= 50;
            UpdateGoldText();
            Debug.Log("카드를 구매하였습니다.");
        }
        else
        {   
            Debug.Log("골드가 충분하지 않습니다.");
        }
    }
}
