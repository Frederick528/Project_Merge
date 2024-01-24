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
    private string[] textArray1 = { "숲 깊숙한 곳에서 아름다운 작은 연못을 발견합니다.",
                                    "연못을 들여다 보자 그 안에서 물의 정령이 나타납니다.",
                                    "\"환영한다, 모험자여. 숲의 길을 걷는 너를 만나게 되어 기쁘구나.\"" };

    private string[] textArray2 = {"",
                                    "\"이 곳은 숲의 마력이 깃든 곳이다. 네가 이곳을 찾게 된 이유는 무엇이냐?\""};

    private string[] textArray3 = { "",
                                    "\"숲은 생명이 숨쉬고, 각 나무와 동물은 숨은 이야기를 가지고 있지.\"",
                                    "\"너가 그것을 듣고 싶으면, 숲의 속삭임을 경청하거라.\"",
                                    "숲의 속삭임을 듣다니, 어떻게 할 수 있을까 생각했다.",
                                    "\"손을 물에 담가보라. 그리고 마음을 비워봐. 그렇게 하면 숲의 목소리가 들리게 될 것이다.\"",
                                    "물의 정령은 내 생각을 알아차린듯 말을 했다. ",
                                    "물에 손을 넣자 동물과 숲의 소리가 들린다."};




    private string[] CharacterName1 = { "","","물의 정령" };
    private string[] CharacterName2 = { "", "물의 정령" };
    private string[] CharacterName3 = { "", "물의 정령", "물의 정령","", "물의 정령","","" };

    private string[] R_CharacterName1 = { "", "", "", "", "물의 정령", "" };
    private string[] R_CharacterName2 = { "", "", "", "", "물의 정령", "" };

    private string[] result1 = { "", "나는 동물의 소리에 경청했다.",
                                     "동물은 계속해서 이야기를 하는것 같다.",
                                     "하지만 나는 그것을 이해할 수 없었다..",
                                     "\"숲의 속삭임의 귀를 기울이라 말했을 터인데\"",
                                     "물의 정령은 나의 행동이 언짢은것 같다.",};
  
    private string[] result2 = { "", "나는 숲의 소리에 경청했다.",
                                     "숲의 소리는 나에게 편안함을 가져다 주었다.",
                                     "숲의 소리가 더 이상 들리지 않아 눈을 떴다.",
                                     "\"숲의 이야기를 들어준 모양이구나\"",
                                     "물의 정령은 나의 행동에 기분이 좋아진 것 같다",};


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
        // 배열 길이 확인 후 업데이트
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
        // 배열 길이 확인 후 업데이트
        if (currentTextIndex < CharacterName1.Length && isWaitingForInput)
        {
            mytext2.text = CharacterName1[currentTextIndex];
            if (mytext2.text == "물의 정령")
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
            if (mytext2.text == "물의 정령")
            {
                StartCoroutine(FadeIn());
            }
           
        }

        else if (currentTextIndex5 < R_CharacterName2.Length && isWaitingForInput)
        {
            mytext2.text = R_CharacterName2[currentTextIndex5];
            if (mytext2.text == "물의 정령")
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
                //갈증 10 감소
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
                //갈증 10 증가
            }
        }
    }
    IEnumerator FadeOut()
    {
        isFading = true;  // Fade 시작 시에 isFading을 true로 설정

        float targetAlpha = 0f;

        while (canvasImage.color.a > targetAlpha)
        {
            float currentAlpha = canvasImage.color.a;
            currentAlpha -= fadeSpeed * Time.deltaTime;
            canvasImage.color = new Color(canvasImage.color.r, canvasImage.color.g, canvasImage.color.b, currentAlpha);

            yield return null;
        }

        isFading = false;  // Fade 종료 시에 isFading을 false로 설정

        // Coroutine이 끝날 때까지 기다린 후 텍스트 업데이트
        UpdateText2(R_CharacterName1);

        // Coroutine이 완료되면 다음 입력 받을 수 있도록 설정
        isWaitingForInput = true;
    }

    // ...

    IEnumerator FadeIn()
    {
        isFading = true;  // Fade 시작 시에 isFading을 true로 설정

        float targetAlpha = 1f;

        while (canvasImage.color.a < targetAlpha)
        {
            float currentAlpha = canvasImage.color.a;
            currentAlpha += fadeSpeed * Time.deltaTime;
            canvasImage.color = new Color(canvasImage.color.r, canvasImage.color.g, canvasImage.color.b, currentAlpha);

            yield return null;
        }

        isFading = false;  // Fade 종료 시에 isFading을 false로 설정

        // Coroutine이 끝날 때까지 기다린 후 텍스트 업데이트
        UpdateText2(R_CharacterName1);

        // Coroutine이 완료되면 다음 입력 받을 수 있도록 설정
        isWaitingForInput = true;

        // 투명도가 1 이상으로 올라갔을 때의 처리
        Debug.Log("Image faded in completely.");
    }
}