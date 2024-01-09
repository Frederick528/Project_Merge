using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{
    public Text Day;
    public Text Time;
    public static int count = 4; // static으로 변경
    public static int maxDays = 31; // static으로 변경
    private static RandomEvent randomEvent; // static으로 변경
    public GameObject closebtn;
    public GameObject fixincounter0, fixincounter1, fixincounter2, fixincounter3;

    private void Update()
    {
        DayText();
        FixIncounter(); 
    }

    public void NextTurn()
    {
        count += 1;

        if (count > maxDays * 4)
        {
            count = 4;
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
            RandomEvent.SpawnPlay();
            closebtn.SetActive(true);
        }
    }

    private void DayText()
    {
        if (count % 4 == 0)
        {
            Day.text = "생존 " + (count / 4).ToString() + "일차";
        }
    }

    void FixIncounter()
    {
        if (count == 4) //시작하자 마자
        {
            fixincounter0.SetActive(true);
        }

        if (count == 23) //5일차 새벽
        {
            fixincounter1.SetActive(true);
        }

        if (count == 43) //10일차 새벽
        {
            fixincounter2.SetActive(true);
        }

        if (count == 63)
        {
            fixincounter3.SetActive(true);
        }
    }
}