using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public bool isWaitingForInput = true;
    public int bifurcation = 0;
    public Text myText, mytext2;
    public GameObject incounter, select1, select2, select3, select4;
    int currentTextIndex, currentTextIndex1, currentTextIndex2, currentTextIndex3, currentTextIndex4, currentTextIndex5 = 0;
    private string[] textArray1 = { "부스럭. 부스럭.",
                                   "어디선가 나뭇잎이 흔들리는 소리가 들렸다.",
                                   "이곳은 어딘지도 모를, 나갈 수 없는 숲이다.",
                                   "그런 곳에 나는 지금 혼자 남겨졌다." };

    private string[] textArray2 = {"",
                                    "그런 생각이 내 머릿속을 맴돌 때, 내 인기척에 누군가 다가오는게 느껴졌다.",
                                    "이상할 정도로 긴 머리카락에, 이런 숲에서 살고있다고 보기도 힘든 그런 옷을 입고있는 여인이 앞에 있었다.",
                                    "사람인가? 어떻게 살아있는지 모르겠네. 일단은 반가워.",
                                    "믿기 힘들겠지만. 나는 마녀야." };

    private string[] textArray3 = { "",
                                    "뭐 믿든 말든 상관없어.",
                                    "나는 이 숲을 지키는 마녀야. 너는 이 숲에 무단으로 들어온 외지인이고." };

    private string[] textArray4 = { "",
                                    "별거 아니야. 30일. 딱 30일을 여기서 살아서 살아있다면.",
                                    "그때는 다른 곳으로 너를 안내해줄게.",
                                    "그녀는 그 말과 함께 주머니 뒤에서 황금빛 사과를 내게 건내며 사라졌다.",
                                    "나는 혼란스러운 마음을 안고 사과를 바라보았다.",
                                    "이 숲의 마녀가 나를 돌봐준다니. 이게 무슨일인걸까..",
                                    "마침 배고파 죽기 직전이다.. 이걸 먹어도 괜찮을까?"};



    private string[] CharacterName1 = { "", "", "", "" };
    private string[] CharacterName2 = { "", "", "", "???", "마녀" };
    private string[] CharacterName3 = { "", "마녀", "마녀" };
    private string[] CharacterName4 = { "", "마녀", "마녀", "", "", "", "" };

    private string[] result1 = { "", "아삭.", "사과를 한입 베어먹었다.", "무언가 몸이.. 건강해진 것 같다..!", "일단은 뭐든.. 30일을 한번 버텨보자.." };
    private string[] result2 = { "", "그래도 뭔가 먹기가 좀 그렇다.", "일단은 들고는 있자", "일단은 뭐든.. 30일을 한번 버텨보자.." };


    void Start()
    {
        myText.text = textArray1[0];
        mytext2.text = CharacterName1[0];
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
        else if (currentTextIndex >= textArray1.Length && currentTextIndex1 >= textArray2.Length && currentTextIndex2 < textArray3.Length)
        {
            Select2();
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex1 >= textArray2.Length && currentTextIndex2 >= textArray3.Length && currentTextIndex3 < textArray4.Length)
        {
            Select3();
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex1 >= textArray2.Length && currentTextIndex2 >= textArray3.Length
                 && currentTextIndex3 >= textArray4.Length && currentTextIndex4 < result1.Length && bifurcation == 0)
        {
            Result1();
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex1 >= textArray2.Length && currentTextIndex2 >= textArray3.Length
                 && currentTextIndex3 >= textArray4.Length && currentTextIndex5 < result2.Length && bifurcation == 1)
        {
            Result2();
        }
        else 
        {
            incounter.SetActive(false);
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
        else if (currentTextIndex2 < textArray3.Length && isWaitingForInput)
        {
            myText.text = textArray3[currentTextIndex2];
        }
        else if (currentTextIndex3 < textArray4.Length && isWaitingForInput)
        {
            myText.text = textArray4[currentTextIndex3];
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
        else if (currentTextIndex2 < CharacterName3.Length && isWaitingForInput)
        {
            mytext2.text = CharacterName3[currentTextIndex2];
        }
        else if (currentTextIndex3 < CharacterName4.Length && isWaitingForInput)
        {
            mytext2.text = CharacterName4[currentTextIndex3];
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
                UpdateText2(CharacterName1);
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
                UpdateText2(CharacterName2);
            }
            if (currentTextIndex1 >= textArray2.Length)
            {
                select2.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }

    public void Select2()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true)
        {
            currentTextIndex2++;
            if (currentTextIndex2 < textArray3.Length)
            {
                UpdateText(textArray3);
                UpdateText2(CharacterName3);
            }
            if (currentTextIndex2 >= textArray3.Length)
            {
                select3.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }

    public void Select3()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true)
        {
            currentTextIndex3++;
            if (currentTextIndex3 < textArray4.Length)
            {
                UpdateText(textArray4);
                UpdateText2(CharacterName4);
            }
            if (currentTextIndex3 >= textArray4.Length)
            {
                select4.SetActive(true);
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
                select4.SetActive(true);
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
                
                select4.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }
}