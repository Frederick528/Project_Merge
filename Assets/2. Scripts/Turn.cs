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

    private void Update()
    {
        DayText();
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
        }
    }

    private void DayText()
    {
        if (count % 4 == 0)
        {
            Day.text = "���� �ϼ�: " + (count / 4).ToString();
        }
    }
}