using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBtn : MonoBehaviour
{
    public TextController textController; // TextController ��ũ��Ʈ ����
    public GameObject selectBtn1, selectBtn2;


    public void Select1()
    {
        textController.transparency.SetActive(false);
        textController.Select1(); // TextController�� Select1 �޼��� ȣ��
        textController.isWaitingForInput = true;
        selectBtn1.SetActive(false);
    }

    public void Select2()
    {
        textController.transparency.SetActive(false);
        textController.Select2(); 
        textController.isWaitingForInput = true;
        selectBtn1.SetActive(false);
        selectBtn2.SetActive(false);
    }

    public void Select3()
    {
        textController.transparency.SetActive(false);
        textController.Select3(); 
        textController.isWaitingForInput = true;
        selectBtn1.SetActive(false);
    }

    public void Result1()
    {
        textController.transparency.SetActive(false);
        textController.Result1(); 
        textController.isWaitingForInput = true;
        textController.bifurcation = 0;
        selectBtn1.SetActive(false);
        selectBtn2.SetActive(false);
    }

    public void Result2()
    {
        textController.transparency.SetActive(false);
        textController.Result2();
        textController.bifurcation = 1;
        textController.isWaitingForInput = true;
        selectBtn1.SetActive(false);
        selectBtn2.SetActive(false);
    }
}
