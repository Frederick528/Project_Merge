using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{
    public Text Day;
    public Text Time;
    public static int count = 4; // static���� ����
    public static int maxDays = 31; // static���� ����
    private static RandomEvent randomEvent; // static���� ����
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
            Time.text = "��ħ".ToString();
            DayText();
        }
        else if (count % 4 == 1)
        {
            Time.text = "����".ToString();
        }
        else if (count % 4 == 2)
        {
            Time.text = "����".ToString();
        }
        else if (count % 4 == 3)
        {
            Time.text = "����".ToString();
            RandomEvent.SpawnPlay();
            closebtn.SetActive(true);
        }
    }

    private void DayText()
    {
        if (count % 4 == 0)
        {
            Day.text = "���� " + (count / 4).ToString() + "����";
        }
    }

    void FixIncounter()
    {
        if (count == 4) //�������� ����
        {
            fixincounter0.SetActive(true);
        }

        if (count == 23) //5���� ����
        {
            fixincounter1.SetActive(true);
        }

        if (count == 43) //10���� ����
        {
            fixincounter2.SetActive(true);
        }

        if (count == 63)
        {
            fixincounter3.SetActive(true);
        }
    }
}