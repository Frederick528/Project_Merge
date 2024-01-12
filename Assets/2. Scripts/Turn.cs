using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{
    public static Turn Instance;
    public Text Day;
    public Text Time;
    public static int count => CoreController.TurnCnt; // static으로 변경
    public static int maxDays = 31; // static으로 변경

    public static int inCounterNum;

    private static RandomEvent randomEvent; // static으로 변경
    public GameObject closebtn;
    public GameObject fixincounter0, fixincounter1, fixincounter2, fixincounter3_1, fixincounter3_2;

    public GameObject blockUI;

    public Button nextBtn;

    private void Awake()
    {
        Instance ??= this;
    }

    private void Update()
    {
        DayText();
    }

    public void NextTurn()
    {
        if (count > maxDays * 4)
        {
            
        }

        if (count % 4 == 0)
        {
            Time.text = "아침".ToString();
            DayText();
        }
        else if (count % 4 == 1)
        {
            Time.text = "점심".ToString();
        }
        else if (count % 4 == 2)
        {
            Time.text = "저녁".ToString();
        }
        else if (count % 4 == 3)
        {
            Time.text = "새벽".ToString();
        }
    }

    private void DayText()
    {
        if (count % 4 == 0)
        {
            Day.text = "생존 " + ((count / 4)+1) + "일차";
        }
    }

    void FixIncounter()
    {
        //if (count == 4) //시작하자 마자
        //{
        //    fixincounter0.SetActive(true);
        //}

        /*else */if (CoreController.Date == 4) //5일차 새벽
        {
            inCounterNum = 100;
            fixincounter1.SetActive(true);
        }

        else if (CoreController.Date == 9) //10일차 새벽
        {
            inCounterNum = 101;
            fixincounter2.SetActive(true);
        }

        else if (CoreController.Date == 14 && Event.Quest == 0)
        {
            inCounterNum = 102;
            fixincounter3_1.SetActive(true);
        }

        else if (CoreController.Date == 14 && Event.Quest == 1)
        {
            inCounterNum = 103;
            fixincounter3_2.SetActive(true);
        }
        else
        {
            RandomEvent.SpawnPlay();
            //inCounterNum = RandomEvent.
        }
    }

    public IEnumerator Encounter()
    {
        yield return new WaitForSeconds(1f);
        FixIncounter();
        GameManager.CardCanvasOn = true;
        blockUI.SetActive(true);
        nextBtn.interactable = false;
        closebtn.SetActive(true);
    }
}