using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBtn : MonoBehaviour
{
    public TextController textController; // TextController 스크립트 참조
    public R_Incounter1 incounter1;
    public R_Incounter2 incounter2;
    public R_Incounter3 incounter3;
    public R_Incounter4 incounter4; 
    public GameObject selectBtn;
    public Quest quest;


    public void Select1()
    {
        textController.transparency.SetActive(false);
        textController.Select1(); // TextController의 Select1 메서드 호출
        textController.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }

    public void Select2()
    {
        textController.transparency.SetActive(false);
        textController.Select2();
        textController.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }

    public void Select3()
    {
        textController.transparency.SetActive(false);
        textController.Select3();
        textController.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }

    public void Result1()
    {
        textController.transparency.SetActive(false);
        textController.Result1();
        textController.isWaitingForInput = true;
        textController.bifurcation = 0;
        selectBtn.SetActive(false);
    }

    public void Result2()
    {
        textController.transparency.SetActive(false);
        textController.Result2();
        textController.isWaitingForInput = true;
        textController.bifurcation = 1;
        selectBtn.SetActive(false); ;
    }

    public void R1_Result1()
    {
        incounter1.transparency.SetActive(false);
        incounter1.Result1();
        incounter1.isWaitingForInput = true;
        incounter1.bifurcation = 0;
        selectBtn.SetActive(false);
    }
    public void R1_Result2()
    {
        incounter1.transparency.SetActive(false);
        incounter1.Result2();
        incounter1.isWaitingForInput = true;
        incounter1.bifurcation = 1;
        selectBtn.SetActive(false);
    }

    public void R2_Result1()
    {
        incounter2.Result1();
        incounter2.isWaitingForInput = true;
        incounter2.bifurcation = 0;
        selectBtn.SetActive(false);
    }
    public void R2_Result2()
    {
        incounter2.Result2();
        incounter2.isWaitingForInput = true;
        incounter2.bifurcation = 1;
        selectBtn.SetActive(false);
    }

    public void R2_Select1()
    {
        incounter2.Select2(); // TextController의 Select1 메서드 호출
        incounter2.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }

    public void R2_Select2()
    {
        incounter2.Select3(); // TextController의 Select1 메서드 호출
        incounter2.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }

    public void R3_Result1()
    {
        incounter3.Result1();
        incounter3.isWaitingForInput = true;
        incounter3.bifurcation = 0;
        selectBtn.SetActive(false);
    }

    public void R3_Result2()
    {
        incounter3.Result2();
        incounter3.isWaitingForInput = true;
        incounter3.bifurcation = 1;
        selectBtn.SetActive(false);
    }

    public void R3_Select1()
    {
        incounter3.Select1();
        incounter3.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }

    public void R4_Select()
    {
        incounter4.Incounter2(); // TextController의 Select1 메서드 호출
        incounter4.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }
    public void R4_Result1()
    {
        incounter4.Result1();
        incounter4.isWaitingForInput = true;
        incounter4.bifurcation = 0;
        selectBtn.SetActive(false);
    }

    public void R4_Result2()
    {
        incounter4.Result2(); // TextController의 Select1 메서드 호출
        incounter4.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }

    public void Guied1()
    {
        quest.Click1();
    }
    public void Guied2()
    {
        quest.Click2();
    }
    public void Guied3()
    {
        quest.Click3();
    }
    public void Guied4()
    {
        quest.Click4();
    }
    public void Guied5()
    {
        quest.Click5();
    }

    public void Guied6()
    {
        quest.Click6();
    }

    public void Guied7()
    {
        quest.Click7();
    }
}
