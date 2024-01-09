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
    public GameObject fixincounter0, fixincounter1, fixincounter2, fixincounter3_1, fixincounter3_2;
    [SerializeField]
    GameObject blockUI;
    [SerializeField]
    Button nextBtn;

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
            FixIncounter();
            GameManager.cardCanvasOn = true;
            blockUI.SetActive(true);
            nextBtn.interactable = false;
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
        //if (count == 4) //�������� ����
        //{
        //    fixincounter0.SetActive(true);
        //}

        /*else */if (count == 23) //5���� ����
        {
            fixincounter1.SetActive(true);
        }

        else if (count == 43) //10���� ����
        {
            fixincounter2.SetActive(true);
        }

        else if (count == 63 && Event.Quest == 0)
        {
            fixincounter3_1.SetActive(true);
        }

        else if (count == 63 && Event.Quest == 1)
        {
            fixincounter3_2.SetActive(true);
        }
        else
        {
            RandomEvent.SpawnPlay();
        }
    }
}