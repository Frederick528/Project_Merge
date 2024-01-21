using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_Incounter4 : MonoBehaviour
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

    private string[] textArray1 = { "가만히 작업하고 있던 날. 하늘에서 어떤 편지가 내려왔다.",
                                   "마녀. 라고 적혀있다. 아무것도 안적혀있는 종이에 마법으로 글씨가 보이기 시작했다."};
    private string[] textArray2 = {"","문제를 하나 낼게 안맞출 수는 없고, 맞추면 상이 있고 틀리면 벌이있을꺼야! 즐겨줘!",
                                   "덜어내면 덜어낼 수록 점점 커지는 것은?"};
    private string[] result1 = { "", "나는 편지를 보고 \"구멍\" 이라고 대답했다.", "\"정답이야, 이걸 맞추다니. 재미없네.\"", "\"아무튼 약속한 상이야.\"","배고픔 10 증가", 
                                 "몸이 든든해지는게 느껴진다.", "저주였으면 어떤 효과였을지 조금 무서워진다…." };
    private string[] result2 = { "", "나는 편지를 보고 \"사랑\" 이라고 대답했다.", "\"사랑? 사랑이라.. 그것도 나쁘지 않네… 답은 아니지만!\"", 
                                 "\"하지만 그래도 대답은 마음에 들었으니, 약간의 저주만 줄게!\"","배고픔 10 감소","몸이 약해지는 것 같다..","마음에 들었으면 한번만 봐주지.." };

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
            Turn.Instance.nextBtn.interactable = true;
            Turn.Instance.blockUI.SetActive(false);
            Turn.Instance.closeBtn.SetActive(false);
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
                CoreController.HungerStatChange(10);
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
                CoreController.HungerStatChange(-10);
            }
        }
    }
}
