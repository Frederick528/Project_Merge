using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_Incounter3 : MonoBehaviour
{
    public bool isWaitingForInput = true;
    public int bifurcation = 0;
    public Text myText;
    public GameObject incounter, select1, select2;


    [SerializeField]
    Button nextBtn;
    [SerializeField]
    GameObject blockUI, closeBtn;

    int currentTextIndex, currentTextIndex2, currentTextIndex3, currentTextIndex4 = 0;

    private string[] textArray1 = { "자원을 구하기 위해 숲속을 탐험하던 중. 오래된 상자를 발견했다.",
                                   "이끼가 많이 덮여있고, 오랜 시간 동안 건들지 않은 상자인 것 같다."};
    private string[] result1 = { "", "나는 침을 삼키고 상자를 열었다.", "안에는 작은 반지가 하나 있었다.", "나는 그 반지를 조심히 들었고.., "};
    private string[] result2 = { "", "마법이 있는 세상에 아무 상자나 열면 안될 것 같다.", "함정일 수도 있으니.. 그냥 두고가자."};

    private string[] result1_1 = { "무언가 따뜻한 느낌이 내 몸을 멤돌았다." };


    void Start()
    {
        myText.text = textArray1[0];
        nextBtn.interactable = false;
      
    }

    void Update()
    {
        if (currentTextIndex < textArray1.Length)
        {
            Incounter1();
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex2 < result1.Length && bifurcation == 0)
        {
            Result1();
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex3 < result2.Length && bifurcation == 1)
        {
            Result2();
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex2 < result1.Length && bifurcation == 0 && currentTextIndex4 < result1_1.Length)
        {
            Select1();
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

    void UpdateText(string[] textArray)
    {
        // 배열 길이 확인 후 업데이트
        if (currentTextIndex < textArray1.Length && isWaitingForInput)
        {
            myText.text = textArray1[currentTextIndex];
        }
        
        else
        {
            Debug.Log("Not Text");
        }
    }

    void Incounter1()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true)
        {
            currentTextIndex++;
            if (currentTextIndex < textArray1.Length)
            {
                UpdateText(textArray1);
            }
            if (currentTextIndex >= textArray1.Length)
            {
                select1.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }

    public void Result1()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 0)
        {
            currentTextIndex2++;
            if (currentTextIndex2 < result1.Length)
            {
                UpdateText(result1);
            }
            if (currentTextIndex2 >= result1.Length)
            {
                select1.SetActive(true);
                isWaitingForInput = false;
            }

        }
    }

    public void Result2()
    {
        bifurcation = 1;

        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 1)
        {
            currentTextIndex3++;
            if (currentTextIndex3 < result2.Length)
            {
                UpdateText(result2);
            }
            if (currentTextIndex3 >= result2.Length)
            {
                isWaitingForInput = false;
            }
        }
    }

    public void Select1()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true)
        {
            currentTextIndex4++;
            if (currentTextIndex4 < result1_1.Length)
            {
                UpdateText(result1_1);
            }
            if (currentTextIndex4 >= result1_1.Length)
            {
                
                isWaitingForInput = false;
            }
        }
    }

}
