using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_Incounter3 : MonoBehaviour
{
    public bool isWaitingForInput = true;
    public int bifurcation = 0;
    public Text myText;
    public GameObject incounter, select1, select2;


    [SerializeField]
    Button nextBtn;
    [SerializeField]
    GameObject blockUI, closeBtn;

    int currentTextIndex, currentTextIndex2, currentTextIndex3, currentTextIndex4 = 0;

    private string[] textArray1 = { "�ڿ��� ���ϱ� ���� ������ Ž���ϴ� ��. ������ ���ڸ� �߰��ߴ�.",
                                   "�̳��� ���� �����ְ�, ���� �ð� ���� �ǵ��� ���� ������ �� ����."};
    private string[] result1 = { "", "���� ħ�� ��Ű�� ���ڸ� ������.", "�ȿ��� ���� ������ �ϳ� �־���.", "���� �� ������ ������ �����.., "};
    private string[] result2 = { "", "������ �ִ� ���� �ƹ� ���ڳ� ���� �ȵ� �� ����.", "������ ���� ������.. �׳� �ΰ���."};

    private string[] result1_a = {"", "���� ������ ������ �� ���� �⵹�Ҵ�." };


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
            Select1();
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
        // �迭 ���� Ȯ�� �� ������Ʈ
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
                isWaitingForInput = false;
            }
        }
    }

    public void Select1()
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
}