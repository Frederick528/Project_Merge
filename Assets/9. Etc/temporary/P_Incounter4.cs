using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_Incounter4 : MonoBehaviour
{
    public bool isWaitingForInput = true;
    public int bifurcation = 0;
    public Text myText, mytext2;
    public GameObject incounter, transparency, Character, select1, select2, select3;
    public Image canvasImage;
    public float fadeSpeed = 0.5f; // ������ �پ��� �ӵ�
    public SelectBtn selectbtn; 

    [SerializeField]
    Button nextBtn;
    [SerializeField]
    GameObject blockUI, closeBtn;

    int currentTextIndex, currentTextIndex4, currentTextIndex5 = 0;
    int currentTextIndex6 = 0, currentTextIndex7 = 0, currentTextIndex8 = 0, currentTextIndex9 = 0;
    private string[] textArray1_1 = {"\"�ȳ�? ������? �־�? �غ�Ȱ���?\" ",
                                     "����� ���ڸ��� ������ ������ �����ߴ�."};
    private string[] CharacterName1_1 = { "����", "" };
    private string[] R_CharacterName1_1 = { "", "����", "", "" };
    private string[] R_CharacterName1_2 = { "", "����", "", "", "����" };

    private string[] result1_1 = { "", "\"������ �������� �Ѱų�?\" ", "����� �Ѽ��� ���� �����̸� ���� ������ �ֵѷ���.", "*���� �ָӴϰ� �������� �� ���١�" };
    private string[] result1_2 = { "", "����.. �̰���.. ����!!\"", "����� �����̸� ���� ������ �ֵѷ���.", "�ָӴϰ� ���������� ��������.", "\"�̰� ������. ������ �� �θ��� �𸣰����� ���� ����.\"" };

    private string[] textArray2_1 = {"\"�ϴ��� �� ������ �� ����\"",
                                     "���õ� ���డ ã�ƿԽ��ϴ�."};
    private string[] textArray2_2 = {"\"�ƴ� ��, ����ִ����� Ȯ���ؾ��ϴϱ�..\"",
                                     "\"������ ���� �ƴѵ�, Ȥ�� ���࿡ ���ؼ� ��� ������?\""};

   
    private string[] CharacterName2_1 = { "����", "" };
    private string[] CharacterName2_2 = { "", "����","����" };
    private string[] R_CharacterName2_1 = { "", "����", "" };
    private string[] R_CharacterName2_2 = { "", "����", ""};


    private string[] result2_1 = { "", "\"�±��ѵ�.. ����� �տ��� �׷��� �̾߱��ϸ� �� �ӻ��ѵ�?\"", "����� �ణ�� ���ָ� ������ �ɾ���. �谡 �� ��������." };
    private string[] result2_2 = { "", "\"�졦 �׷�����.. ��. �� ���� ���� ����� �׷����� ���ڳ�.\"", "�׳�� ȥ�� �߾�Ÿ��ٰ� ���� ����������."};

    void Start()
    {
        if (selectbtn.bifurcation15 == true)
        {
            myText.text = textArray1_1[0];
            mytext2.text = CharacterName1_1[0];
        }

        if (selectbtn.bifurcation15 == false)
        {
            myText.text = textArray2_1[0];
            mytext2.text = CharacterName2_1[0];
        }
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
        if (currentTextIndex < textArray1_1.Length && selectbtn.bifurcation15 == true)
        {
            Incounter1_1();
        }
        
        else if (currentTextIndex >= textArray1_1.Length && currentTextIndex4 < result1_1.Length && bifurcation == 0 && selectbtn.bifurcation15 == true)
        {
            Result1_1();
        }
        else if (currentTextIndex >= textArray1_1.Length && currentTextIndex5 < result1_2.Length && bifurcation == 1 && selectbtn.bifurcation15 == true)
        {
            Result1_2();
        }


        else if (currentTextIndex6 < textArray2_1.Length && selectbtn.bifurcation15 == false)
        {
            Incounter2_1();
        }
        else if (currentTextIndex6 >= textArray2_1.Length && currentTextIndex7 < textArray2_2.Length && selectbtn.bifurcation15 == false)
        {
            Incounter2_2();
        }

        else if (currentTextIndex6 >= textArray2_1.Length && currentTextIndex7 >= textArray2_2.Length && currentTextIndex8 < result2_1.Length && bifurcation == 0 && selectbtn.bifurcation15 == false)
        {
            Result2_1();
        }
        else if (currentTextIndex6 >= textArray2_1.Length && currentTextIndex7 >= textArray2_2.Length && currentTextIndex9 < result2_2.Length && bifurcation == 1 && selectbtn.bifurcation15 == false)
        {
            Result2_2();
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
        if (currentTextIndex < textArray1_1.Length && isWaitingForInput && selectbtn.bifurcation15 == true)
        {
            myText.text = textArray1_1[currentTextIndex];
        }
        else if (currentTextIndex4 < result1_1.Length && isWaitingForInput && bifurcation == 0 && selectbtn.bifurcation15 == true)
        {
            myText.text = result1_1[currentTextIndex4];
        }
        else if (currentTextIndex5 < result1_2.Length && isWaitingForInput && bifurcation == 1 && selectbtn.bifurcation15 == true)
        {
            myText.text = result1_2[currentTextIndex5];
        }

        else if (currentTextIndex6 < textArray2_1.Length && isWaitingForInput && selectbtn.bifurcation15 == false)
        {
            myText.text = textArray2_1[currentTextIndex6];
        }
        else if (currentTextIndex7 < textArray2_2.Length && isWaitingForInput && selectbtn.bifurcation15 == false)
        {
            myText.text = textArray2_2[currentTextIndex7];
        }
        else if (currentTextIndex8 < result2_1.Length && isWaitingForInput && bifurcation == 0 && selectbtn.bifurcation15 == false)
        {
            myText.text = result2_1[currentTextIndex8];
        }
        else if (currentTextIndex9 < result2_2.Length && isWaitingForInput && bifurcation == 1 && selectbtn.bifurcation15 == false)
        {
            myText.text = result2_2[currentTextIndex9];
        }
        else
        {
            Debug.Log("Not Text");
        }
    }

    void UpdateText2(string[] textArray)
    {
        // �迭 ���� Ȯ�� �� ������Ʈ
        if (currentTextIndex < CharacterName1_1.Length && isWaitingForInput && selectbtn.bifurcation15 == true)
        {
            mytext2.text = CharacterName1_1[currentTextIndex];
            if (mytext2.text == "����")
            {
                StartCoroutine(FadeIn());
            }
        }
        else if (currentTextIndex4 < R_CharacterName1_1.Length && isWaitingForInput && bifurcation == 0 && selectbtn.bifurcation15 == true)
        {
            mytext2.text = R_CharacterName1_1[currentTextIndex4];
            if (mytext2.text != "����")
            {
                StartCoroutine(FadeOut());
            }
        }
        else if (currentTextIndex5 < R_CharacterName1_2.Length && isWaitingForInput && bifurcation == 1 && selectbtn.bifurcation15 == true)
        {
            mytext2.text = R_CharacterName1_2[currentTextIndex5];
            if (mytext2.text != "����")
            {
                StartCoroutine(FadeOut());
            }
        }

        if (currentTextIndex6 < CharacterName2_1.Length && isWaitingForInput && selectbtn.bifurcation15 == false)
        {
            mytext2.text = CharacterName2_1[currentTextIndex6];
            if (mytext2.text == "����")
            {
                StartCoroutine(FadeIn());
            }
        }
        if (currentTextIndex7 < CharacterName2_2.Length && isWaitingForInput  && selectbtn.bifurcation15 == false)
        {
            mytext2.text = CharacterName2_2[currentTextIndex7];
        }

        else if (currentTextIndex8 < R_CharacterName2_1.Length && isWaitingForInput && bifurcation == 0 && selectbtn.bifurcation15 == false)
        {
            mytext2.text = R_CharacterName2_1[currentTextIndex8];
            if (mytext2.text == "")
            {
                StartCoroutine(FadeOut());
            }

        }

        else if (currentTextIndex9 < R_CharacterName2_2.Length && isWaitingForInput && bifurcation == 1 && selectbtn.bifurcation15 == false)
        {
            mytext2.text = R_CharacterName2_2[currentTextIndex9];
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

    public void Incounter1_1()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true)
        {
            currentTextIndex++;
            if (currentTextIndex < textArray1_1.Length)
            {
                UpdateText(textArray1_1);
                UpdateText2(CharacterName1_1);
            }
            if (currentTextIndex >= textArray1_1.Length)
            {
                transparency.SetActive(true);
                select1.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }

    public void Result1_1()
    {
        bifurcation = 0;
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 0)
        {
            currentTextIndex4++;
            if (currentTextIndex4 < result1_1.Length)
            {
                UpdateText(result1_1);
                UpdateText2(R_CharacterName1_1);
            }
            if (currentTextIndex4 >= result1_1.Length)
            {
                transparency.SetActive(true);
                isWaitingForInput = false;
            }
        }
 }

    public void Result1_2()
    {
        bifurcation = 1;

        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 1)
        {
            currentTextIndex5++;
            if (currentTextIndex5 < result1_2.Length)
            {
                UpdateText(result1_2);
                UpdateText2(R_CharacterName1_2);
            }
            if (currentTextIndex5 >= result1_2.Length)
            {
                transparency.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }

    public void Incounter2_1()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true )
        {
            currentTextIndex6++;
            if (currentTextIndex6 < textArray2_1.Length)
            {
                UpdateText(textArray2_1);
                UpdateText2(CharacterName2_1);
            }
            if (currentTextIndex6 >= textArray2_1.Length)
            {
                transparency.SetActive(true);
                select2.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }
    public void Incounter2_2()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true)
        {
            currentTextIndex7++;
            if (currentTextIndex7 < textArray2_2.Length)
            {
                UpdateText(textArray2_2);
                UpdateText2(CharacterName2_2);
            }
            if (currentTextIndex7 >= textArray2_2.Length)
            {
                transparency.SetActive(true);
                select3.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }

    public void Result2_1()
    {
        bifurcation = 0;

        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 0)
        {
            currentTextIndex8++;
            if (currentTextIndex8 < result2_1.Length)
            {
                UpdateText(result2_1);
                UpdateText2(R_CharacterName2_1);
            }
            if (currentTextIndex8 >= result2_1.Length)
            {
                transparency.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }

    public void Result2_2()
    {
        bifurcation = 1;

        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 1)
        {
            currentTextIndex9++;
            if (currentTextIndex9 < result2_2.Length)
            {
                UpdateText(result2_2);
                UpdateText2(R_CharacterName2_2);
            }
            if (currentTextIndex9 >= result2_2.Length)
            {
                transparency.SetActive(true);
                isWaitingForInput = false;
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

        Debug.Log("Image faded in completely.");
    }
}