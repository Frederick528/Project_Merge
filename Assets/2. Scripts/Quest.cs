using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public Text questText;
    public GameObject Open, Close;
    public int count = 0;

    public Image targetImage1;
    public Image targetImage2;// ������ �̹����� �ִ� Image ������Ʈ
    public Sprite sprite1; 
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;
    public Sprite sprite6;
    public Sprite sprite7;
    public Sprite sprite8;
    public Sprite sprite9;
    public Sprite sprite10;
    public Sprite sprite11;
    public Sprite sprite12;
    public Sprite sprite13;
    public Sprite sprite14;

    private Sprite defaultSprite; // �⺻ ��������Ʈ ����

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
            targetImage1.sprite = sprite1;
            targetImage2.sprite = sprite2;
        }

        if (count == 2)
        {

            questText.text = "ī�� ���콺 ��Ŭ���Ͽ� ī���� ������ �� �� �ֽ��ϴ�.";
            targetImage1.sprite = sprite3;
            targetImage2.sprite = sprite4;
        }

        if (count == 3)
        {

            questText.text = "2Ƽ�� �̻��� ī�带 ��Ŭ�� �Ͽ� ī���� ���� ȭ�鿡�� " +
                             "�����ϱ� ��ư�� ������ ������ ī���� 1�ܰ� ���� Ƽ�� ��� 2���� �������� ȹ���մϴ�";
            targetImage1.sprite = sprite5;
            targetImage2.sprite = sprite6;
        }

        if (count == 4)
        {

            questText.text = "���� ī�带 ��Ŭ�� �Ͽ� ī���� ���� ȭ�鿡�� " +
                             "�Ա� ��ư�� ������ �Դ� ����� �Բ� ����� ��ġ�� �����մϴ�.";
            targetImage1.sprite = sprite7;
            targetImage2.sprite = sprite8;
        }

        if (count == 5)
        {

            questText.text = "���� �������� �پ��� ����� �Ͼ�ϴ�. " +
                             "�÷��̾��� �������� ���� ���� ���� �Ͼ�ų� �������� �Ͼ �� ������ �����ϰ� ���ñ� �ٶ��ϴ�.";
            targetImage1.sprite = sprite9;
            targetImage2.sprite = sprite10;
        }
        if (count == 6)
        {
            questText.text = "ī�� �̱� ��ư�� ������ �ൿ�� 1�� �Ҹ��ϰ� 1Ƽ�� ��� ī�带 ȹ�� �� �� �ֽ��ϴ�." +
                             "�ൿ���� 0�� �Ǹ� ī�带 ���� �� �����ϴ�. ";
            targetImage1.sprite = sprite11;
            targetImage2.sprite = sprite12;
        }

        if (count == 7)
        {
            questText.text = "�� �ѱ�� ��ư�� ������ ������� ��ħ, ����, ����, ���� ������ ����Ǹ� " +
                             "������ ������ ���� �ϼ��� 1�� �����մϴ�";
            targetImage1.sprite = sprite13;
            targetImage2.sprite = sprite14;
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

    public void Click1()
    {
        count = 1;
        Guide();
    }

    public void Click2()
    {
        count = 2;
        Guide();
    }

    public void Click3()
    {
        count = 3;
        Guide();
    }

    public void Click4()
    {
        count = 4;
        Guide();
    }

    public void Click5()
    {
        count = 5;
        Guide();
    }

    public void Click6()
    {
        count = 6;
        Guide();
    }

    public void Click7()
    {
        count = 7;
        Guide();
    }
}
