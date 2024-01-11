using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseRightClick : MonoBehaviour
{
    // 설정창매니저 역할(스크립트 이름 나중에 바꿔야할 듯)
    //public Vector3 targetPosition;
    public static bool onRightClick;
    public GameObject canvas;
    public Image cardImage;
    public Text cardName;
    public Text cardText;
    public Button cardEatBtn;
    [SerializeField]
    Button cardDecompositionBtn;

    [SerializeField]
    Transform effectUICanvas;

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
        if (hit.collider.gameObject.CompareTag("Card") && !GameManager.CardCanvasOn)
        {
            Card cardContents = hit.collider.GetComponent<Card>();
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
                cardEatBtn.interactable = true;
                cardEatBtn.onClick.RemoveAllListeners();
                cardEatBtn.onClick.AddListener(() =>
                {
                    if (cardContents.transform.parent.TryGetComponent(out CardGroup cardGroup))
                        CardManager.DestroyCard(cardGroup.RemoveCard(cardContents));
                    else
                        CardManager.DestroyCard(cardContents);
                    //Destroy(hit.collider.gameObject);
                    CanvasClose();
                    EffectManager.instance.eatCardImg = cardImage.sprite;
                    EffectManager.instance.cardContents = cardContents;
                    GameObject eatCard = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/EatCardEffect"), effectUICanvas);
                });
            }
            else
            {
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



