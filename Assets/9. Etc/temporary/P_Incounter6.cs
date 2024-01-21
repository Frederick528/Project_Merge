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
    public float fadeSpeed = 0.5f; // ������ �پ��� �ӵ�

    [SerializeField]
    Button nextBtn;
    [SerializeField]
    GameObject blockUI, closeBtn;

    int currentTextIndex, currentTextIndex1, currentTextIndex4, currentTextIndex5, currentTextIndex6 = 0;
    private string[] textArray1 = { "�̰����� ���� 25����.",
                                    "���ó� ���� �� �����ִ��� �� �𸣰ڴ�.",
                                    "����� �����̰�",
                                    "���� �����̸�",
                                    "������ ���� ������ ���� �����ִ°�." };

    private string[] textArray2 = {"",
                                    "����� 30���� ��Ƽ�� �̰����� �ٸ� ������ �����ְڴٰ� �߾���..",
                                    "������ ���شٰ� ������ ����.",
                                    "����� Ȯ���� ���𰡸� ����� �־���.",
                                    "���డ ��������.." };


    private string[] result1_1 = { "",
                                 "���� ������ ���� ��������.",
                                 "�� �̻��� �� ���� �� ���� ���̴�.",
                                 "õõ�� ������ �ָ����� ���ư��� �����ߴ�.",
                                 "��������"
    };

    private string[] result1_2 = { "",
                                 "���� ����� ���ƿ´�..",
                                 "���� ������� ���",
                                 "�����ϰ� �;��� ����� �� ��������.",
                                 "��ﳵ��.",
                                 "���� �̰����� ����� �ذ� ġ��ޱ� ���ؼ� ���Դ�.",
                                 "..�׷��� ���డ �� ������ �����ǰ�..",
                                 "����..",
                                 "������ ���������",
                                 "�� �̻� ���ư� �� ������.",
                                 "�� �ڿ� �ִ� ���� �� �̻� ������ �ʾҰ�..",
                                 "�� ������ �̷��� ���ߴ�.",
                                 "-BAD ENDING-"
    };

    private string[] result2 = { "", "�ƴϴ�. ���ݸ� �� �����غ��ƾ��� �����̴�.",
                                     "�и� ����� ���� ��� ���ϻ�������.. ���������� �׷��� �ʾҴ�.",
                                     "��ġ �� ���¸� ���� ���ָ� �Ŵ�.. �׷� �����̾���.",
                                     "���� ���忡 ������ �ظ� ��ĥ ���� ���� ���̴�.",
                                     "�׸��� ���.. ���� �̰��� ������ ��°����� �𸣰ڴ�.",
                                     "�͸� ��ġ�� �ٶ��� ��鸮�� �ܵ�Ҹ���",
                                     "���� ������ ���������� �� ������..",
                                     "���ݸ� �� ���°� ���� �� ����.",
                                     "[��������]",
                                     "�� �ڿ��� �ȴ� �Ҹ��� ����´�.",
                                     "�����ߴ���� ���࿴��",
                                     "���� ������ �ٸ��� ��������ʰ� ���ฦ �����־���."};


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
        // �迭 ���� Ȯ�� �� ������Ʈ
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