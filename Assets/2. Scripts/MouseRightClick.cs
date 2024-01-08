using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseRightClick : MonoBehaviour
{
    // 설정창매니저 역할(스크립트 이름 나중에 바꿔야할 듯)
    //public Vector3 targetPosition;
    public GameObject canvas;
    public Image cardImage;
    public Text cardName;
    public Text cardText;
    public Button cardEatBtn;

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
                if (hit.collider.gameObject.CompareTag("Card"))
                {
                    Card cardContents = hit.collider.GetComponent<Card>();
                    string cardID = $"{cardContents.cardType}_{cardContents.level}";

                    Texture2D cardTexture = Resources.Load<Texture2D>($"Images/{cardID}");
                    if (cardTexture != null)    // 나중에 if문은 빼도 될 것 같음.
                    {
                        cardImage.sprite = Sprite.Create(cardTexture, new Rect(0, 0, cardTexture.width, cardTexture.height), new Vector2(0.5f, 0.5f));
                    }
                    cardName.text = CardDIct.cardNameDict[cardID];
                    cardText.text = CardDIct.cardTextDict[cardID];

                    if (cardContents.cardType == Card.CardType.Food || cardContents.cardType == Card.CardType.Water)
                    {
                        cardEatBtn.interactable = true;
                        cardEatBtn.onClick.RemoveAllListeners();
                        cardEatBtn.onClick.AddListener(() =>
                        {
                            stat.curHunger = ((stat.curHunger + 20) > stat.maxHunger) ? stat.maxHunger : stat.curHunger + 20;   //여기 값 불러오기(20)
                            stat.curThirst = ((stat.curThirst + 10) > stat.maxThirst) ? stat.maxThirst : stat.curThirst + 10;   //여기 값 불러오기(10)
                            Destroy(hit.collider);
                            canvas.SetActive(false);
                        });
                    }
                    else
                    {
                        cardEatBtn.interactable= false;
                    }

                    GameManager.instance.cardCanvasOn = true;
                    canvas.SetActive(true);
                }
            } 
        }
    }

    public void CanvasClose()
    {
        GameManager.instance.cardCanvasOn = false;
        canvas.SetActive(false);
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



