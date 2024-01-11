using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EatCard : MonoBehaviour
{
    [Header("¸Ô±â")]
    [SerializeField]
    Animator eatAnim;
    [SerializeField]
    RectTransform eatCard;
    [SerializeField]
    Image eatCardImg;
    [SerializeField]
    SpriteRenderer eatRenderer;

    Stat stat;
    CardData cardData;

    private void Start()
    {
        stat = StatManager.instance.playerCtrl.stat;
        cardData = EffectManager.instance.cardContents.Data;
        StartCoroutine(EatCardEffect());
    }
    IEnumerator EatCardEffect()
    {
        eatCard.transform.localPosition = new Vector3(0, -350, 0);
        eatCardImg.sprite = EffectManager.instance.eatCardImg;
        if (EffectManager.instance.cardContents.cardType == Card.CardType.Food)
            eatRenderer.color = new Color(171 / 255f,111 / 255f,64 / 255f);
        else if (EffectManager.instance.cardContents.cardType == Card.CardType.Water)
            eatRenderer.color = new Color(0 / 255f, 155 / 255f, 255 / 255f);
        else if (EffectManager.instance.cardContents.ID == 3000)
            eatRenderer.color = new Color(171 / 255f, 111 / 255f, 64 / 255f);
        else if (EffectManager.instance.cardContents.ID == 3001)
            eatRenderer.color = new Color(0 / 255f, 155 / 255f, 255 / 255f);

        for (int i = 0; i < 60; i++)
        {
            eatCard.transform.localPosition = Vector3.MoveTowards(eatCard.transform.localPosition, new Vector3(0, -390, 0), 1f);
            yield return new WaitForSeconds(0.01f);
        }
        eatAnim.Play("EatCardAnimation", -1, 0);
        yield return StartCoroutine(Eat(-390));
        //eatCard.transform.localPosition = new Vector3(0, -490, 0);
        //yield return new WaitForSeconds(0.5f);
        eatAnim.Play("EatCardAnimation", -1, 0);
        yield return StartCoroutine(Eat(-490));
        //eatCard.transform.localPosition = new Vector3(0, -590, 0);
        //yield return new WaitForSeconds(0.5f);
        eatAnim.Play("EatCardAnimation", -1, 0);
        yield return StartCoroutine(Eat(-590));
        //eatCard.transform.localPosition = new Vector3(0, -690, 0);
        //yield return new WaitForSeconds(0.5f);
        stat.curHunger = ((stat.curHunger + cardData.Hunger) > stat.maxHunger) ? stat.maxHunger : stat.curHunger + cardData.Hunger;
        stat.curThirst = ((stat.curThirst + cardData.Thirst) > stat.maxThirst) ? stat.maxThirst : stat.curThirst + cardData.Thirst;
        Destroy(this.gameObject);
    }

    IEnumerator Eat(float yPos)
    {
        eatCard.transform.localPosition = new Vector3(0, yPos, 0);
        for (int i = 0; i < 60; i++)
        {
            if (i < 15)
            {
                eatCard.transform.localPosition = Vector3.MoveTowards(eatCard.transform.localPosition, new Vector3(0, yPos + 100, 0), 2f);
                yield return new WaitForSeconds(0.01f);
            }
            else if (i < 30)
            {
                eatCard.transform.localPosition = Vector3.MoveTowards(eatCard.transform.localPosition, new Vector3(0, yPos - 100, 0), 2f);
                yield return new WaitForSeconds(0.01f);
            }
            else if (i < 45)
            {
                eatCard.transform.localPosition = Vector3.MoveTowards(eatCard.transform.localPosition, new Vector3(0, yPos + 100, 0), 2f);
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                eatCard.transform.localPosition = Vector3.MoveTowards(eatCard.transform.localPosition, new Vector3(0, yPos - 100, 0), 10f);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
