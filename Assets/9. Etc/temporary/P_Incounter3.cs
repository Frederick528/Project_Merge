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
    public float fadeSpeed = 0.5f; // 투명도가 줄어드는 속도

    public SelectBtn selectbtn;

    [SerializeField]
    Button nextBtn;
    [SerializeField]
    GameObject blockUI, closeBtn;

    int currentTextIndex, currentTextIndex1, currentTextIndex4, currentTextIndex5 = 0;
    private string[] textArray1 = { "이곳에 조난당한지 10일차. ",
                                    "이 시간대면 항상 찾아오는 사람이 하나 있다.",
                                    "안녕? 엇, 이미 올 줄 알고 있었구나",
                                    "나는 그녀가 올 줄 알고 미리 의자를 하나 만들어놨다."};

    private string[] textArray2 = {"",
                                    "\"오늘은 네 도움이 필요해서 찾아왔어.\"",
                                    "마녀는 자신 혼자서 만들기 까다로운 물품을 주문했다.",
                                    "다목적 목공도구로, 자신에게 꼭 필요한 물건이라고 한다..",
                                    "\"만들어주면 또 마법 걸어줄게!! 제발!!\"",
                                    "또 이상한 마법일지도 모른다.."};

    private string[] CharacterName1 = { "", "", "마녀", "" };
    private string[] CharacterName2 = { "", "마녀", "", "", "마녀", "" };
    private string[] R_CharacterName1 = { "", "마녀", "", "" };
    private string[] R_CharacterName2 = { "", "마녀", "", "", "" };

    private string[] result1 = { "", "\"정말로 고마워!! 5일 뒤에 올게!!\"", "그녀는 그렇게 다시 숲으로 들어갔다.", "선금 받을껄 그랬나.." };
    private string[] result2 = { "", "\"그래 나도 더러워서 안받는다!!\"", "마녀는 지팡이를 크게 흔들더니, 이윽고 마법을 썼다.", "그 방향은 내 쪽이었다..", "몸이 엄청나게 무거워졌다..!!" };


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
        // 배열 길이 확인 후 업데이트
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
        // 배열 길이 확인 후 업데이트
        if (currentTextIndex < CharacterName1.Length && isWaitingForInput)
        {
            mytext2.text = CharacterName1[currentTextIndex];
            if (mytext2.text == "마녀" )
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
            if (mytext2.text != "마녀")
            {
                StartCoroutine(FadeOut());
            }

        }
       
        else if (currentTextIndex5 < R_CharacterName2.Length && isWaitingForInput && bifurcation == 1)
        {
            mytext2.text = R_CharacterName2[currentTextIndex5];
            if (mytext2.text != "마녀")
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
                // 허기 갈증 50% 감소
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

        // 투명도가 1 이상으로 올라갔을 때의 처리
        Debug.Log("Image faded in completely.");
    }
}