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
    public float fadeSpeed = 0.5f; // 투명도가 줄어드는 속도

    [SerializeField]
    Button nextBtn;
    [SerializeField]
    GameObject blockUI, closeBtn;

    int currentTextIndex, currentTextIndex1, currentTextIndex2, currentTextIndex3, currentTextIndex4, currentTextIndex5, currentTextIndex6 = 0;
    private string[] textArray1 = { "오늘은 마녀가 찾아오는 날이다.",
                                    "그러나, 갑자기 문득 궁금한 점이 생겼다."};

    private string[] textArray2 = {"",
                                    "나는 여기에 조난당했다. ",
                                    "분명히 그랬었다.",
                                    "하지만 내가 어디서 왔는지, 어떻게 된건지에 대한 기억이 없다.",
                                    "마치 원래 없던 것 처럼",
                                    "머리가 아파온다.."};

    private string[] textArray3 = { "",
                                    "\"뭘 어떻게 해?\"",
                                    "숲에서 마녀가 나왔다. ",
                                    "\"무슨 하고싶은 말이라도 있는거야?\"",
                                    "할말은 많다. 하지만 무언가 돌이킬 수 없는 강일지도 모른다.."};

    private string[] textArray4 = { "",
                                    "\"아 맞다 설명을 안했네.\"",
                                    "\"이곳은 망각의 숲이야. 이곳에서 보내면 보낼수록 기억을 잃지.\"" };

    private string[] textArray5 = { "",
                                    "\"나는 마법이 있잖아.\""};

    private string[] textArray6 = { "",
                                    "\"물론! 걸고는 있지. 조금 걸릴뿐이야.\"",
                                    "\"한.. 30일정도?\"" };

    private string[] textArray7 = { "",
                                    "\"그러니까 말이야..\"",
                                    "아무튼 새로운 비밀을 알게된 밤이었다." };




    //private string[] CharacterName1 = { "","" };
   // private string[] CharacterName2 = { "", "", "", "", "","","" };
    private string[] CharacterName3 = { "", "마녀", "","마녀","" };
    private string[] CharacterName4 = { "", "마녀", "마녀" };
    private string[] CharacterName5 = { "", "마녀"};
    private string[] CharacterName6 = { "", "마녀", "마녀" };
    private string[] CharacterName7 = { "", "마녀", "" };
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
        // 배열 길이 확인 후 업데이트
        /* if (currentTextIndex < CharacterName1.Length && isWaitingForInput)
         {
             mytext2.text = CharacterName1[currentTextIndex];
             if (mytext2.text == "마녀")
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
            if (mytext2.text == "마녀")
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

        // 투명도가 1 이상으로 올라갔을 때의 처리
        Debug.Log("Image faded in completely.");
    }
}