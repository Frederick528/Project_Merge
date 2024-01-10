using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBtn : MonoBehaviour
{
    public TextController textController; // TextController ��ũ��Ʈ ����
    public GameObject selectBtn1, selectBtn2;

    public void Select1()
    {
        Debug.Log("Button Clicked");
        textController.Select1(); // TextController�� NextText �޼��� ȣ��
        textController.isWaitingForInput = true;
        selectBtn1.SetActive(false);
    }

    public void Select2()
    {
        Debug.Log("Button Clicked");
        textController.Select2(); // TextController�� NextText �޼��� ȣ��
        textController.isWaitingForInput = true;
        selectBtn1.SetActive(false);
        selectBtn2.SetActive(false);
    }

    public void Select3()
    {
        Debug.Log("Button Clicked");
        textController.Select3(); // TextController�� NextText �޼��� ȣ��
        textController.isWaitingForInput = true;
        selectBtn1.SetActive(false);
    }

    public void Result1()
    {
        Debug.Log("Button Clicked");
        textController.Result1(); // TextController�� NextText �޼��� ȣ��
        textController.isWaitingForInput = true;
        selectBtn1.SetActive(false);
        selectBtn2.SetActive(false);
    }

    public void Result2()
    {
        Debug.Log("Button Clicked");
        textController.Result2(); // TextController�� NextText �޼��� ȣ��
        textController.isWaitingForInput = true;
        selectBtn1.SetActive(false);
        selectBtn2.SetActive(false);
    }
}
