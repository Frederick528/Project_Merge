using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatCard : MonoBehaviour
{
    [Header("¸Ô±â")]
    public Animator eatAnim;
    public RectTransform eatCard;

    private void Start()
    {
        StartCoroutine(EatCardEffect());
    }
    IEnumerator EatCardEffect()
    {
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
