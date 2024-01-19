using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_Incounter17 : MonoBehaviour
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
        "저번에 설치해둔 덫을 확인하고 싶다.",
        "하지만 요즘 근처에 곰이 도사리고 있던데..",
        "지금 확인을 안하면 잡힌 동물이 도망갈지도 모른다."
    };

    private string[] result1 = {
        "",
        "지금 확인하기로 했다!",
        "다행히 근처에 곰은 없는 것 같다!",
        "나는 최선을 다해 덫으로 뛰어갔다!",
        "하지만 이미 덫은 동물의 다리 한쪽을 빼고 아무것도 남아있지 않았다.",
        "누가 이미 먹은 것이겠지.."
    };

    private string[] result2 = {
        "",
        "곰 때문에 너무 위험하다.",
        "그냥 다음에 근처에 덫을 만드는걸로 하자.",
    };

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
            nextBtn.interactable = true;
            blockUI.SetActive(false);
            closeBtn.SetActive(false);
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
            }
        }
    }
}
