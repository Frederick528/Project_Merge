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
    public int maxDays = 31; // 최대 생존 일수

    public void Update()
    {
        DayText();
    }

    public void NextTurn()
    {
        count += 1;

        if (count > maxDays * 4) // 최대 일수를 초과하면 초기화
        {
            count = 1;
        }

        if (count % 4 == 1)
        {
            Img_Renderer.sprite = morning;
            DayText(); // 새벽에 DayText 업데이트
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
            Day.text = "생존 일수: " + (count / 4).ToString(); // 생존 일수 업데이트
        }
    }
}