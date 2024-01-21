using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_Incounter18 : MonoBehaviour
{
    public bool isWaitingForInput = true;
    public int bifurcation = 0;
    public Text myText;
    public GameObject incounter, transparency, select1;

    [SerializeField]
    Button nextBtn;
    [SerializeField]
    GameObject blockUI, closeBtn;

    int currentTextIndex, currentTextIndex2, currentTextIndex3 = 0;
    private string[] textArray1 = {
        "마녀가 선물을 보내왔다.",
        "콩 하나였다.",
        "먹으라고 써있는데.. 콩 하나다."
    };

    private string[] result1 = {
        "",
        "미래를 위해서 키우기로 결정했다!",
        "잠시뒤 엄청 큰 뿌리가 나왔고..",
        "이 뿌리가 나를 덮쳤다!",
        "뿌리를 빼내기 위해 많은 시간을 보냈다."
    };

    private string[] result2 = {
        "",
        "그냥 먹으라 했으니 먹었다.",
        "꿀꺽 삼키니, 몸에 힘이 돌았다!",
    };

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
        else if (currentTextIndex >= textArray1.Length && currentTextIndex2 < result1.Length && bifurcation == 0)
        {
            Result1();
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex3 < result2.Length && bifurcation == 1)
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
        if (currentTextIndex < textArray1.Length && isWaitingForInput)
        {
            myText.text = textArray1[currentTextIndex];
        }
        else if (currentTextIndex2 < result1.Length && isWaitingForInput && bifurcation == 0)
        {
            myText.text = result1[currentTextIndex2];
        }
        else if (currentTextIndex3 < result2.Length && isWaitingForInput && bifurcation == 1)
        {
            myText.text = result2[currentTextIndex3];
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
                transparency.SetActive(true);
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
                transparency.SetActive(true);
                isWaitingForInput = false;
                CoreController.ThirstStatChange(-10);
                //갈증 10 감소
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
                transparency.SetActive(true);
                isWaitingForInput = false;
                CoreController.HungerStatChange(10);
                CoreController.ThirstStatChange(10);
                //허기 10 , 갈증 10 증가
            }
        }
    }
}
