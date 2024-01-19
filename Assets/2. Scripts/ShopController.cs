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

public class ShopController : MonoBehaviour
{
    private const int cnt = 4;
    public GameObject shopCanvas;
    public GameObject exchangeCanvasUI;
    public GameObject buypanel;

    public Text[] countText = new Text[cnt];
    public Text[] cardCountText = new Text[cnt];

    public Card[][] cardCount = new Card[cnt][];
    public int[] buyCnt = new int[cnt];

    public Button[] buyButton = new Button[cnt];
    public Button[] buttons = new Button[cnt];
    public Sprite[] card1Tiers = new Sprite[cnt];
    public Sprite[] card2Tiers = new Sprite[cnt];
    public Sprite[] card3Tiers = new Sprite[cnt];

    public int[] cardID = new int[] {1010, 1020, 2010, 2020};
    public string[] cardType = new string[] {"Food", "Water", "Wood", "Stone"};

    public int selectCardCount;


    private void Start()
    {
        card1Tiers[0] = Resources.Load<Sprite>("Images/Food/1010");
        card1Tiers[1] = Resources.Load<Sprite>("Images/Water/1020");
        card1Tiers[2] = Resources.Load<Sprite>("Images/Wood/2010");
        card1Tiers[3] = Resources.Load<Sprite>("Images/Stone/2020");

        card2Tiers[0] = Resources.Load<Sprite>("Images/Food/1011");
        card2Tiers[1] = Resources.Load<Sprite>("Images/Water/1021");
        card2Tiers[2] = Resources.Load<Sprite>("Images/Wood/2011");
        card2Tiers[3] = Resources.Load<Sprite>("Images/Stone/2021");

        card3Tiers[0] = Resources.Load<Sprite>("Images/Food/1012");
        card3Tiers[1] = Resources.Load<Sprite>("Images/Water/1022");
        card3Tiers[2] = Resources.Load<Sprite>("Images/Wood/2012");
        card3Tiers[3] = Resources.Load<Sprite>("Images/Stone/2022");
    }

    private void OnEnable()
    {

    }

    public void BuyCancel()
    {
        for (int i = 0; i < cnt; i++)
        {
            buyCnt[i] = 0;
        }
        foreach (var text in countText)
        {
            text.text = "0";
        }
        buypanel.SetActive(!buypanel.activeSelf);
        selectCardCount = 0;
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
        if (selectCardCount >= 2)
            return;
        buyCnt[idx] = Mathf.Clamp(int.Parse(countText[idx].text) + 1, 0, cardCount[idx].Length);
        if (buyCnt[idx] < cardCount[idx].Length)
            selectCardCount++;
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
            buyButton[i].onClick.RemoveAllListeners();
            float rand = Random.Range(0, 0.99f);
            if(rand < 0.5f)
            {
                int shopLevel = 1;
                buyButton[i].image.sprite = Resources.Load<Sprite>($"Images/{cardType[i]}/{cardID[i] + shopLevel}");
                //버튼 클릭시 온클릭 이벤트 추가
                buyButton[i].onClick.AddListener(() =>
                {
                    for (int x = 0; x < cnt; x++)
                    {
                        print(cardID[x] + shopLevel - 1);
                        buttons[x].image.sprite = Resources.Load<Sprite>($"Images/{cardType[x]}/{cardID[x] + shopLevel - 1}"); ;
                        CardManager.TryGetCardsByID(cardID[x] + shopLevel - 1, out Card[] cards);
                        cardCount[x] = cards;
                        cardCountText[x].text = cardCount[x].Length.ToString();
                    }
                    //CardManager.TryGetCardsByID(1010, out Card[] foodCards);
                    //cardCount[0] = foodCards;
                    //CardManager.TryGetCardsByID(1020, out Card[] waterCards);
                    //cardCount[1] = waterCards;
                    //CardManager.TryGetCardsByID(2010, out Card[] woodCards);
                    //cardCount[2] = woodCards;
                    //CardManager.TryGetCardsByID(2020, out Card[] stoneCards);
                    //cardCount[3] = stoneCards;
                });
            }
            else
            {
                int shopLevel = 2;
                buyButton[i].image.sprite = Resources.Load<Sprite>($"Images/{cardType[i]}/{cardID[i] + shopLevel}");
                //buyButton[i].image.sprite = card3Tiers[i];
                buyButton[i].onClick.AddListener(() =>
                {
                    for (int x = 0; x < cnt; x++)
                    {
                        print(cardID[x] + shopLevel - 1);
                        buttons[x].image.sprite = Resources.Load<Sprite>($"Images/{cardType[x]}/{cardID[x] + shopLevel - 1}"); ;
                        CardManager.TryGetCardsByID(cardID[x] + shopLevel - 1, out Card[] cards);
                        cardCount[x] = cards;
                        cardCountText[x].text = cardCount[x].Length.ToString();
                    }
                    //CardManager.TryGetCardsByID(1011, out Card[] foodCards);
                    //cardCount[0] = foodCards;
                    //CardManager.TryGetCardsByID(1021, out Card[] waterCards);
                    //cardCount[1] = waterCards;
                    //CardManager.TryGetCardsByID(2011, out Card[] woodCards);
                    //cardCount[2] = woodCards;
                    //CardManager.TryGetCardsByID(2021, out Card[] stoneCards);
                    //cardCount[3] = stoneCards;
                });
            }
        }
    }
    public void OnButtonClicked(int objectId)
    {
        CardManager.DestroyCard(cardCount[objectId][0]);
        CardManager.DestroyCard(cardCount[objectId][1]);
        //CardManager.CreateCard();
        
    }

    private void DestroyObject()
    {
        throw new NotImplementedException();
    }
}