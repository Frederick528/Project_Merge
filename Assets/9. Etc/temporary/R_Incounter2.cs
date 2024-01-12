using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_Incounter2 : MonoBehaviour
{
    public bool isWaitingForInput = true;
    public int bifurcation = 0;
    public Text myText;
    public GameObject incounter, select1, select2, select3;

    [SerializeField]
    Button nextBtn;
    [SerializeField]
    GameObject blockUI, closeBtn;

    int currentTextIndex, currentTextIndex2, currentTextIndex3, currentTextIndex4, currentTextIndex5 = 0;
    private string[] textArray1 = { "오늘 하루도 끝나간다.",
                                   "그렇게 바닥에 앉아 쉬고 있던 나에게 옆에 다람쥐가 다가왔다.",
                                   "그 다람쥐는 일반적인 다람쥐랑은 좀 다르게 생겼지만.. 다람쥐다..",};


    private string[] result1 = { "", "나는 지금 고기가 땡긴다. ", "그러니 나는 이 다람쥐를 잡아야한다..!", "나는 빠르게 손을 뻗어 다람쥐를…", "꽈악" };
    private string[] result1_a = { "", "다람쥐는 엄청난 속도로 다가오던 내 손을 물고 도망갔다.", "다람쥐에게 독이라도 있던건지, 몸에 힘이 빠진다.." };
    private string[] result2 = { "", "나는 조심히 움직여 다람쥐에게 인사했다.", "다람쥐는 내 반응이 무엇을 의미하는지 아는 것 같다.!", "다람쥐는 새 손으로 올라와 들고있던 열매를 내게 주었다.",
                                "나는 그걸 조심히 입에 넣었다." };
    private string[] result2_a = { "", "몸에 힘이 도는 것 같다!" };

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
        else if (currentTextIndex >= textArray1.Length && currentTextIndex2 < result1.Length && bifurcation == 0)
        {
            Result1();
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex3 < result2.Length && bifurcation == 1)
        {
            Result2();
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex2 >= result1.Length && bifurcation == 0 && currentTextIndex4 < result1_a.Length)
        {
            Select2();
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex3 >= result2.Length && bifurcation == 1 && currentTextIndex5 < result2_a.Length)
        {
            Select3();
        }
        else
        {
            incounter.SetActive(false);
            nextBtn.interactable = false;
            blockUI.SetActive(false);
            closeBtn.SetActive(false);
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
                select2.SetActive(true);
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
                select3.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }

    public void Select2()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 0)
        {
            currentTextIndex4++;
            if (currentTextIndex4 < result1_a.Length)
            {
                UpdateText(result1_a, currentTextIndex4);
            }
            if (currentTextIndex4 >= result1_a.Length)
            {
                isWaitingForInput = false;
            }
        }
    }

    public void Select3()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput== true && bifurcation == 1)
        {
            currentTextIndex5++;
            if (currentTextIndex5 < result2_a.Length)
            {
                UpdateText(result2_a, currentTextIndex5);
            }
            if (currentTextIndex5 >= result2_a.Length)
            {
                isWaitingForInput = false;
            }
        }
    }
}
