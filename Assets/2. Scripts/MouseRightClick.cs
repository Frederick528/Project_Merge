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

    Stat stat;

    private void Start()
    {
        stat = StatManager.instance.playerCtrl.stat;
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
                    stat.curHunger = ((stat.curHunger + cardData.Hunger) > stat.maxHunger) ? stat.maxHunger : stat.curHunger + cardData.Hunger;
                    stat.curThirst = ((stat.curThirst + cardData.Thirst) > stat.maxThirst) ? stat.maxThirst : stat.curThirst + cardData.Thirst;
                    CardManager.DestroyCard(cardContents);

                    CanvasClose();
                });
            }
            else
            {
                cardEatBtn.interactable= false;
            }

            cardDecompositionBtn.onClick.RemoveAllListeners();
            cardDecompositionBtn.onClick.AddListener(() =>
            {
                print(cardContents.ID);
                cardContents.OnDecomposition(out Card[] cards);
                CanvasClose();
            });


            GameManager.CardCanvasOn = true;
            canvas.SetActive(true);
        }
    }
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
}
    //void MoveObjectToTargetPosition(GameObject objToMove)
    //{
    //    objToMove.transform.position = targetPosition;
        
    //}



