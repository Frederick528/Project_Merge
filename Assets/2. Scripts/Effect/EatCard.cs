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

    private void Start()
    {
        StartCoroutine(EatCardEffect());
    }
    IEnumerator EatCardEffect()
    {
        eatCardImg.sprite = EffectManager.instance.eatCardImg;
        if (EffectManager.instance.cardContents.cardType == Card.CardType.Food)
            eatRenderer.color = new Color(255,255,255);
        else if (EffectManager.instance.cardContents.cardType == Card.CardType.Water)
            eatRenderer.color = new Color(255, 255, 255);
        else if (EffectManager.instance.cardContents.ID == 3000)
            eatRenderer.color = new Color(255, 255, 255);
        else if (EffectManager.instance.cardContents.ID == 3001)
            eatRenderer.color = new Color(255, 255, 255);
        eatCard.transform.localPosition = new Vector3(0, -390, 0);
        yield return new WaitForSeconds(0.5f);
        eatAnim.Play("EatCardAnimation", -1, 0);
        eatCard.transform.localPosition = new Vector3(0, -490, 0);
        yield return new WaitForSeconds(0.5f);
        eatAnim.Play("EatCardAnimation", -1, 0);
        eatCard.transform.localPosition = new Vector3(0, -590, 0);
        yield return new WaitForSeconds(0.5f);
        eatAnim.Play("EatCardAnimation", -1, 0);
        eatCard.transform.localPosition = new Vector3(0, -690, 0);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
