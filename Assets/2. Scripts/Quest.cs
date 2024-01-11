using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public Text questText;
    public GameObject Open, Close;
    public int count = 0;

    private void Start()
    {
        Open.SetActive(false);
        Close.SetActive(true);
    }
    private void Update()
    {
        Guide();
    }

    void Guide()
    {
        if (count == 1)
        {
            questText.text = "화면에서 카드를 드래그하여 1단계 높은 티어의 카드를 획득 할 수 있습니다.";
        }

        if (count == 2)
        {

            questText.text = "카드 마우스 우클릭하여 카드의 설명을 볼 수 있습니다.";
        }

        if (count == 3)
        {

            questText.text = "2티어 이상의 카드를 우클릭 하여 카드의 정보 화면에서 " +
                             "분해하기 버튼을 누르면 분해한 카드의 1단계 낮은 티어 재료 2개를 랜덤으로 획득합니다";
        }

        if (count == 4)
        {

            questText.text = "음식 카드를 우클릭 하여 카드의 정보 화면에서 " +
                             "먹기 버튼을 누르면 배고픔 수치가 증가합니다.";
        }

        if (count == 5)
        {

            questText.text = "매일 새벽마다 다양한 사건이 일어납니다. " +
                             "플레이어의 선택지에 따라 좋은 일이 일어나거나 나쁜일이 일어날 수 있으니 신중하게 고르시길 바랍니다.";
        }
        if (count == 6)
        {
            questText.text = "카드 뽑기 버튼을 누르면 행동력 1을 소모하고 1티어 재료 카드를 획득 할 수 있습니다." +
                             "행동력이 0이 되면 카드를 뽑을 수 없습니다. ";
        }

        if (count == 7)
        {
            questText.text = "턴 넘기기 버튼을 누르면 순서대로 아침, 점심, 저녁, 새벽 순으로 변경되며 " +
                             "새벽이 지나면 생존 일수가 1일 증가합니다";
        }
    }

    public void Open_Q()
    {
        Open.SetActive(true);
        Close.SetActive(false);
    }

    public void Close_Q()
    {
        Open.SetActive(false);
        Close.SetActive(true);
    }

    public void Click1()
    {
        count = 1;
        Guide();
    }

    public void Click2()
    {
        count = 2;
        Guide();
    }

    public void Click3()
    {
        count = 3;
        Guide();
    }

    public void Click4()
    {
        count = 4;
        Guide();
    }

    public void Click5()
    {
        count = 5;
        Guide();
    }

    public void Click6()
    {
        count = 6;
        Guide();
    }

    public void Click7()
    {
        count = 7;
        Guide();
    }
}
