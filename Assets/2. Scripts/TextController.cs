using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public bool isWaitingForInput = true;
    public int bifurcation = 0;
    public Text myText, mytext2;
    public GameObject incounter, transparency, Character, select1, select2, select3, select4;
    int currentTextIndex, currentTextIndex1, currentTextIndex2, currentTextIndex3, currentTextIndex4, currentTextIndex5 = 0;
    private string[] textArray1 = { "�ν���. �ν���.",
                                   "��𼱰� �������� ��鸮�� �Ҹ��� ��ȴ�.",
                                   "�̰��� ������� ��, ���� �� ���� ���̴�.",
                                   "�׷� ���� ���� ���� ȥ�� ��������." };

    private string[] textArray2 = {"",
                                    "�׷� ������ �� �Ӹ����� �ɵ� ��, �� �α�ô�� ������ �ٰ����°� ��������.",
                                    "�̻��� ������ �� �Ӹ�ī����, �̷� ������ ����ִٰ� ���⵵ ���� �׷� ���� �԰��ִ� ������ �տ� �־���.",
                                    "����ΰ�? ��� ����ִ��� �𸣰ڳ�. �ϴ��� �ݰ���.",
                                    "�ϱ� ���������. ���� �����." };

    private string[] textArray3 = { "",
                                    "�� �ϵ� ���� �������.",
                                    "���� �� ���� ��Ű�� �����. �ʴ� �� ���� �������� ���� �������̰�." };

    private string[] textArray4 = { "",
                                    "���� �ƴϾ�. 30��. �� 30���� ���⼭ ��Ƽ� ����ִٸ�.",
                                    "�׶��� �ٸ� ������ �ʸ� �ȳ����ٰ�.",
                                    "�׳�� �� ���� �Բ� �ָӴ� �ڿ��� Ȳ�ݺ� ����� ���� �ǳ��� �������.",
                                    "���� ȥ�������� ������ �Ȱ� ����� �ٶ󺸾Ҵ�.",
                                    "�� ���� ���డ ���� �����شٴ�. �̰� �������ΰɱ�..",
                                    "��ħ ����� �ױ� �����̴�.. �̰� �Ծ ��������?"};



    private string[] CharacterName1 = { "", "", "", "" };
    private string[] CharacterName2 = { "", "", "", "???", "����" };
    private string[] CharacterName3 = { "", "����", "����" };
    private string[] CharacterName4 = { "", "����", "����", "", "", "", "" };

    private string[] result1 = { "", "�ƻ�.", "����� ���� ����Ծ���.", "���� ����.. �ǰ����� �� ����..!", "�ϴ��� ����.. 30���� �ѹ� ���ߺ���.." };
    private string[] result2 = { "", "�׷��� ���� �ԱⰡ �� �׷���.", "�ϴ��� ����� ����", "�ϴ��� ����.. 30���� �ѹ� ���ߺ���.." };


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
        // �迭 ���� Ȯ�� �� ������Ʈ
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
        // �迭 ���� Ȯ�� �� ������Ʈ
        if (currentTextIndex < CharacterName1.Length && isWaitingForInput)
        {
            mytext2.text = CharacterName1[currentTextIndex];
        }
        else if (currentTextIndex1 < CharacterName2.Length && isWaitingForInput)
        {
            mytext2.text = CharacterName2[currentTextIndex1];
            if (mytext2.text == "����" || mytext2.text == "???")
            {
                Character.SetActive(true);
            }
            else
            {
                Character.SetActive(false);
            }
        }
        else if (currentTextIndex2 < CharacterName3.Length && isWaitingForInput)
        {
            mytext2.text = CharacterName3[currentTextIndex2];
            if (mytext2.text == "����" || mytext2.text == "???")
            {
                Character.SetActive(true);
            }
            else
            {
                Character.SetActive(false);
            }
        }
        else if (currentTextIndex3 < CharacterName4.Length && isWaitingForInput)
        {
            mytext2.text = CharacterName4[currentTextIndex3];
            if (mytext2.text == "����" || mytext2.text == "???")
            {
                Character.SetActive(true);
            }
            else
            {
                Character.SetActive(false);
            }
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
                transparency.SetActive(true);
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
                transparency.SetActive(true);
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
                transparency.SetActive(true);
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
                transparency.SetActive(true);
                select4.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }
}