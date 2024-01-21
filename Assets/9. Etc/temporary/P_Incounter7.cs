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
    public float fadeSpeed = 0.5f; // 투명도가 줄어드는 속도

    [SerializeField]
    Button nextBtn;
    [SerializeField]
    GameObject blockUI, closeBtn;

    int currentTextIndex, currentTextIndex1, currentTextIndex2, currentTextIndex3, currentTextIndex4, currentTextIndex5, currentTextIndex6, currentTextIndex7, currentTextIndex8, currentTextIndex9 = 0;
    private string[] textArray1 = { "\"드디어 30일이네.\"" };
    private string[] textArray2 = { "", "\"준비는 됐어?\"" };
    private string[] textArray3 = { "",
                                    "그녀는 나에게 고민할게 무엇이 있냐며 한숨을 쉬었다.",
                                    "\"일단 알았어 기다려줄게.\"",
                                    "그녀는 기다려주기로 했다.",
                                    "친절하게도."};

    private string[] CharacterName1 = { "마녀" };
    private string[] CharacterName2 = { "", "마녀" };
    private string[] CharacterName3 = { "", "", "마녀", "", "" };



    private string[] result1_1 = { "",
                                 "나는 빠르게 숲을 떠나갔다.",
                                 "더 이상은 날 막을 수 없을 것이다.",
                                 "천천히 숲에서 멀리까지 나아가기 시작했다.",
                                 "하지만…"
    };
    private string[] result1_2 = { "",
                                 "무언가 기억이 돌아온다..",
                                 "여태 힘들었던 기억",
                                 "포기하고 싶었던 기억이 날 괴롭혔다.",
                                 "기억났다.",
                                 "나는 이곳에서 기억을 잊고 치료받기 위해서 나왔다.",
                                 "..그래서 마녀가 날 죽이지 않은건가..",
                                 "나는..",
                                 "숲에서 벗어났지만…",
                                 "더 이상 돌아갈 수 없었다.",
                                 "내 뒤에 있던 숲은 더 이상 보이지 않았고..",
                                 "그 이후의 미래는 뻔했다.",
                                 "-BAD ENDING-"
    };
    private string[] result2_1 = { "", "나는 그녀와 함께 숲에 남기를 청했다.",
                                       "\"그게 무슨말이야?\"",
                                       "\"여기에서 나가고 싶던게 아니었어?\"" };
    private string[] result2_2 = { "", "무언가 걸리는게 있었다.",
                                       "아니 이게 맞는 것 같다.",
                                       "무언가 나는.. 내가 원해서 숲에 들어온게 아닌가 싶다.",
                                       "모종의 이유로 기억을 잃고.. 치유받는 그런 느낌.."};
    private string[] result2_3 = { "", "\"뭐, 다행이네. 그래도 너가 좀 편했다는게.",
                                       "\"이제 진실을 말해줄게.\"",
                                       "\"너는 살면서 생긴 깊은 마음의 상처로 이곳에 오게 되었어.\""};
    private string[] result2_4 = { "", "기억난다. ",
                                       "나는 한때 회사에서 열정적이었고 성공적인 삶을 살았다고 자부할 수 있었다.",
                                       "하지만 그것도 잠깐일뿐. 사람들은 내 그런 열정에 더 많은 일을 처리시켰다.",
                                       "잔업부터 시작해서 개인적인 일까지. ",
                                       "나는 그때 일에만 미쳐 다른 일은 아무것도 하지 못했다.",
                                       "그렇게 내 인생은 꼬이고 말았다.",
                                       "어느날 한번의 작은 실수가 있었을때.",
                                       "이거 하나 못하냐며로 시작한 구박은 다른 사람도 그렇게 생각하게 만들기 쉬운 말이었다.",
                                       "그 뒤로 얼마나 지났을까?",
                                       "제대로 일을 해도 욕을 먹던 나날에. 너무 끊임없는 스트레스에 나는..",
                                       "\"힘들었겠지. 그러니까 허무맹랑한 이야기인 이 숲에 대한 이야기를 듣고도 찾아왔으니.\"",};
    private string[] result2_5 = { "", "\"내가 할일은 다 끝냈으니. 솔직하게 말해서 나는 좋아. 그렇게 해\". ",
                                       "그렇다. 나는 더 이상 예전 일에 대해 힘들어하지 않는다.",
                                       "굳이 할 필요도 없었다.",
                                       "나는 혼자 살았으니. 딱히 후회는 없다. ",
                                       "이곳에 있으면 나도 다른 이들을 도와줄 수 있을 것이다.",};


    private string[] R2_CharacterName1 = { "", "", "마녀", "마녀" };
    private string[] R2_CharacterName2 = { "", "", "", "", "" };
    private string[] R2_CharacterName3 = { "", "마녀", "마녀", "마녀" };
    private string[] R2_CharacterName4 = { "", "", "","","","","","","","","", "마녀" };
    private string[] R2_CharacterName5 = { "", "마녀", "", "", "", ""};

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

        StartCoroutine(FadeIn()); // 수정된 부분
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
        // 배열 길이 확인 후 업데이트
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
            mytext2.text = R2_CharacterName4[currentTextIndex7]; // 수정된 부분
            if (mytext2.text == "마녀")
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
                // 여기서 isWaitingForInput을 false로 설정
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
                UpdateText2(R2_CharacterName4);  // 수정된 부분
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

        // 투명도가 1 이상으로 올라갔을 때의 처리
        Debug.Log("Image faded in completely.");
    }
}
