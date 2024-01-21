using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_Incounter6 : MonoBehaviour
{
    public bool isWaitingForInput = true;
    public int bifurcation = 0;
    public Text myText, mytext2;
    public GameObject incounter,select1, select2, select3;
    public float fadeSpeed = 0.5f; // 투명도가 줄어드는 속도

    [SerializeField]
    Button nextBtn;
    [SerializeField]
    GameObject blockUI, closeBtn;

    int currentTextIndex, currentTextIndex1, currentTextIndex4, currentTextIndex5, currentTextIndex6 = 0;
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


    private string[] result1_1 = { "",
                                 "나는 빠르게 숲을 떠나갔다.",
                                 "더 이상은 날 막을 수 없을 것이다.",
                                 "천천히 숲에서 멀리까지 나아가기 시작했다.",
                                 "하지만…"
    };

    private string[] result1_2 = { "",
                                 "무언가 기억이 돌아온다..",
                                 "여태 힘들었던 기억",
                                 "포기하고 싶었던 기억이 날 괴롭혔다.",
                                 "기억났다.",
                                 "나는 이곳에서 기억을 잊고 치료받기 위해서 나왔다.",
                                 "..그래서 마녀가 날 죽이지 않은건가..",
                                 "나는..",
                                 "숲에서 벗어났지만…",
                                 "더 이상 돌아갈 수 없었다.",
                                 "내 뒤에 있던 숲은 더 이상 보이지 않았고..",
                                 "그 이후의 미래는 뻔했다.",
                                 "-BAD ENDING-"
    };

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
        Turn.Instance.nextBtn.interactable = false;
        Turn.Instance.closeBtn.SetActive(true);        
    }

    void Update()
    {
        if (currentTextIndex < textArray1.Length)
        {
            Incounter1();
        }
        else if (currentTextIndex1 < textArray2.Length)
        {
            Select1();
        }
        else if (currentTextIndex4 < result1_1.Length && bifurcation == 0)
        {
            Result1_1();
        }
        else if (currentTextIndex6 < result1_2.Length && bifurcation == 0)
        {
            Result1_2();
        }
        else if (currentTextIndex5 < result2.Length && bifurcation == 1)
        {
            Result2();
        }
        else
        {
            incounter.SetActive(false);
            Turn.Instance.nextBtn.interactable = true;
            Turn.Instance.blockUI.SetActive(false);
            Turn.Instance.closeBtn.SetActive(false);
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
        else if (currentTextIndex4 < result1_1.Length && isWaitingForInput && bifurcation == 0)
        {
            myText.text = result1_1[currentTextIndex4];
        }
        else if (currentTextIndex6 < result1_2.Length && isWaitingForInput && bifurcation == 0)
        {
            myText.text = result1_2[currentTextIndex6];
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

    
    public void Result1_1()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 0)
        {
            currentTextIndex4++;
            if (currentTextIndex4 < result1_1.Length)
            {
                UpdateText(result1_1);
            }
            if (currentTextIndex4 >= result1_1.Length)
            {
                isWaitingForInput = false;
                select3.SetActive(true);
            }

        }
    }
    public void Result1_2()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 0)
        {
            currentTextIndex6++;
            if (currentTextIndex6 < result1_2.Length)
            {
                UpdateText(result1_2);
            }
            if (currentTextIndex6 >= result1_2.Length)
            {
                isWaitingForInput = false;
                Turn.Instance.closeBtn.SetActive(false);
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
                Turn.Instance.closeBtn.SetActive(false);
            }
        }
    }
}