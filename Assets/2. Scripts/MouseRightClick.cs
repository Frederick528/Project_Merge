using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseRightClick : MonoBehaviour
{
    // 설정창매니저 역할(스크립트 이름 나중에 바꿔야할 듯)
    //public Vector3 targetPosition;
    public static bool onRightClick;
    public static MouseRightClick Instance;
    public static Card CurrentRef;
    public GameObject canvas;
    public Image cardImage;
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI cardText;
    public Button cardEatBtn;
    [SerializeField]
    public Button cardDecompositionBtn;

    [SerializeField]
    public Transform effectUICanvas;

    [SerializeField]
    GameObject cardEatInfo;
    [SerializeField]
    TextMeshProUGUI eatInfoText;
    [SerializeField]
    ShopController shopController;

    private void Awake()
    {
        Instance ??= this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                RayCastEvt(hit);
            } 
        }
    }

    public void CanvasClose(bool boolean)
    {
        canvas.SetActive(false);
        CoreController.ModifyFluctuation(boolean);
        GameManager.CardCanvasOn = false;
        TutorialManager.WaitButtonCallBack = true;
    }
    
    private void RayCastEvt(RaycastHit hit)
    {
        if (GameManager.CardCanvasOn) return;
        if(hit.collider.TryGetComponent(out Card cardContents))
        {
            if (GameManager.Instance.isTutorial)
            {
                ShowCardInfo(cardContents.ID);
            }
            else
            {
                ShowCardInfo(cardContents);
            }

            CurrentRef = cardContents;
        }
    }

    public void ShowCardInfo(Card cardContents)
    {
        if (cardContents.ID == 5000)
        {
            shopController.shopCanvas.SetActive(true);
            return;
        } 
        CardData cardData = cardContents.Data;
        Stat stat = StatManager.instance.playerCtrl.stat;

        //string cardID = $"{cardContents.cardType}_{cardContents.level}";
        if (Resources.Load<Sprite>($"Images/{cardContents.cardType}/{cardContents.ID}") != null)    // 나중에 if문은 빼야 함.
        {
            cardImage.sprite = Resources.Load<Sprite>($"Images/{cardContents.cardType}/{cardContents.ID}");
        }
        cardName.text = cardData.KR;
        cardText.text = cardData.Info;
        float tempStatHunger = stat.curHunger;
        float tempStatThirst = stat.curThirst;
        CoreController.ModifyFluctuation(cardData.Hunger, cardData.Thirst);

        if (cardContents.cardType == Card.CardType.Food || cardContents.cardType == Card.CardType.Water || cardContents.ID == 3000 || cardContents.ID == 3001)
        {
            eatInfoText.text = $"<#AB6F40>음식 섭취량:<b></color> <#000000>{cardData.Hunger}</color></b>\n<#009BFF>수분 섭취량:</color> <b><#000000>{cardData.Thirst}</color></b>";
            cardEatInfo.SetActive(true);
            cardEatBtn.interactable = true;
            cardEatBtn.onClick.RemoveAllListeners();
            cardEatBtn.onClick.AddListener(() =>
            {
                if (cardContents.transform.parent.TryGetComponent(out CardGroup cardGroup))
                    CardManager.DestroyCard(cardGroup.RemoveCard(cardContents));
                else
                    CardManager.DestroyCard(cardContents);
                //Destroy(hit.collider.gameObject);
                CanvasClose(false);
                EffectManager.instance.eatCardImg = cardImage.sprite;
                EffectManager.instance.cardContents = cardContents;
                EffectManager.instance.addHungerValue = (tempStatHunger + cardData.Hunger > stat.maxHunger) ? stat.maxHunger - tempStatHunger : cardData.Hunger;
                EffectManager.instance.addThirstValue = (tempStatThirst + cardData.Thirst > stat.maxThirst) ? stat.maxThirst - tempStatThirst : cardData.Thirst;
                GameObject eatCard = Instantiate(EffectManager.instance.eatEffect, effectUICanvas);
            });
        }
        else
        {
            cardEatInfo.SetActive(false);
            cardEatBtn.interactable= false;
        }

        if (cardContents.level == 0)
        {
            cardDecompositionBtn.interactable = false;
        }
        else
        {
            cardDecompositionBtn.interactable = true;
            cardDecompositionBtn.onClick.RemoveAllListeners();
            cardDecompositionBtn.onClick.AddListener(() =>
            {
                cardContents.OnDecomposition(out Card[] cards);
                CardManager.DestroyCard(cardContents);
                //if (cardContents.transform.parent.TryGetComponent(out CardGroup cardGroup))
                //    CardManager.DestroyCard(cardGroup.RemoveCard(cardContents));
                //else
                //    CardManager.DestroyCard(cardContents);
                CanvasClose(true);
            });
        }


        GameManager.CardCanvasOn = true;
        canvas.SetActive(true);
    }
    
    public void ShowCardInfo(int ID, bool onField = false)
    {
        if (ID == 5000 && onField == true)
        {
            shopController.shopCanvas.SetActive(true);
            return;
        }
        if (!CardDataDeserializer.TryGetData(ID, out CardData cardData))
            return;
        var cardType = ID < 1020 ? Card.CardType.Food :
            ID < 2010 ? Card.CardType.Water :
            ID < 2020 ? Card.CardType.Wood :
            ID < 3000 ? Card.CardType.Stone : Card.CardType.Combination;

        //string cardID = $"{cardContents.cardType}_{cardContents.level}";
        if (Resources.Load<Sprite>($"Images/{cardType}/{ID}") != null)    // 나중에 if문은 빼야 함.
        {
            cardImage.sprite = Resources.Load<Sprite>($"Images/{cardType}/{ID}");
        }
        cardName.text = cardData.KR;
        cardText.text = cardData.Info;
        
        cardEatInfo.SetActive(false);

        if (!GameManager.Instance.isTutorial)
        {
            cardEatBtn.interactable = false;
            cardDecompositionBtn.interactable = false;
        }
        
        //GameManager.CardCanvasOn = true;
        canvas.SetActive(true);
    }
    
    //IEnumerator EatCard()
    //{

    //}

    //if (input.getmousebuttondown(1))
    //{

    //    ray ray = camera.main.screenpointtoray(input.mouseposition);
    //    raycasthit hit;


    //    if (physics.raycast(ray, out hit) && hit.collider.comparetag("card"))
    //    {
    //        debug.log("true object");
    //        hit.rigidbody.iskinematic = true;
    //        moveobjecttotargetposition(hit.transform.gameobject);
    //    }
    //}
    //void MoveObjectToTargetPosition(GameObject objToMove)
    //{
    //    objToMove.transform.position = targetPosition;
        
    //}
}