using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_Incounter20 : MonoBehaviour
{
    public bool isWaitingForInput = true;
    public int bifurcation = 0;
    public Text myText, mytext2;
    public GameObject incounter, transparency, select1, select2;


    [SerializeField]
    Button nextBtn;
    [SerializeField]
    GameObject blockUI, closeBtn;

    int currentTextIndex, currentTextIndex1, currentTextIndex4, currentTextIndex5 = 0;
    private string[] textArray1 = { "\"안녕 친구! 이 시간에 보는일은 별로 없었지?\"",
                                  };

    private string[] textArray2 = {"",
                                    "\"맞아맞아, 떠돌이 상인이야! \"",
                                    "\"오늘은 뭐 팔러온건 아니고 선물을 주려고 왔어! 골라봐!\""};

    private string[] CharacterName1 = { " 떠돌이 상인" };
    private string[] CharacterName2 = { "", "떠돌이 상인","떠돌이 상인" };
    private string[] R_CharacterName1 = { "", " 떠돌이 상인", " 떠돌이 상인", " 떠돌이 상인"};
    private string[] R_CharacterName2 = { "", " 떠돌이 상인", " 떠돌이 상인", " 떠돌이 상인"};

    private string[] result1 = { "", "나는 원재료가 들어있는 상자를 가르켰다.", "\"오? 역시 필요할까 싶어서 챙겼는데, 다행이네!\"", "\"나중에 또 보자!\"" };
    private string[] result2 = { "", "\"이게 뭔지 잘 모르겠는데 역시 비싼걸 챙길 줄 알았어\"", "\"한번 잘 써봐!\"", "나중에 또 보자!"};


    void Start()
    {
        myText.text = textArray1[0];
        mytext2.text = CharacterName1[0];
        nextBtn.interactable = false;
        closeBtn.SetActive(true);
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
            closeBtn.SetActive(false);
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

    void UpdateText2(string[] textArray)
    {
        // 배열 길이 확인 후 업데이트
        if (currentTextIndex < CharacterName1.Length && isWaitingForInput)
        {
            mytext2.text = CharacterName1[currentTextIndex];
        }
        else if (currentTextIndex1 < CharacterName2.Length && isWaitingForInput)
        {
            mytext2.text = CharacterName2[currentTextIndex1];
        }

        else if (currentTextIndex4 < R_CharacterName1.Length && isWaitingForInput && bifurcation == 0)
        {
            mytext2.text = R_CharacterName1[currentTextIndex4];
        }
       
        else if (currentTextIndex5 < R_CharacterName2.Length && isWaitingForInput && bifurcation == 1)
        {
            mytext2.text = R_CharacterName2[currentTextIndex5];
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
                UpdateText2(CharacterName1);
            }
            if (currentTextIndex >= textArray1.Length)
            {
                transparency.SetActive(true);
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
                UpdateText2(CharacterName2);
            }
            if (currentTextIndex1 >= textArray2.Length)
            {
                transparency.SetActive(true);
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
                UpdateText2(R_CharacterName1);
            }
            if (currentTextIndex4 >= result1.Length)
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
            currentTextIndex5++;
            if (currentTextIndex5 < result2.Length)
            {
                UpdateText(result2);
                UpdateText2(R_CharacterName2);
            }
            if (currentTextIndex5 >= result2.Length)
            {
                transparency.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }

}