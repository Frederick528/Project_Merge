using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_Incounter6 : MonoBehaviour
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

    private string[] textArray1 = { "밤이 되고 숲의 어둠 속에서 이상한 그림자들이 지나가는듯한 느낌을 받았다.", 
                                    "그림자들은 나무 사이를 미끄러지듯 움직이는것 같다. ",
                                    "어딘가에서 나를 주시하고 있는 듯한 기분을 주며 자취를 감춘다."};
    private string[] textArray2 = {"","저 그림자를 따라가봐야 하나…"};
    private string[] result1 = { "", "그림자가 멈춘곳에 다가갔다.", 
                                     "그곳에 도착하자 빛나는 돌을 발견했다. ",
                                     "돌에 손을 대자 신비로운 에너지가 전달되었다."};
    private string[] result2 = { "", "역시 따라가는 것은 위험해보여…",
                                     "그냥 돌아가는게 좋겠어." };

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
