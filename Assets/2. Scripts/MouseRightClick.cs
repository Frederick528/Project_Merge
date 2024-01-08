using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseRightClick : MonoBehaviour
{
    //public Vector3 targetPosition;
    public GameObject canvas;
    public Image cardImage;
    public Text cardName;
    public Text cardText;
    public bool cardCanvasOn = false;

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

    
    private void RayCastEvt(RaycastHit hit)
    {
        if (hit.collider.gameObject.CompareTag("Card"))
        {
            string card = $"{hit.collider.GetComponent<Card>().cardType}_{hit.collider.GetComponent<Card>().level}";

            Texture2D cardTexture = Resources.Load<Texture2D>($"Images/{card}");
            if (cardTexture != null)
            {
                cardImage.sprite = Sprite.Create(cardTexture, new Rect(0, 0, cardTexture.width, cardTexture.height), new Vector2(0.5f, 0.5f));
            }
            cardName.text = CardDIct.cardNameDict[card];
            cardText.text = CardDIct.cardTextDict[card];
            //cardCanvasOn = true;
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



