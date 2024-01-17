using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBtn : MonoBehaviour
{
    public TextController textController; // TextController 스크립트 참조
    public P_Incounter2 p_Incounter2;
    public P_Incounter3 p_Incounter3;
    public P_Incounter4 p_Incounter4;
    public P_Incounter5 p_Incounter5;
    public P_Incounter6 p_Incounter6;
    public P_Incounter7 p_Incounter7;
    public R_Incounter1 incounter1;
    public R_Incounter2 incounter2;
    public R_Incounter3 incounter3;
    public R_Incounter4 incounter4; 
    public R_Incounter5 incounter5;
    public R_Incounter6 incounter6;
    public R_Incounter7 incounter7;
    public R_Incounter8 incounter8;
    public R_Incounter9 incounter9;
    public GameObject selectBtn;
    public Quest quest;

    public bool bifurcation15 = true;


    public void Select1()
    {
        textController.transparency.SetActive(false);
        textController.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }

    public void Select2()
    {
        textController.transparency.SetActive(false);
        textController.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }

    public void Select3()
    {
        textController.transparency.SetActive(false);
        textController.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }

    public void Result1()
    {
        textController.transparency.SetActive(false);
        textController.isWaitingForInput = true;
        textController.bifurcation = 0;
        selectBtn.SetActive(false);
    }

    public void Result2()
    {
        textController.transparency.SetActive(false);
        textController.isWaitingForInput = true;
        textController.bifurcation = 1;
        selectBtn.SetActive(false);
    }

    public void P_Select1() 
    {
        p_Incounter2.transparency.SetActive(false);
        p_Incounter2.Incounter1(); // TextController의 Select1 메서드 호출
        p_Incounter2.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }

    public void P_Select2()
    {
        p_Incounter2.transparency.SetActive(false);
        p_Incounter2.Select1(); // TextController의 Select1 메서드 호출
        p_Incounter2.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }


    public void P_Result1()
    {
        p_Incounter2.transparency.SetActive(false); 
        p_Incounter2.Result1();
        p_Incounter2.isWaitingForInput = true;
        p_Incounter2.bifurcation = 0;
        selectBtn.SetActive(false);
    }
    public void P_Result2()
    {
        p_Incounter2.transparency.SetActive(false);
        p_Incounter2.Result2();
        p_Incounter2.isWaitingForInput = true;
        p_Incounter2.bifurcation = 1;
        selectBtn.SetActive(false);
    }

    public void P_Select2_1()
    {
        p_Incounter3.transparency.SetActive(false);
        p_Incounter3.Incounter1(); 
        p_Incounter3.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }
    public void P_Result2_1()
    {
        p_Incounter3.transparency.SetActive(false);
        p_Incounter3.Result1();
        p_Incounter3.isWaitingForInput = true;
        p_Incounter3.bifurcation = 0;
        selectBtn.SetActive(false);
        //p_Incounter4.bifurcation15 = true;
        bifurcation15 = true;

    }
    public void P_Result2_2()
    {
        p_Incounter3.transparency.SetActive(false);
        p_Incounter3.isWaitingForInput = true;
        p_Incounter3.bifurcation = 1;
        selectBtn.SetActive(false);
        //p_Incounter4.bifurcation15 = false;
        bifurcation15 = false;
    }

    public void P_Select3_1_1()
    {
        p_Incounter4.transparency.SetActive(false);
        p_Incounter4.Incounter1_1();
        p_Incounter4.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }
    public void P_Select3_2_1()
    {
        p_Incounter4.transparency.SetActive(false);
        p_Incounter4.Incounter2_1();
        p_Incounter4.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }
    public void P_Select3_2_2()
    {
        p_Incounter4.transparency.SetActive(false);
        p_Incounter4.Incounter2_2();
        p_Incounter4.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }
    public void P_Result3_1_1()
    {
        p_Incounter4.transparency.SetActive(false);
        p_Incounter4.Result1_1();
        p_Incounter4.isWaitingForInput = true;
        p_Incounter4.bifurcation = 0;
        selectBtn.SetActive(false);
    }
    public void P_Result3_1_2()
    {
        p_Incounter4.transparency.SetActive(false);
        p_Incounter4.Result1_2();
        p_Incounter4.isWaitingForInput = true;
        p_Incounter4.bifurcation = 1;
        selectBtn.SetActive(false);
    }
    public void P_Result3_2_1()
    {
        p_Incounter4.transparency.SetActive(false);
        p_Incounter4.Result2_1();
        p_Incounter4.isWaitingForInput = true;
        p_Incounter4.bifurcation = 0;
        selectBtn.SetActive(false);
    }
    public void P_Result3_2_2()
    {
        p_Incounter4.transparency.SetActive(false);
        p_Incounter4.Result2_2();
        p_Incounter4.isWaitingForInput = true;
        p_Incounter4.bifurcation = 1;
        selectBtn.SetActive(false);
    }
    public void P_Select4_1()
    {
        p_Incounter5.transparency.SetActive(false);
        p_Incounter5.Incounter1(); // TextController의 Select1 메서드 호출
        p_Incounter5.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }

    public void P_Select4_2()
    {
        p_Incounter5.transparency.SetActive(false);
        p_Incounter5.Select1(); // TextController의 Select1 메서드 호출
        p_Incounter5.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }
    public void P_Select4_3()
    {
        p_Incounter5.transparency.SetActive(false);
        p_Incounter5.Select2(); // TextController의 Select1 메서드 호출
        p_Incounter5.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }
    public void P_Select4_4()
    {
        p_Incounter5.transparency.SetActive(false);
        p_Incounter5.Select3(); // TextController의 Select1 메서드 호출
        p_Incounter5.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }
    public void P_Select4_5()
    {
        p_Incounter5.transparency.SetActive(false);
        p_Incounter5.Select4(); // TextController의 Select1 메서드 호출
        p_Incounter5.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }
    public void P_Select4_6()
    {
        p_Incounter5.transparency.SetActive(false);
        p_Incounter5.Select5(); // TextController의 Select1 메서드 호출
        p_Incounter5.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }
    public void P_Select5_1()
    {
        p_Incounter6.Incounter1(); // TextController의 Select1 메서드 호출
        p_Incounter6.isWaitingForInput = true;
        selectBtn.SetActive(false);

    }

    public void P_Result5_1()
    {
        p_Incounter6.Result1();
        p_Incounter6.isWaitingForInput = true;
        p_Incounter6.bifurcation = 0;
        selectBtn.SetActive(false);
    }
    public void P_Result5_2()
    {
        p_Incounter6.Result2();
        p_Incounter6.isWaitingForInput = true;
        p_Incounter6.bifurcation = 1;
        selectBtn.SetActive(false);
    }

    public void P_Select6_1()
    {
        p_Incounter7.transparency.SetActive(false);
        p_Incounter7.Select1(); // TextController의 Select1 메서드 호출
        p_Incounter7.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }

    public void P_Select6_2()
    {
        p_Incounter7.transparency.SetActive(false);
        p_Incounter7.Select2(); // TextController의 Select1 메서드 호출
        p_Incounter7.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }

    public void P_Result6_1()
    {
        p_Incounter7.transparency.SetActive(false);
        //p_Incounter7.Result1();
        p_Incounter7.isWaitingForInput = true;
        p_Incounter7.bifurcation = 0;
        selectBtn.SetActive(false);
    }

    public void P_Result6_2_1()
    {
        p_Incounter7.transparency.SetActive(false);
       // p_Incounter7.Result2_1();
        p_Incounter7.isWaitingForInput = true;
        p_Incounter7.bifurcation = 1;
        selectBtn.SetActive(false);
    }
    public void P_Result6_2_2()
    {
         selectBtn.SetActive(false);
         //p_Incounter7.isWaitingForInput = false;
    }
    public void P_Result6_2_2_1()
    {
        p_Incounter7.transparency.SetActive(false);
        p_Incounter7.isWaitingForInput = true;
        p_Incounter7.bifurcation = 1;
        selectBtn.SetActive(false);
    }
    public void P_Result6_2_3()
    {
        selectBtn.SetActive(false);
    }

    public void P_Result6_2_3_1()
    {
        p_Incounter7.transparency.SetActive(false);
        p_Incounter7.isWaitingForInput = true;
        p_Incounter7.bifurcation = 1;
        selectBtn.SetActive(false);
    }

    public void P_Result6_4()
    {
        p_Incounter7.transparency.SetActive(false);
        p_Incounter7.isWaitingForInput = true;
        p_Incounter7.bifurcation = 1;
        selectBtn.SetActive(false);
    }

    /*public void P_Result6_5()
    {
        p_Incounter7.transparency.SetActive(false);
        p_Incounter7.Result2_5();
        p_Incounter7.isWaitingForInput = true;
        p_Incounter7.bifurcation = 0;
        selectBtn.SetActive(false);
    } */

    public void R1_Result2_2()
    {
        incounter1.transparency.SetActive(false);
        incounter1.Result1();
        incounter1.isWaitingForInput = true;
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
        incounter2.Select2(); 
        incounter2.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }

    public void R2_Select2()
    {
        incounter2.Select3();
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
        incounter4.Incounter2(); 
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
        incounter4.Result2(); 
        incounter4.isWaitingForInput = true;
        incounter4.bifurcation = 1;
        selectBtn.SetActive(false);
    }
    public void R5_Select()
    {
        incounter5.Incounter2(); 
        incounter5.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }
    public void R5_Result1()
    {
        incounter5.Result1();
        incounter5.isWaitingForInput = true;
        incounter5.bifurcation = 0;
        selectBtn.SetActive(false);
    }

    public void R5_Result2()
    {
        incounter5.Result2(); 
        incounter5.isWaitingForInput = true;
        incounter5.bifurcation = 1;
        selectBtn.SetActive(false);
    }

    public void R6_Select()
    {
        incounter6.Incounter2(); 
        incounter6.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }
    public void R6_Result1()
    {
        incounter6.Result1();
        incounter6.isWaitingForInput = true;
        incounter6.bifurcation = 0;
        selectBtn.SetActive(false);
    }

    public void R6_Result2()
    {
        incounter6.Result2(); 
        incounter6.isWaitingForInput = true;
        incounter6.bifurcation = 1;
        selectBtn.SetActive(false);
    }

    public void R7_Select1()
    {
        incounter7.transparency.SetActive(false);
        incounter7.Incounter1(); // TextController의 Select1 메서드 호출
        incounter7.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }

    public void R7_Select2()
    {
        incounter7.transparency.SetActive(false);
        incounter7.Select1(); // TextController의 Select1 메서드 호출
        incounter7.isWaitingForInput = true;
        selectBtn.SetActive(false);
    }
    public void R7_Result1()
    {
        incounter7.transparency.SetActive(false);
        incounter7.Result1();
        incounter7.isWaitingForInput = true;
        incounter7.bifurcation = 0;
        selectBtn.SetActive(false);
    }
    public void R7_Result2()
    {
        incounter7.transparency.SetActive(false);
        incounter7.Result2();
        incounter7.isWaitingForInput = true;
        incounter7.bifurcation = 1;
        selectBtn.SetActive(false);
    }
    public void R8_Result1()
    {
        incounter8.transparency.SetActive(false);
        incounter8.isWaitingForInput = true;
        incounter8.bifurcation = 0;
        selectBtn.SetActive(false);
    }
    public void R8_Result2()
    {
        incounter8.transparency.SetActive(false);
        incounter8.isWaitingForInput = true;
        incounter8.bifurcation = 1;
        selectBtn.SetActive(false);
    }

      public void R9_Result1()
    {
        incounter9.transparency.SetActive(false);
        incounter9.isWaitingForInput = true;
        incounter9.bifurcation = 0;
        selectBtn.SetActive(false);
    }
    public void R9_Result2()
    {
        incounter9.transparency.SetActive(false);
        incounter9.isWaitingForInput = true;
        incounter9.bifurcation = 1;
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
