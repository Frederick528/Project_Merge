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

    [Header("먹기")]
    [HideInInspector]
    public Sprite eatCardImg;
    [HideInInspector]
    public Card cardContents;
    [HideInInspector]
    public float addHungerValue;
    [HideInInspector]
    public float addThirstValue;
    public GameObject eatEffect;

    [Header("합성")]
    public Animator mergeAnim;
    public GameObject mergeEffect;

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

    public void MergeEffect()
    {
        mergeEffect.SetActive(true);
        mergeAnim.Play("MergeAnimation", -1, 0);
    }

}
