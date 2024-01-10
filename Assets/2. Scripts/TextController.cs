using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public Text myText;
    public GameObject Select;
    private int currentTextIndex = 0;
    private string[] textArray1 = { "당신은 이곳이 어디인지도 모른체 숲속에 조난 당했습니다.",
                                   "여긴 어디지 라는 생각도 잠시, 숲속 사이에서 긴 모자를 쓰고 있는 여성이 나타났습니다.",
                                   "“사람인가? 어떻게 살아있는지 모르겠네. 일단 반가워. 나는 마녀야.",
                                   "당신은 마녀가 어떻게 존재하냐고 물어보고 싶었지만 마녀는 계속해서 말을 이어갔습니다.",
                                   "“이건 선물이야. 나는 착한 마녀니까. 30일 뒤에 네가 살아있다면 상관하지 않겠다만, 그 전에 네가 죽어있다면 네 시체는 내가 잘 써주겠어”",
                                   "그녀는 선물이라며 황금빛 사과를 거내며 자리를 떠납니다.",
                                   "죽은 뒤에 찾아오는게 무슨 상관일까요? 마침 굶어 죽을 것 같은데.. 이 사과를 먹어도 될까요?"
                                   };
    private string[] textArray2 = { " 그 즉시 배고픔 수치와 갈증 수치가 100까지 찬다.", };
    private string[] textArray3 = { "황금 사과를 카드로 획득할 수 있다. * 황금사과 - 유통기한 없음 , 먹으면 스탯을 모두 꽉 찬다.", };

    void Start()
    {
        myText.text = textArray1[0];
    }

    void Update()
    {
        Incounter1();
    }

    void UpdateText(string[] textArray)
    {
        // 배열 길이 확인 후 업데이트
        if (currentTextIndex < textArray.Length)
        {
            myText.text = textArray[currentTextIndex];
        }
        //else
        //{
        //    Debug.Log("더 이상 텍스트가 없어요!");
        //}
    }

    void Incounter1()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentTextIndex++;
            if (currentTextIndex < textArray1.Length)
            {
                print($"길이: {textArray1.Length}");
                print(currentTextIndex);
                UpdateText(textArray1);
            }
            else
            {
                ShowSelect();
                return;
            }
        }
    }

    public void IncounterSelect1()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentTextIndex < textArray2.Length)
            {
                if (currentTextIndex < textArray2.Length)
                {
                    UpdateText(textArray2);
                }
            }
            else
            {
                Debug.Log("더 이상 텍스트가 없어요!");
            }
        }
    }

    public void IncounterSelect2()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentTextIndex < textArray3.Length)
            {
                if (currentTextIndex < textArray3.Length)
                {
                    UpdateText(textArray3);
                }
            }
            else
            {
                Debug.Log("더 이상 텍스트가 없어요!");
            }
        }
    }
    void ShowSelect()
    {
        // 대화가 끝났을 때 Select 게임 오브젝트를 보이도록 설정
        Select.SetActive(true);
    }
}


