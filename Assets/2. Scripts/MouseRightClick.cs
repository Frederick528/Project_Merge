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
    public GameObject canvas;
    public Image cardImage;
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI cardText;
    public Button cardEatBtn;
    [SerializeField]
    Button cardDecompositionBtn;

    [SerializeField]
    Transform effectUICanvas;

    [SerializeField]
    GameObject cardEatInfo;
    [SerializeField]
    TextMeshProUGUI eatInfoText;

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

    public void CanvasClose()
    {
        GameManager.CardCanvasOn = false;
        canvas.SetActive(false);
    }
    
    private void RayCastEvt(RaycastHit hit)
    {
        if (GameManager.CardCanvasOn) return;
        if(hit.collider.TryGetComponent(out Card cardContents))
        {
            ShowCardInfo(cardContents);
        }
    }

    public void ShowCardInfo(Card cardContents)
    {
        CardData cardData = cardContents.Data;

        //string cardID = $"{cardContents.cardType}_{cardContents.level}";
        if (Resources.Load<Sprite>($"Images/{cardContents.cardType}/{cardContents.ID}") != null)    // 나중에 if문은 빼야 함.
        {
            cardImage.sprite = Resources.Load<Sprite>($"Images/{cardContents.cardType}/{cardContents.ID}");
        }
        cardName.text = cardData.KR;
        cardText.text = cardData.Info;

        if (cardContents.cardType == Card.CardType.Food || cardContents.cardType == Card.CardType.Water || cardContents.ID == 3000 || cardContents.ID == 3001)
        {
            eatInfoText.text = $"<#AB6F40>음식 섭취량:<b></color> <#000000>{cardData.Hunger}</color></b>\n<#009BFF>수분 섭취량:</color> <b><#000000>{cardData.Thirst}</color></b>";
            cardEatInfo.SetActive(true);
            cardEatBtn.interactable = true;
            cardEatBtn.onClick.RemoveAllListeners();
            cardEatBtn.onClick.AddListener(() =>
            {
                CoreController.ModifyHunger(cardContents.Data.Hunger);
                CoreController.ModifyHunger(cardContents.Data.Thirst);
                if (cardContents.transform.parent.TryGetComponent(out CardGroup cardGroup))
                    CardManager.DestroyCard(cardGroup.RemoveCard(cardContents));
                else
                    CardManager.DestroyCard(cardContents);
                //Destroy(hit.collider.gameObject);
                CanvasClose();
                EffectManager.instance.eatCardImg = cardImage.sprite;
                EffectManager.instance.cardContents = cardContents;
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
                CanvasClose();
            });
        }


        GameManager.CardCanvasOn = true;
        canvas.SetActive(true);
    }
    
    public void ShowCardInfo(int ID)
    {
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
        cardEatBtn.interactable= false;
        cardDecompositionBtn.interactable = false;
        
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



