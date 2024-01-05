using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{
    public Text Day;
    public SpriteRenderer Img_Renderer;
    public Sprite morning, lunch, dinner, night;
    public int count = 4;
    public int maxDays = 31; // �ִ� ���� �ϼ�

    public void Update()
    {
        DayText();
    }

    public void NextTurn()
    {
        count += 1;

        if (count > maxDays * 4) // �ִ� �ϼ��� �ʰ��ϸ� �ʱ�ȭ
        {
            count = 1;
        }

        if (count % 4 == 1)
        {
            Img_Renderer.sprite = morning;
            DayText(); // ������ DayText ������Ʈ
        }
        else if (count % 4 == 2)
        {
            Img_Renderer.sprite = lunch;
        }
        else if (count % 4 == 3)
        {
            Img_Renderer.sprite = dinner;
        }
        else if (count % 4 == 0)
        {
            Img_Renderer.sprite = night;
        }
    }

    void DayText()
    {
        if (count % 4 == 1)
        {
            Day.text = "���� �ϼ�: " + (count / 4).ToString(); // ���� �ϼ� ������Ʈ
        }
    }
}