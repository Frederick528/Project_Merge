using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public Text questText;
    public GameObject Open, Close;
    public int count = 0;

    private void Start()
    {
        Open.SetActive(false);
        Close.SetActive(true);
    }
    private void Update()
    { 
            Guide();       
    }

    void Guide()
    {     
        if (count == 1) 
        {
            questText.text = "ȭ�鿡�� ī�带 �巡���Ͽ� 1�ܰ� ���� Ƽ���� ī�带 ȹ�� �� �� �ֽ��ϴ�.";
        }

        if (count == 2)
        {
            
            questText.text = "ī�� ���콺 ��Ŭ���Ͽ� ī���� ������ �� �� �ֽ��ϴ�.";
        }

        if (count == 3)
        {
            
            questText.text = "2Ƽ�� �̻��� ī�带 ��Ŭ�� �Ͽ� ī���� ���� ȭ�鿡�� " +
                             "�����ϱ� ��ư�� ������ ������ ī���� 1�ܰ� ���� Ƽ�� ��� 2���� �������� ȹ���մϴ�";
        }

        if (count == 4)
        {
            
            questText.text = "���� ī�带 ��Ŭ�� �Ͽ� ī���� ���� ȭ�鿡�� " +
                             "�Ա� ��ư�� ������ ����� ��ġ�� �����մϴ�.";
        }

        if (count == 5)
        {

            questText.text = "���� �������� �پ��� ����� �Ͼ�ϴ�. " +
                             "�÷��̾��� �������� ���� ���� ���� �Ͼ�ų� �������� �Ͼ �� ������ �����ϰ� ���ñ� �ٶ��ϴ�.";
        }
    }

    public void Open_Q()
    {
        Open.SetActive(true);
        Close.SetActive(false);
    }

    public void Close_Q()
    {
        Open.SetActive(false);
        Close.SetActive(true);
    }

    public void Click1 ()
    {
        count = 1;
        Guide();
    }

    public void Click2 () 
    {
        count = 2;
        Guide();
    }

    public void Click3 ()
    {
        count = 3;
        Guide();
    }

    public void Click4 ()
    {
        count = 4;
        Guide();
    }

    public void Click5 ()
    {
        count = 5;
        Guide();
    }
}
