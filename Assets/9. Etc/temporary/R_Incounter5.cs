using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_Incounter5 : MonoBehaviour
{
    public bool isWaitingForInput = true;
    public int bifurcation = 0;
    public Text myText;
    public GameObject incounter, select1, select2;


    [SerializeField]
    Button nextBtn;
    [SerializeField]
    GameObject blockUI, closeBtn;

    int currentTextIndex, currentTextIndex2, currentTextIndex3, currentTextIndex5 = 0;

    private string[] textArray1 = { "숲을 돌아다니다가 독특한 모양의 과일 나무를 발견했다."};
    private string[] textArray2 = {"","나무에는 많은 과일이 달려 있었다.",
                                   "하지만 어떤 과일을 먹어야 할까…", "안전해보이는 빨간색 열매와 맛있어 보이는 자홍색 열매 중 어떤 걸 골라야하지?"};
    private string[] result1 = { "", "나는 빨간색 열매를 먹었다.", "열매를 먹은 직후 열매가 마녀의 마법으로 바뀐것을 직감했다.",
                                 "아무래도 마녀가 심어둔 함정인거 같다. ", "몸에 힘이 빠지는 기분이 든다." };
    private string[] result2 = { "", "나는 자홍색 열매를 먹었다.", "열매를 먹은 직후 주위의 식물들의 색이 변하는거 같다.",
                                 "정신을 차린 후 더 이상 배고픔이 느껴지지 않았다." };

    void Start()
    {
        myText.text = textArray1[0];
        nextBtn.interactable = false;
        closeBtn.SetActive(true);    
    }

    void Update()
    {
        if (currentTextIndex < textArray1.Length)
        {
            Incounter1();
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex5 < textArray2.Length)
        {
            Incounter2();
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex5 >= textArray2.Length && currentTextIndex2 < result1.Length && bifurcation == 0)
        {
            Result1();
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex5 >= textArray2.Length && currentTextIndex3 < result2.Length && bifurcation == 1)
        {
            Result2();
        }
        else
        {
            incounter.SetActive(false);
            nextBtn.interactable = true;
            blockUI.SetActive(false);
            //closeBtn.SetActive(false);
            GameManager.CardCanvasOn = false;
        }
    }

    void UpdateText(string[] textArray, int currentIndex)
    {
        // 배열 길이 확인 후 업데이트
        if (currentIndex < textArray.Length)
        {
            myText.text = textArray[currentIndex];
        }
        else
        {
            Debug.Log("Not Text");
        }
    }

    void Incounter1()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput)
        {
            currentTextIndex++;
            if (currentTextIndex < textArray1.Length)
            {
                UpdateText(textArray1, currentTextIndex);
            }
            if (currentTextIndex >= textArray1.Length)
            {
                select1.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }
    public void Incounter2()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput)
        {
            currentTextIndex5++;
            if (currentTextIndex5 < textArray2.Length)
            {
                UpdateText(textArray2, currentTextIndex5);
            }
            if (currentTextIndex5 >= textArray2.Length)
            {
                select2.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }

    public void Result1()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput && bifurcation == 0)
        {
            currentTextIndex2++;
            if (currentTextIndex2 < result1.Length)
            {
                UpdateText(result1, currentTextIndex2);
            }
            if (currentTextIndex2 >= result1.Length)
            {
                isWaitingForInput = false;
            }
        }
    }

    public void Result2()
    {
        bifurcation = 1;

        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput && bifurcation == 1)
        {
            currentTextIndex3++;
            if (currentTextIndex3 < result2.Length)
            {
                UpdateText(result2, currentTextIndex3);
            }
            if (currentTextIndex3 >= result2.Length)
            {
                isWaitingForInput = false;
            }
        }
    }
}
