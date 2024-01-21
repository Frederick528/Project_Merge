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
using System.Net.NetworkInformation;
using Cysharp.Threading.Tasks.CompilerServices;

public class ShopController : MonoBehaviour
{
    private const int cnt = 4;
    private const int term = 2;

    public GameObject shopCanvas;
    //public GameObject exchangeCanvasUI;
    public GameObject buypanel;

    public Text[] countText = new Text[cnt];
    public Text[] cardCountText = new Text[cnt];

    public Card[][] cardCount = new Card[cnt][];
    public int[] buyCnt = new int[cnt];

    public Button[] buyButton = new Button[cnt];
    public GameObject[] soldOut = new GameObject[cnt];

    public Button[] buttons = new Button[cnt];
    public Sprite[] card1Tiers = new Sprite[cnt];
    public Sprite[] card2Tiers = new Sprite[cnt];
    public Sprite[] card3Tiers = new Sprite[cnt];

    public int[] cardID = new int[] { 1010, 1020, 2010, 2020 };
    //private bool[] canBuy = new bool[4] { true, true, true, true };
    private int buyIdx;
    public string[] cardType = new string[] {"Food", "Water", "Wood", "Stone"};

    public int selectCardCount;
    public int selectCardID;
    //private List<int> purchasedCardIDs = new List<int>();
    private bool[] soldOutBool = new bool[cnt];

    //private void Start()
    //{
    //    card1Tiers[0] = Resources.Load<Sprite>("Images/Food/1010");
    //    card1Tiers[1] = Resources.Load<Sprite>("Images/Water/1020");
    //    card1Tiers[2] = Resources.Load<Sprite>("Images/Wood/2010");
    //    card1Tiers[3] = Resources.Load<Sprite>("Images/Stone/2020");

    //    card2Tiers[0] = Resources.Load<Sprite>("Images/Food/1011");
    //    card2Tiers[1] = Resources.Load<Sprite>("Images/Water/1021");
    //    card2Tiers[2] = Resources.Load<Sprite>("Images/Wood/2011");
    //    card2Tiers[3] = Resources.Load<Sprite>("Images/Stone/2021");

    //    card3Tiers[0] = Resources.Load<Sprite>("Images/Food/1012");
    //    card3Tiers[1] = Resources.Load<Sprite>("Images/Water/1022");
    //    card3Tiers[2] = Resources.Load<Sprite>("Images/Wood/2012");
    //    card3Tiers[3] = Resources.Load<Sprite>("Images/Stone/2022");
    //}

    //private void OnEnable()
    //{
    //    for (int i = 0; i < cnt; i++)
    //    {
    //        canBuy[i] = true;
    //    }
    //}

    public void BuyCancel()
    {
        for (int i = 0; i < cnt; i++)
        {
            buyCnt[i] = 0;
            soldOut[i].SetActive(soldOutBool[i]);
        }
        foreach (var text in countText)
        {
            text.text = "0";
        }
        buypanel.SetActive(false);
        selectCardCount = 0;
        selectCardID = 0;
    }
    public void ExchangeSence()
    {
        for (int i = 0; i < cnt; i++)
        {
            soldOutBool[i] = soldOut[i].activeSelf;
            soldOut[i].SetActive(false);
        }
        buypanel.SetActive(true);
    }

    public void ShopUiCancel()
    {
        shopCanvas.SetActive(false);
    }

    public void IncreaseCount(int idx)
    {
        if (selectCardCount >= term)
            return;
        if (buyCnt[idx] < cardCount[idx].Length)
            selectCardCount++;
        buyCnt[idx] = Mathf.Clamp(int.Parse(countText[idx].text) + 1, 0, cardCount[idx].Length);
        countText[idx].text = buyCnt[idx].ToString();
        print(selectCardCount);
    }

    public void ShowCardCount()
    {
        for (int i = 0; i < cnt; i++)
        {
            cardCountText[i].text = cardCount[i].Length.ToString();
            //cardCountText[i].text = CardManager.Cards.FindAll((c) => c.cardType == (Card.CardType)i && c.level == 1).Count.ToString();
        }
    }

    public void SetRandomButtonImg()
    {
        for (int i = 0; i < cnt; i++)
        {
            soldOut[i].SetActive(false);
            buyButton[i].interactable = true;
            buyButton[i].onClick.RemoveAllListeners();
            float rand = Random.Range(0, 0.99f);
            if (rand < 0.6f)
            {
                OpenShopSetting(i, 1);
            }
            else if (rand < 0.93f)
            {
                OpenShopSetting(i, 2);
            }
            else if (rand < 0.97f)
            {
                OpenShopSetting(i, 3);
            }
            else
            {
                OpenShopSetting(i, 4);
            }
        }
    }
    public void OnButtonClicked()
    {
        if (selectCardCount < term)
            return;
        buyButton[buyIdx].interactable = false;
        //soldOut[buyIdx].SetActive(true);
        soldOutBool[buyIdx] = true;
        for (int z = 0;  z < cnt; z++)
        {
            for(int y = 0; y < buyCnt[z]; y++)
            {
                CardManager.DestroyCard(cardCount[z][^(y+1)]);
            }
        }
        CardManager.CreateCard(selectCardID);

        BuyCancel();

    }

    /// <summary>
    /// 상점 카드 셋팅
    /// </summary>
    /// <param name="idx">구매 상품 인덱스</param>
    /// <param name="shopLevel">구매 상품 레벨</param>
    public void OpenShopSetting(int idx, int shopLevel)
    {

        buyButton[idx].image.sprite = Resources.Load<Sprite>($"Images/{cardType[idx]}/{cardID[idx] + shopLevel}");
        //if (canBuy[idx] == false)
        //    return;
        buyButton[idx].onClick.AddListener(() =>
        {
            buyIdx = idx;
            selectCardID = cardID[idx] + shopLevel;
            for (int x = 0; x < cnt; x++)
            {
                buttons[x].image.sprite = Resources.Load<Sprite>($"Images/{cardType[x]}/{cardID[x] + shopLevel - 1}"); ;
                CardManager.TryGetCardsByID(cardID[x] + shopLevel - 1, out Card[] cards);
                cardCount[x] = cards;
                cardCountText[x].text = cardCount[x].Length.ToString();
            }
        });
    }
}