using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_Incounter5 : MonoBehaviour
{
    public bool isWaitingForInput = true;
    public int bifurcation = 0;
    public Text myText, mytext2;
    public GameObject incounter, transparency, Character, select1, select2, select3 ,select4, select5, select6;
    public Image canvasImage;
    public float fadeSpeed = 0.5f; // ������ �پ��� �ӵ�

    [SerializeField]
    Button nextBtn;
    [SerializeField]
    GameObject blockUI, closeBtn;

    int currentTextIndex, currentTextIndex1, currentTextIndex2, currentTextIndex3, currentTextIndex4, currentTextIndex5, currentTextIndex6 = 0;
    private string[] textArray1 = { "������ ���డ ã�ƿ��� ���̴�.",
                                    "�׷���, ���ڱ� ���� �ñ��� ���� �����."};

    private string[] textArray2 = {"",
                                    "���� ���⿡ �������ߴ�. ",
                                    "�и��� �׷�����.",
                                    "������ ���� ��� �Դ���, ��� �Ȱ����� ���� ����� ����.",
                                    "��ġ ���� ���� �� ó��",
                                    "�Ӹ��� ���Ŀ´�.."};

    private string[] textArray3 = { "",
                                    "\"�� ��� ��?\"",
                                    "������ ���డ ���Դ�. ",
                                    "\"���� �ϰ���� ���̶� �ִ°ž�?\"",
                                    "�Ҹ��� ����. ������ ���� ����ų �� ���� �������� �𸥴�.."};

    private string[] textArray4 = { "",
                                    "\"�� �´� ������ ���߳�.\"",
                                    "\"�̰��� ������ ���̾�. �̰����� ������ �������� ����� ����.\"" };

    private string[] textArray5 = { "",
                                    "\"���� ������ ���ݾ�.\""};

    private string[] textArray6 = { "",
                                    "\"����! �ɰ�� ����. ���� �ɸ����̾�.\"",
                                    "\"��.. 30������?\"" };

    private string[] textArray7 = { "",
                                    "\"�׷��ϱ� ���̾�..\"",
                                    "�ƹ�ư ���ο� ����� �˰Ե� ���̾���." };




    //private string[] CharacterName1 = { "","" };
   // private string[] CharacterName2 = { "", "", "", "", "","","" };
    private string[] CharacterName3 = { "", "����", "","����","" };
    private string[] CharacterName4 = { "", "����", "����" };
    private string[] CharacterName5 = { "", "����"};
    private string[] CharacterName6 = { "", "����", "����" };
    private string[] CharacterName7 = { "", "����", "" };
    void Start()
    {
        myText.text = textArray1[0];
      //  mytext2.text = CharacterName1[0];
        nextBtn.interactable = false;
        //closeBtn.SetActive(true);
        if (canvasImage == null)
        {
            canvasImage = GetComponent<Image>();
            if (canvasImage == null)
            {
                Debug.LogError("Image component not found.");
                return;
            }
        }
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
        else if (currentTextIndex >= textArray1.Length && currentTextIndex1 >= textArray2.Length && currentTextIndex2 >= textArray3.Length && currentTextIndex3 >= textArray4.Length &&
                 currentTextIndex4 < textArray5.Length)
        {
            Select4();
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex1 >= textArray2.Length && currentTextIndex2 >= textArray3.Length && currentTextIndex3 >= textArray4.Length &&
                currentTextIndex4 >= textArray5.Length && currentTextIndex5 < textArray6.Length)
        {
            Select5();
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
        else if (currentTextIndex4 < textArray5.Length && isWaitingForInput)
        {
            myText.text = textArray5[currentTextIndex4];
        }
        else if (currentTextIndex5 < textArray6.Length && isWaitingForInput)
        {
            myText.text = textArray6[currentTextIndex5];
        }
        else if (currentTextIndex6 < textArray7.Length && isWaitingForInput)
        {
            myText.text = textArray7[currentTextIndex6];
        }
        else
        {
            Debug.Log("Not Text");
        }
    }

    void UpdateText2(string[] textArray)
    {
        // �迭 ���� Ȯ�� �� ������Ʈ
        /* if (currentTextIndex < CharacterName1.Length && isWaitingForInput)
         {
             mytext2.text = CharacterName1[currentTextIndex];
             if (mytext2.text == "����")
             {
                 StartCoroutine(FadeIn());
             }
         }
         else if (currentTextIndex1 < CharacterName2.Length && isWaitingForInput)
         {
             mytext2.text = CharacterName2[currentTextIndex1];
         } 
         else*/
        if (currentTextIndex2 < CharacterName3.Length && isWaitingForInput)
        {
            mytext2.text = CharacterName3[currentTextIndex2];
            if (mytext2.text == "����")
            {
                StartCoroutine(FadeIn());
            }
        }
        else if (currentTextIndex3 < CharacterName4.Length && isWaitingForInput)
        {
            mytext2.text = CharacterName4[currentTextIndex3];
        }
        else if (currentTextIndex4 < CharacterName5.Length && isWaitingForInput)
        {
            mytext2.text = CharacterName5[currentTextIndex4];
        }
        else if (currentTextIndex5 < CharacterName6.Length && isWaitingForInput)
        {
            mytext2.text = CharacterName6[currentTextIndex5];
        }
        else if (currentTextIndex6 < CharacterName7.Length && isWaitingForInput)
        {
            mytext2.text = CharacterName7[currentTextIndex6];
            if (mytext2.text == "")
            {
                StartCoroutine(FadeOut());
            }
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
               // UpdateText2(CharacterName1);
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
               // UpdateText2(CharacterName2);
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

    public void Select4()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true)
        {
            currentTextIndex4++;
            if (currentTextIndex4 < textArray5.Length)
            {
                UpdateText(textArray5);
                UpdateText2(CharacterName5);
            }
            if (currentTextIndex4 >= textArray5.Length)
            {
                transparency.SetActive(true);
                select5.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }

    public void Select5()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true)
        {
            currentTextIndex5++;
            if (currentTextIndex5 < textArray6.Length)
            {
                UpdateText(textArray6);
                UpdateText2(CharacterName6);
            }
            if (currentTextIndex5 >= textArray6.Length)
            {
                transparency.SetActive(true);
                select6.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }


    IEnumerator FadeOut()
    {
        float targetAlpha = 0f;

        while (canvasImage.color.a >= targetAlpha)
        {
            float currentAlpha = Mathf.MoveTowards(canvasImage.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            canvasImage.color = new Color(canvasImage.color.r, canvasImage.color.g, canvasImage.color.b, currentAlpha);

            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        float targetAlpha = 1f;

        while (canvasImage.color.a < targetAlpha)
        {
            float currentAlpha = Mathf.MoveTowards(canvasImage.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            canvasImage.color = new Color(canvasImage.color.r, canvasImage.color.g, canvasImage.color.b, currentAlpha);

            yield return null;
        }

        // ������ 1 �̻����� �ö��� ���� ó��
        Debug.Log("Image faded in completely.");
    }
}