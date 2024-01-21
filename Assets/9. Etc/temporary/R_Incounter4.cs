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

    private string[] textArray1 = { "������ �۾��ϰ� �ִ� ��. �ϴÿ��� � ������ �����Դ�.",
                                   "����. ��� �����ִ�. �ƹ��͵� �������ִ� ���̿� �������� �۾��� ���̱� �����ߴ�."};
    private string[] textArray2 = {"","������ �ϳ� ���� �ȸ��� ���� ����, ���߸� ���� �ְ� Ʋ���� ������������! �����!",
                                   "����� ��� ���� ���� Ŀ���� ����?"};
    private string[] result1 = { "", "���� ������ ���� \"����\" �̶�� ����ߴ�.", "\"�����̾�, �̰� ���ߴٴ�. ��̾���.\"", "\"�ƹ�ư ����� ���̾�.\"","����� 10 ����", 
                                 "���� ��������°� ��������.", "���ֿ����� � ȿ�������� ���� ���������١�." };
    private string[] result2 = { "", "���� ������ ���� \"���\" �̶�� ����ߴ�.", "\"���? ����̶�.. �װ͵� ������ �ʳס� ���� �ƴ�����!\"", 
                                 "\"������ �׷��� ����� ������ �������, �ణ�� ���ָ� �ٰ�!\"","����� 10 ����","���� �������� �� ����..","������ ������� �ѹ��� ������.." };

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
