using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_Incounter14 : MonoBehaviour
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
        "숲속을 수색하던 중, 바닥에서 무언가를 주웠다.",
        "되게 영롱하게 생긴것이… ",
        "마녀가 쓰던 물건인 것 같다."
    };

    private string[] result1 = {
        "",
        "나는 이 물건을 들어 마녀에게 돌려주었다.",
        "마녀는 잠시 그 물건을 보더니, 잠시 손으로 만지더니..",
        "나는 이거 가지고 충분히 놀았어. 라며 나에게 주었다.",
        "무언가 몸에 힘이 도는 것 같다..!"
    };

    private string[] result2 = {
        "",
        "나는 물건을 들어 대충 활성화를 시도해보았다.",
        "이게… 이건가?",
        "!!!",
        "물건이 과부화가 되는 것 같다…!",
        "따갑다! "
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
                //아티펙트 무작위 1개 획득
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
                CoreController.HungerStatChange(-10);
                //허기 10 감소
            }
        }
    }
}
