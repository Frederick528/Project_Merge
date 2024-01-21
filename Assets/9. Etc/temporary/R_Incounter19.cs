using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_Incounter19 : MonoBehaviour
{
    public bool isWaitingForInput = true;
    public int bifurcation = 0;
    public Text myText;
    public GameObject incounter, transparency, select1;

    [SerializeField]
    Button nextBtn;
    [SerializeField]
    GameObject blockUI, closeBtn;

    int currentTextIndex, currentTextIndex2, currentTextIndex3 = 0;
    private string[] textArray1 = {
        "어디선가 음악소리가 들린다.",
        "듣고있자니 편안해진다.",
        "가까이서 듣고싶은데.. 위험하지 않을까.",
        "여러가지 마녀의 장난이라던가, 사이렌 같은건 아닐까?"
    };

    private string[] result1 = {
        "",
        "나는 노래가 들리는곳으로 향했다.",
        "그곳에선 ",
        "그 식물들은 나를 발견하곤, 마치 여기에 앉으라는 식으로 자리를 안내했다.",
        "정말 좋은 공연이었다."
    };

    private string[] result2 = {
        "",
        "위험할 수도 있으니 가지 않는 게 좋겠어.",
        "나는 멀리서 가만히 노래를 듣기만 했다.",
    };

    void OnEnable()
    {
        SoundManager.instance.Play("Sounds/Bgm/StoryBgm", Sound.Bgm, 0.2f);
        myText.text = textArray1[0];
        Turn.Instance.nextBtn.interactable = false;
        Turn.Instance.closeBtn.SetActive(true);
        isWaitingForInput = true;
        bifurcation = 0;
        (currentTextIndex, currentTextIndex2, currentTextIndex3) = (0, 0, 0);
    }

    void Update()
    {
        if (currentTextIndex < textArray1.Length)
        {
            Incounter1();
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex2 < result1.Length && bifurcation == 0)
        {
            Result1();
        }
        else if (currentTextIndex >= textArray1.Length && currentTextIndex3 < result2.Length && bifurcation == 1)
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
            SoundManager.instance.Play("Sounds/Bgm/GameBgm", Sound.Bgm, 0.3f);
        }
    }

    void UpdateText(string[] textArray)
    {
        if (currentTextIndex < textArray1.Length && isWaitingForInput)
        {
            myText.text = textArray1[currentTextIndex];
        }
        else if (currentTextIndex2 < result1.Length && isWaitingForInput && bifurcation == 0)
        {
            myText.text = result1[currentTextIndex2];
        }
        else if (currentTextIndex3 < result2.Length && isWaitingForInput && bifurcation == 1)
        {
            myText.text = result2[currentTextIndex3];
        }
        else
        {
            Debug.Log("Not Text");
        }
    }

    void Incounter1()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true)
        {
            currentTextIndex++;
            if (currentTextIndex < textArray1.Length)
            {
                UpdateText(textArray1);
            }
            if (currentTextIndex >= textArray1.Length)
            {
                transparency.SetActive(true);
                select1.SetActive(true);
                isWaitingForInput = false;
            }
        }
    }

    public void Result1()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 0)
        {
            currentTextIndex2++;
            if (currentTextIndex2 < result1.Length)
            {
                UpdateText(result1);
            }
            if (currentTextIndex2 >= result1.Length)
            {
                transparency.SetActive(true);
                isWaitingForInput = false;
                CoreController.HungerStatChange(5);
                CoreController.ThirstStatChange(5);
                //갈증 5, 허기 5 증가
            }
        }
    }

    public void Result2()
    {
        bifurcation = 1;

        if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 1)
        {
            currentTextIndex3++;
            if (currentTextIndex3 < result2.Length)
            {
                UpdateText(result2);
            }
            if (currentTextIndex3 >= result2.Length)
            {
                transparency.SetActive(true);
                isWaitingForInput = false;
                //효과 없음
            }
        }
    }
}
