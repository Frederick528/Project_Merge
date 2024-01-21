using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_Incounter7 : MonoBehaviour
{
    public bool isWaitingForInput = true;
    public int bifurcation = 0;
    public Text myText, mytext2;
    public GameObject incounter, transparency, Character, select1, select2, select3,select4, select2_1, select2_2, select2_3,select2_4;
    public int re2_Select = 0;
    public Image canvasImage;
    public float fadeSpeed = 0.5f; // ������ �پ��� �ӵ�

    [SerializeField]
    Button nextBtn;
    [SerializeField]
    GameObject blockUI, closeBtn;

    int currentTextIndex, currentTextIndex1, currentTextIndex2, currentTextIndex3, currentTextIndex4, currentTextIndex5, currentTextIndex6, currentTextIndex7, currentTextIndex8, currentTextIndex9 = 0;
    private string[] textArray1 = { "\"���� 30���̳�.\"" };
    private string[] textArray2 = { "", "\"�غ�� �ƾ�?\"" };
    private string[] textArray3 = { "",
                                    "�׳�� ������ ����Ұ� ������ �ֳĸ� �Ѽ��� ������.",
                                    "\"�ϴ� �˾Ҿ� ��ٷ��ٰ�.\"",
                                    "�׳�� ��ٷ��ֱ�� �ߴ�.",
                                    "ģ���ϰԵ�."};

    private string[] CharacterName1 = { "����" };
    private string[] CharacterName2 = { "", "����" };
    private string[] CharacterName3 = { "", "", "����", "", "" };



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
    private string[] result2_1 = { "", "���� �׳�� �Բ� ���� ���⸦ û�ߴ�.",
                                       "\"�װ� �������̾�?\"",
                                       "\"���⿡�� ������ �ʹ��� �ƴϾ���?\"" };
    private string[] result2_2 = { "", "���� �ɸ��°� �־���.",
                                       "�ƴ� �̰� �´� �� ����.",
                                       "���� ����.. ���� ���ؼ� ���� ���°� �ƴѰ� �ʹ�.",
                                       "������ ������ ����� �Ұ�.. ġ���޴� �׷� ����.."};
    private string[] result2_3 = { "", "\"��, �����̳�. �׷��� �ʰ� �� ���ߴٴ°�.",
                                       "\"���� ������ �����ٰ�.\"",
                                       "\"�ʴ� ��鼭 ���� ���� ������ ��ó�� �̰��� ���� �Ǿ���.\""};
    private string[] result2_4 = { "", "��ﳭ��. ",
                                       "���� �Ѷ� ȸ�翡�� �������̾��� �������� ���� ��Ҵٰ� �ں��� �� �־���.",
                                       "������ �װ͵� ����ϻ�. ������� �� �׷� ������ �� ���� ���� ó�����״�.",
                                       "�ܾ����� �����ؼ� �������� �ϱ���. ",
                                       "���� �׶� �Ͽ��� ���� �ٸ� ���� �ƹ��͵� ���� ���ߴ�.",
                                       "�׷��� �� �λ��� ���̰� ���Ҵ�.",
                                       "����� �ѹ��� ���� �Ǽ��� �־�����.",
                                       "�̰� �ϳ� ���ϳĸ�� ������ ������ �ٸ� ����� �׷��� �����ϰ� ����� ���� ���̾���.",
                                       "�� �ڷ� �󸶳� ��������?",
                                       "����� ���� �ص� ���� �Դ� ������. �ʹ� ���Ӿ��� ��Ʈ������ ����..",
                                       "\"���������. �׷��ϱ� �㹫�Ͷ��� �̾߱��� �� ���� ���� �̾߱⸦ ��� ã�ƿ�����.\"",};
    private string[] result2_5 = { "", "\"���� ������ �� ��������. �����ϰ� ���ؼ� ���� ����. �׷��� ��\". ",
                                       "�׷���. ���� �� �̻� ���� �Ͽ� ���� ��������� �ʴ´�.",
                                       "���� �� �ʿ䵵 ������.",
                                       "���� ȥ�� �������. ���� ��ȸ�� ����. ",
                                       "�̰��� ������ ���� �ٸ� �̵��� ������ �� ���� ���̴�.",};


    private string[] R2_CharacterName1 = { "", "", "����", "����" };
    private string[] R2_CharacterName2 = { "", "", "", "", "" };
    private string[] R2_CharacterName3 = { "", "����", "����", "����" };
    private string[] R2_CharacterName4 = { "", "", "","","","","","","","","", "����" };
    private string[] R2_CharacterName5 = { "", "����", "", "", "", ""};

    void Start()
    {
        SoundManager.instance.Play("Sounds/Bgm/StoryBgm", Sound.Bgm, 0.2f);
        myText.text = textArray1[0];
        mytext2.text = CharacterName1[0];

        Turn.Instance.closeBtn.SetActive(true);
        Turn.Instance.nextBtn.interactable = false;
        if (canvasImage == null)
        {
            canvasImage = GetComponent<Image>();
            if (canvasImage == null)
            {
                Debug.LogError("Image component not found.");
                return;
            }
        }

        StartCoroutine(FadeIn()); // ������ �κ�
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
        else if (currentTextIndex2 < textArray3.Length)
        {
            Select2();
        }
        else if (currentTextIndex3 < result1_1.Length && bifurcation == 0)
        {
            Result1_1();   
        }
        else if (currentTextIndex9 < result1_2.Length && bifurcation == 0)
        {
            StartCoroutine(FadeOut());
            Result1_2();
        }
        else if (currentTextIndex4 < result2_1.Length && bifurcation == 1)
        {
            Result2_1();
        }
        else if (currentTextIndex5 < result2_2.Length && bifurcation == 1)
        {
            Result2_2();
        }
        else if (currentTextIndex6 < result2_3.Length && bifurcation == 1)
        {
            Result2_3();
        }
        else if (currentTextIndex7 < result2_4.Length && bifurcation == 1)
        {
            Result2_4();
        }
        else if (currentTextIndex8 < result2_5.Length && bifurcation == 1)
        {
            Result2_5();
        }
        else
        {
            incounter.SetActive(false);
            Turn.Instance.nextBtn.interactable = true;
            Turn.Instance.blockUI.SetActive(false);
            Turn.Instance.closeBtn.SetActive(false);
            GameManager.CardCanvasOn = false;
            SoundManager.instance.Play("Sounds/Bgm/GameBgm", Sound.Bgm, 0.3f);
        }
    }

    void UpdateText(string[] textArray)
    {
        // �迭 ���� Ȯ�� �� ������Ʈ
        if (currentTextIndex < textArray1.Length && isWaitingForInput)
        {
            myText.text = textArray1[currentTextIndex];
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex1 < textArray2.Length && isWaitingForInput)
        {
            myText.text = textArray2[currentTextIndex1];
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex1 >= textArray2.Length && currentTextIndex2 < textArray3.Length && isWaitingForInput)
        {
            myText.text = textArray3[currentTextIndex2];
        }
        else if (currentTextIndex3 < result1_1.Length && isWaitingForInput && bifurcation == 0)
        {
            myText.text = result1_1[currentTextIndex3];
        }
        else if (currentTextIndex9 < result1_2.Length && isWaitingForInput && bifurcation == 0)
        {
            myText.text = result1_2[currentTextIndex9];
        }
        else if (currentTextIndex4 < result2_1.Length && isWaitingForInput && bifurcation == 1)
        {
            myText.text = result2_1[currentTextIndex4];
        }
        else if (currentTextIndex5 < result2_2.Length && isWaitingForInput && bifurcation == 1)
        {
            myText.text = result2_2[currentTextIndex5];
        }
        else if (currentTextIndex6 < result2_3.Length && isWaitingForInput && bifurcation == 1)
        {
            myText.text = result2_3[currentTextIndex6];
        }
        else if (currentTextIndex7 < result2_4.Length && isWaitingForInput && bifurcation == 1)
        {
            myText.text = result2_4[currentTextIndex7];
        }
        else if (currentTextIndex8 < result2_5.Length && isWaitingForInput && bifurcation == 1)
        {
            myText.text = result2_5[currentTextIndex8];
        }
        else
        {
            Debug.Log("Not Text");
        }
    }

    void UpdateText2(string[] textArray)
    {
        if (currentTextIndex < CharacterName1.Length)
        {
            mytext2.text = CharacterName1[currentTextIndex];

        }
        else if (currentTextIndex1 < CharacterName2.Length)
        {
            mytext2.text = CharacterName2[currentTextIndex1];
        }
        else if (currentTextIndex2 < CharacterName3.Length)
        {
            mytext2.text = CharacterName3[currentTextIndex2];
        }
        else if (currentTextIndex4 < R2_CharacterName1.Length)
        {
            mytext2.text = R2_CharacterName1[currentTextIndex4];
        }
        else if (currentTextIndex5 < R2_CharacterName2.Length)
        {
            mytext2.text = R2_CharacterName2[currentTextIndex5];
        }
        else if (currentTextIndex6 < R2_CharacterName3.Length)
        {
            mytext2.text = R2_CharacterName3[currentTextIndex6];
        }
        else if (currentTextIndex7 < R2_CharacterName4.Length)
        {
            mytext2.text = R2_CharacterName4[currentTextIndex7]; // ������ �κ�
            if (mytext2.text == "����")
            {
                StartCoroutine(FadeIn());
            }
            else
            {
                StartCoroutine(FadeOut());
            }
        }
        else if (currentTextIndex8 < R2_CharacterName5.Length)
        {
            mytext2.text = R2_CharacterName5[currentTextIndex8];
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

    public void Result1_1()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 0)
        {
            currentTextIndex3++;
            if (currentTextIndex3 < result1_1.Length)
            {
                UpdateText(result1_1);
            }
            if (currentTextIndex3 >= result1_1.Length)
            {
                transparency.SetActive(true);
                isWaitingForInput = false;
                select4.SetActive(true);
            }

        }
    }

    public void Result1_2()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 0)
        {
            currentTextIndex9++;
            if (currentTextIndex9 < result1_2.Length)
            {
                UpdateText(result1_2);
            }
            if (currentTextIndex9 >= result1_2.Length)
            {
                transparency.SetActive(true);
                isWaitingForInput = false;
            }

        }
    }

    public void Result2_1()
    {
        bifurcation = 1;

        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 1)
        {
            currentTextIndex4++;
            if (currentTextIndex4 < result2_1.Length)
            {
                UpdateText(result2_1);
                UpdateText2(R2_CharacterName1);
            }
            if (currentTextIndex4 >= result2_1.Length)
            {
                transparency.SetActive(true);
                isWaitingForInput = false;
                select2_1.SetActive(true);
            }
        }
    }
    public void Result2_2()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 1)
        {
            currentTextIndex5++;
            if (currentTextIndex5 < result2_2.Length)
            {
                UpdateText(result2_2);
                UpdateText2(R2_CharacterName2);
            }
            if (currentTextIndex5 >= result2_2.Length)
            {
                // ���⼭ isWaitingForInput�� false�� ����
                isWaitingForInput = false;
                select2_2.SetActive(true);
                transparency.SetActive(true);
            }
        }
    }

    public void Result2_3()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 1)
        {
            currentTextIndex6++;
            if (currentTextIndex6 < result2_3.Length)
            {
                UpdateText(result2_3);
                UpdateText2(R2_CharacterName3);
            }
            if (currentTextIndex6 >= result2_3.Length)
            {
                select2_3.SetActive(true);
                transparency.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }
    public void Result2_4()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 1)
        {
            currentTextIndex7++;
            if (currentTextIndex7 < result2_4.Length)
            {
                UpdateText(result2_4);
                UpdateText2(R2_CharacterName4);  // ������ �κ�
            }
            if (currentTextIndex7 >= result2_4.Length)
            {
                select2_4.SetActive(true);
                transparency.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }
    public void Result2_5()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 1)
        {
            currentTextIndex8++;
            if (currentTextIndex8 < result2_5.Length)
            {
                UpdateText(result2_5);
                UpdateText2(R2_CharacterName5);
            }
            if (currentTextIndex8 >= result2_5.Length)
            {
                transparency.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }

    IEnumerator FadeOut()
    {
        float targetAlpha = 0f;

        while (canvasImage.color.a > targetAlpha)
        {
            float currentAlpha = canvasImage.color.a;
            currentAlpha -= fadeSpeed * Time.deltaTime;
            canvasImage.color = new Color(canvasImage.color.r, canvasImage.color.g, canvasImage.color.b, Mathf.Max(currentAlpha, targetAlpha));

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
