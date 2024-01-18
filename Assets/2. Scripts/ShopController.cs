using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using System;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;
using Button = UnityEngine.UI.Button;

public class ShopController : MonoBehaviour
{
    private const int cnt = 4;
    public GameObject shopCanvas;
    public GameObject exchangeCanvasUI;
    public GameObject buypanel;

    public Text[] countText = new Text[cnt];
    public Text[] cardCountText = new Text[cnt];
    public int[] buyCnt = new int[cnt];

    public Button[] buyButton = new Button[cnt];
    public Button[] buttons = new Button[cnt];
    public Sprite[] card1Tiers = new Sprite[cnt];
    public Sprite[] card2Tiers = new Sprite[cnt];
    public Sprite[] card3Tiers = new Sprite[cnt];

    private void OnEnable()
    {

    }

    public void BuyCancel()
    {
        foreach (var text in countText)
        {
            text.text = "0";
        }
        buypanel.SetActive(!buypanel.activeSelf);
    }
    public void ExchangeSence()
    {
        exchangeCanvasUI.SetActive(true);
                                                        
    }

    public void ShopUiCancel()
    {
        shopCanvas.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            shopCanvas.SetActive(!shopCanvas.activeSelf);
            SetRandomButtonImg();
        }
    }

    public void IncreaseCount(int idx)
    {
        buyCnt[idx] = Mathf.Clamp(int.Parse(countText[idx].text) + 1, 0, 2);
        countText[idx].text = buyCnt[idx].ToString();
    }

    public void ShowCardCount()
    {
        for (int i = 0; i < cnt; i++)
        {
            cardCountText[i].text = CardManager.Cards.FindAll((c) => c.cardType == (Card.CardType)i && c.level == 1).Count.ToString();
        }
    }

    public void SetRandomButtonImg()
    {
        for (int i = 0; i < cnt; i++)
        {
            buyButton[i].onClick.RemoveAllListeners();
            float rand = Random.Range(0, 0.99f);
            if(rand < 0.5f)
            {
                buyButton[i].image.sprite = card2Tiers[i];
                //버튼 클릭시 온클릭 이벤트 추가
                buyButton[i].onClick.AddListener(() =>
                {
                    for (int i = 0; i < cnt; i++)
                    {
                        buttons[i].image.sprite = card1Tiers[i];
                    }
                });
            }
            else
            {
                buyButton[i].image.sprite = card3Tiers[i];
                buyButton[i].onClick.AddListener(() =>
                {
                    for (int i = 0; i < cnt; i++)
                    {
                        buttons[i].image.sprite = card2Tiers[i];
                    }
                });
            }
        }
    }
}