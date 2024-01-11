using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    public RectTransform effectCanvas;
    public RectTransform effectUICanvas;
    Vector2 particleCanvasSize;
    [Header("클릭")]
    public Animator clickAnim;
    public GameObject clickEffect;

    //[Header("먹기")]
    //public Animator eatAnim;
    //public RectTransform eatCard;

    private void Awake()
    {
        instance ??= this;
    }

    // Start is called before the first frame update
    void Start()
    {
        particleCanvasSize = effectCanvas.sizeDelta / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickEffect();
        }
    }

    void ClickEffect()
    {
        clickEffect.transform.localPosition = new Vector3(Input.mousePosition.x - particleCanvasSize.x, Input.mousePosition.y - particleCanvasSize.y, 0);
        clickAnim.Play("ClickAnimation", -1, 0);
    }
    //public IEnumerator EatCardEffect()
    //{
    //    Animator eatAnim = GetComponent<Animator>();
    //    RectTransform eatCard = GetComponentsInChildren<RectTransform>()[1];
    //    eatCard.transform.localPosition = new Vector3(0, -390, 0);
    //    yield return new WaitForSeconds(0.5f);
    //    eatAnim.Play("EatCardAnimation", -1, 0);
    //    eatCard.transform.localPosition = new Vector3(0, -490, 0);
    //    yield return new WaitForSeconds(0.5f);
    //    eatAnim.Play("EatCardAnimation", -1, 0);
    //    eatCard.transform.localPosition = new Vector3(0, -590, 0);
    //    yield return new WaitForSeconds(0.5f);
    //    eatAnim.Play("EatCardAnimation", -1, 0);
    //    eatCard.transform.localPosition = new Vector3(0, -690, 0);
    //    yield return new WaitForSeconds(0.5f);
    //    Destroy(this.gameObject);
    //}
}
