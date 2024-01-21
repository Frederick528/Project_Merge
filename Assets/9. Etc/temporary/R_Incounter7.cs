using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_Incounter7 : MonoBehaviour
{
    public bool isWaitingForInput = true;
    public int bifurcation = 0;
    public Text myText, mytext2;
    public GameObject incounter, transparency, Character, select1, select2, select3;
    public Image canvasImage;
    public float fadeSpeed = 0.8f;

    [SerializeField]
    Button nextBtn;
    [SerializeField]
    GameObject blockUI, closeBtn;

    private bool isFading = false;

    int currentTextIndex, currentTextIndex1, currentTextIndex2, currentTextIndex4, currentTextIndex5 = 0;
    private string[] textArray1 = { "�� ����� ������ �Ƹ��ٿ� ���� ������ �߰��մϴ�.",
                                    "������ �鿩�� ���� �� �ȿ��� ���� ������ ��Ÿ���ϴ�.",
                                    "\"ȯ���Ѵ�, �����ڿ�. ���� ���� �ȴ� �ʸ� ������ �Ǿ� ��ڱ���.\"" };

    private string[] textArray2 = {"",
                                    "\"�� ���� ���� ������ ��� ���̴�. �װ� �̰��� ã�� �� ������ �����̳�?\""};

    private string[] textArray3 = { "",
                                    "\"���� ������ ������, �� ������ ������ ���� �̾߱⸦ ������ ����.\"",
                                    "\"�ʰ� �װ��� ��� ������, ���� �ӻ����� ��û�ϰŶ�.\"",
                                    "���� �ӻ����� ��ٴ�, ��� �� �� ������ �����ߴ�.",
                                    "\"���� ���� �㰡����. �׸��� ������ �����. �׷��� �ϸ� ���� ��Ҹ��� �鸮�� �� ���̴�.\"",
                                    "���� ������ �� ������ �˾������� ���� �ߴ�. ",
                                    "���� ���� ���� ������ ���� �Ҹ��� �鸰��."};




    private string[] CharacterName1 = { "","","���� ����" };
    private string[] CharacterName2 = { "", "���� ����" };
    private string[] CharacterName3 = { "", "���� ����", "���� ����","", "���� ����","","" };

    private string[] R_CharacterName1 = { "", "", "", "", "���� ����", "" };
    private string[] R_CharacterName2 = { "", "", "", "", "���� ����", "" };

    private string[] result1 = { "", "���� ������ �Ҹ��� ��û�ߴ�.",
                                     "������ ����ؼ� �̾߱⸦ �ϴ°� ����.",
                                     "������ ���� �װ��� ������ �� ������..",
                                     "\"���� �ӻ����� �͸� ����̶� ������ ���ε�\"",
                                     "���� ������ ���� �ൿ�� ��¨���� ����.",};
  
    private string[] result2 = { "", "���� ���� �Ҹ��� ��û�ߴ�.",
                                     "���� �Ҹ��� ������ ������� ������ �־���.",
                                     "���� �Ҹ��� �� �̻� �鸮�� �ʾ� ���� ����.",
                                     "\"���� �̾߱⸦ ����� ����̱���\"",
                                     "���� ������ ���� �ൿ�� ����� ������ �� ����",};


    void OnEnable()
    {
        SoundManager.instance.Play("Sounds/Bgm/StoryBgm", Sound.Bgm, 0.2f);
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
        isWaitingForInput = true;
        bifurcation = 0;
        (currentTextIndex, currentTextIndex1, currentTextIndex2, currentTextIndex4, currentTextIndex5) = (0, 0, 0, 0, 0);
    }

    void Update()
    {
        if (!isFading)
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
            else if (currentTextIndex >= textArray1.Length && currentTextIndex1 >= textArray2.Length && currentTextIndex2 >= textArray3.Length
                     && currentTextIndex4 < result1.Length && bifurcation == 0)
            {
                Result1();
            }
            else if (currentTextIndex >= textArray1.Length && currentTextIndex1 >= textArray2.Length && currentTextIndex2 >= textArray3.Length
                    && currentTextIndex5 < result2.Length && bifurcation == 1)
            {
                Result2();
            }
            else
            {
                incounter.SetActive(false);
                Turn.Instance.closeBtn.SetActive(false);
                Turn.Instance.nextBtn.interactable = true;
                Turn.Instance.blockUI.SetActive(false);
                GameManager.CardCanvasOn = false;
                SoundManager.instance.Play("Sounds/Bgm/GameBgm", Sound.Bgm, 0.3f);
            }
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
            if (mytext2.text == "���� ����")
            {
                StartCoroutine(FadeIn());
            }

        }
        else if (currentTextIndex1 < CharacterName2.Length && isWaitingForInput)
        {
            mytext2.text = CharacterName2[currentTextIndex1];
        }
        else if (currentTextIndex2 < CharacterName3.Length && isWaitingForInput)
        {
            mytext2.text = CharacterName3[currentTextIndex2];
        }

        else if (currentTextIndex4 < R_CharacterName1.Length && isWaitingForInput)
        {
            mytext2.text = R_CharacterName1[currentTextIndex4];
            if (mytext2.text == "���� ����")
            {
                StartCoroutine(FadeIn());
            }
           
        }

        else if (currentTextIndex5 < R_CharacterName2.Length && isWaitingForInput)
        {
            mytext2.text = R_CharacterName2[currentTextIndex5];
            if (mytext2.text == "���� ����")
            {
                StartCoroutine(FadeIn());
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
                StartCoroutine(FadeOut());
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
                CoreController.ThirstStatChange(-10);
                //���� 10 ����
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
                CoreController.ThirstStatChange(10);
                //���� 10 ����
            }
        }
    }
    IEnumerator FadeOut()
    {
        isFading = true;  // Fade ���� �ÿ� isFading�� true�� ����

        float targetAlpha = 0f;

        while (canvasImage.color.a > targetAlpha)
        {
            float currentAlpha = canvasImage.color.a;
            currentAlpha -= fadeSpeed * Time.deltaTime;
            canvasImage.color = new Color(canvasImage.color.r, canvasImage.color.g, canvasImage.color.b, currentAlpha);

            yield return null;
        }

        isFading = false;  // Fade ���� �ÿ� isFading�� false�� ����

        // Coroutine�� ���� ������ ��ٸ� �� �ؽ�Ʈ ������Ʈ
        UpdateText2(R_CharacterName1);

        // Coroutine�� �Ϸ�Ǹ� ���� �Է� ���� �� �ֵ��� ����
        isWaitingForInput = true;
    }

    // ...

    IEnumerator FadeIn()
    {
        isFading = true;  // Fade ���� �ÿ� isFading�� true�� ����

        float targetAlpha = 1f;

        while (canvasImage.color.a < targetAlpha)
        {
            float currentAlpha = canvasImage.color.a;
            currentAlpha += fadeSpeed * Time.deltaTime;
            canvasImage.color = new Color(canvasImage.color.r, canvasImage.color.g, canvasImage.color.b, currentAlpha);

            yield return null;
        }

        isFading = false;  // Fade ���� �ÿ� isFading�� false�� ����

        // Coroutine�� ���� ������ ��ٸ� �� �ؽ�Ʈ ������Ʈ
        UpdateText2(R_CharacterName1);

        // Coroutine�� �Ϸ�Ǹ� ���� �Է� ���� �� �ֵ��� ����
        isWaitingForInput = true;

        // ������ 1 �̻����� �ö��� ���� ó��
        Debug.Log("Image faded in completely.");
    }
}