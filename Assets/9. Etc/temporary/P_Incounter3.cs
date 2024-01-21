using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_Incounter3 : MonoBehaviour
{
    public bool isWaitingForInput = true;
    public int bifurcation = 0;
    public Text myText, mytext2;
    public GameObject incounter, transparency, Character, select1, select2;
    public Image canvasImage;
    public float fadeSpeed = 0.5f; // ������ �پ��� �ӵ�

    public SelectBtn selectbtn;

    [SerializeField]
    Button nextBtn;
    [SerializeField]
    GameObject blockUI, closeBtn;

    int currentTextIndex, currentTextIndex1, currentTextIndex4, currentTextIndex5 = 0;
    private string[] textArray1 = { "�̰��� ���������� 10����. ",
                                    "�� �ð���� �׻� ã�ƿ��� ����� �ϳ� �ִ�.",
                                    "�ȳ�? ��, �̹� �� �� �˰� �־�����",
                                    "���� �׳డ �� �� �˰� �̸� ���ڸ� �ϳ� ��������."};

    private string[] textArray2 = {"",
                                    "\"������ �� ������ �ʿ��ؼ� ã�ƿԾ�.\"",
                                    "����� �ڽ� ȥ�ڼ� ����� ��ٷο� ��ǰ�� �ֹ��ߴ�.",
                                    "�ٸ��� ���������, �ڽſ��� �� �ʿ��� �����̶�� �Ѵ�..",
                                    "\"������ָ� �� ���� �ɾ��ٰ�!! ����!!\"",
                                    "�� �̻��� ���������� �𸥴�.."};

    private string[] CharacterName1 = { "", "", "����", "" };
    private string[] CharacterName2 = { "", "����", "", "", "����", "" };
    private string[] R_CharacterName1 = { "", "����", "", "" };
    private string[] R_CharacterName2 = { "", "����", "", "", "" };

    private string[] result1 = { "", "\"������ ����!! 5�� �ڿ� �ð�!!\"", "�׳�� �׷��� �ٽ� ������ ����.", "���� ������ �׷���.." };
    private string[] result2 = { "", "\"�׷� ���� �������� �ȹ޴´�!!\"", "����� �����̸� ũ�� ������, ������ ������ ���.", "�� ������ �� ���̾���..", "���� ��û���� ���ſ�����..!!" };


    void Start()
    {
        myText.text = textArray1[0];
        mytext2.text = CharacterName1[0];
        Turn.Instance.nextBtn.interactable = false;
        Turn.Instance.closeBtn.SetActive(true);
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
            if (mytext2.text == "����" )
            {
                StartCoroutine(FadeIn());
            }
        }
        else if (currentTextIndex1 < CharacterName2.Length && isWaitingForInput)
        {
            mytext2.text = CharacterName2[currentTextIndex1];
        }

        else if (currentTextIndex4 < R_CharacterName1.Length && isWaitingForInput && bifurcation == 0)
        {
            mytext2.text = R_CharacterName1[currentTextIndex4];
            if (mytext2.text != "����")
            {
                StartCoroutine(FadeOut());
            }

        }
       
        else if (currentTextIndex5 < R_CharacterName2.Length && isWaitingForInput && bifurcation == 1)
        {
            mytext2.text = R_CharacterName2[currentTextIndex5];
            if (mytext2.text != "����")
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
                selectbtn.bifurcation15 = true;
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
                selectbtn.bifurcation15 = false;
                // ��� ���� 50% ����
            }
        }
    }


    IEnumerator FadeOut()
    {
        float targetAlpha = 0f;

        while (canvasImage.color.a >= targetAlpha)
        {
            float currentAlpha = canvasImage.color.a;
            currentAlpha -= fadeSpeed * Time.deltaTime;
            canvasImage.color = new Color(canvasImage.color.r, canvasImage.color.g, canvasImage.color.b, currentAlpha);

            yield return null;
        }
    }
    IEnumerator FadeIn()
    {
        float targetAlpha = 1f;

        while (canvasImage.color.a < targetAlpha)
        {
            float currentAlpha = canvasImage.color.a;
            currentAlpha += fadeSpeed * Time.deltaTime;
            canvasImage.color = new Color(canvasImage.color.r, canvasImage.color.g, canvasImage.color.b, currentAlpha);

            yield return null;
        }

        // ������ 1 �̻����� �ö��� ���� ó��
        Debug.Log("Image faded in completely.");
    }
}