using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_Incounter6 : MonoBehaviour
{
    public bool isWaitingForInput = true;
    public int bifurcation = 0;
    public Text myText, mytext2;
    public GameObject incounter,select1, select2;
    public float fadeSpeed = 0.5f; // 투명도가 줄어드는 속도

    [SerializeField]
    Button nextBtn;
    [SerializeField]
    GameObject blockUI, closeBtn;

    int currentTextIndex, currentTextIndex1, currentTextIndex4, currentTextIndex5 = 0;
    private string[] textArray1 = { "이곳에서 산지 25일차.",
                                    "역시나 내가 왜 여기있는지 잘 모르겠다.",
                                    "마녀는 무엇이고",
                                    "나는 누구이며",
                                    "지금의 나는 무엇을 위해 여기있는가." };

    private string[] textArray2 = {"",
                                    "마녀는 30일을 버티면 이곳에서 다른 곳으로 보내주겠다고 했었다..",
                                    "나가게 해준다고 한적은 없다.",
                                    "마녀는 확실히 무언가를 숨기고 있었다.",
                                    "마녀가 오기전에.." };


    private string[] result1 = { "", "배드엔딩 - 시트 작성 예정" };
    private string[] result2 = { "", "아니다. 조금만 더 생각해보아야할 사항이다.",
                                     "분명 마녀는 나를 몇번 죽일뻔했지만.. 직접적으로 그러진 않았다.",
                                     "마치 내 상태를 보고 저주를 거는.. 그런 느낌이었다.",
                                     "지금 당장에 나에게 해를 끼칠 수는 없을 것이다.",
                                     "그리고 사실.. 나는 이곳이 마음에 드는걸지도 모르겠다.",
                                     "귀를 스치는 바람에 흔들리는 잔디소리와",
                                     "무언가 마음이 따뜻해지는 이 분위기..",
                                     "조금만 더 남는게 좋을 것 같다.",
                                     "[저벅저벅]",
                                     "내 뒤에서 걷는 소리가 들려온다.",
                                     "예상했던대로 마녀였고",
                                     "나는 전과는 다르게 경계하지않고 마녀를 대해주었다."};


    void Start()
    {
        myText.text = textArray1[0];
        nextBtn.interactable = false;
        //closeBtn.SetActive(true);        
    }

    void Update()
    {
        if (currentTextIndex < textArray1.Length)
        {
            Incounter1();
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex1 < textArray2.Length)
        {
            Select1();
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex1 >= textArray2.Length 
                 && currentTextIndex4 < result1.Length && bifurcation == 0)
        {
            Result1();
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex1 >= textArray2.Length 
                && currentTextIndex5 < result2.Length && bifurcation == 1)
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

    void UpdateText(string[] textArray)
    {
        // 배열 길이 확인 후 업데이트
        if (currentTextIndex < textArray1.Length && isWaitingForInput)
        {
            myText.text = textArray1[currentTextIndex];
        }
        else if (currentTextIndex1 < textArray2.Length && isWaitingForInput)
        {
            myText.text = textArray2[currentTextIndex1];
        }
        else if (currentTextIndex4 < result1.Length && isWaitingForInput && bifurcation == 0)
        {
            myText.text = result1[currentTextIndex4];
        }
        else if (currentTextIndex5 < result2.Length && isWaitingForInput && bifurcation == 1)
        {
            myText.text = result2[currentTextIndex5];
        }
        else
        {
            Debug.Log("Not Text");
        }
    }

  

    public void Incounter1()
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

    public void Select1()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true)
        {
            currentTextIndex1++;
            if (currentTextIndex1 < textArray2.Length)
            {
                UpdateText(textArray2);
            }
            if (currentTextIndex1 >= textArray2.Length)
            {
                select2.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }

    
    public void Result1()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 0)
        {
            currentTextIndex4++;
            if (currentTextIndex4 < result1.Length)
            {
                UpdateText(result1);
            }
            if (currentTextIndex4 >= result1.Length)
            {
                isWaitingForInput = false;
            }

        }
    }

    public void Result2()
    {
        bifurcation = 1;

        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 1)
        {
            currentTextIndex5++;
            if (currentTextIndex5 < result2.Length)
            {
                UpdateText(result2);
            }
            if (currentTextIndex5 >= result2.Length)
            {
                isWaitingForInput = false;
            }
        }
    }
}