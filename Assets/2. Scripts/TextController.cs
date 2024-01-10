using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public Text myText;
    public GameObject Select;
    private int currentTextIndex = 0;
    private string[] textArray1 = { "����� �̰��� ��������� ��ü ���ӿ� ���� ���߽��ϴ�.",
                                   "���� ����� ��� ������ ���, ���� ���̿��� �� ���ڸ� ���� �ִ� ������ ��Ÿ�����ϴ�.",
                                   "������ΰ�? ��� ����ִ��� �𸣰ڳ�. �ϴ� �ݰ���. ���� �����.",
                                   "����� ���డ ��� �����ϳİ� ����� �;����� ����� ����ؼ� ���� �̾���ϴ�.",
                                   "���̰� �����̾�. ���� ���� ����ϱ�. 30�� �ڿ� �װ� ����ִٸ� ������� �ʰڴٸ�, �� ���� �װ� �׾��ִٸ� �� ��ü�� ���� �� ���ְھ",
                                   "�׳�� �����̶�� Ȳ�ݺ� ����� �ų��� �ڸ��� �����ϴ�.",
                                   "���� �ڿ� ã�ƿ��°� ���� ����ϱ��? ��ħ ���� ���� �� ������.. �� ����� �Ծ �ɱ��?"
                                   };
    private string[] textArray2 = { " �� ��� ����� ��ġ�� ���� ��ġ�� 100���� ����.", };
    private string[] textArray3 = { "Ȳ�� ����� ī��� ȹ���� �� �ִ�. * Ȳ�ݻ�� - ������� ���� , ������ ������ ��� �� ����.", };

    void Start()
    {
        myText.text = textArray1[0];
    }

    void Update()
    {
        Incounter1();
    }

    void UpdateText(string[] textArray)
    {
        // �迭 ���� Ȯ�� �� ������Ʈ
        if (currentTextIndex < textArray.Length)
        {
            myText.text = textArray[currentTextIndex];
        }
        //else
        //{
        //    Debug.Log("�� �̻� �ؽ�Ʈ�� �����!");
        //}
    }

    void Incounter1()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentTextIndex++;
            if (currentTextIndex < textArray1.Length)
            {
                print($"����: {textArray1.Length}");
                print(currentTextIndex);
                UpdateText(textArray1);
            }
            else
            {
                ShowSelect();
                return;
            }
        }
    }

    public void IncounterSelect1()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentTextIndex < textArray2.Length)
            {
                if (currentTextIndex < textArray2.Length)
                {
                    UpdateText(textArray2);
                }
            }
            else
            {
                Debug.Log("�� �̻� �ؽ�Ʈ�� �����!");
            }
        }
    }

    public void IncounterSelect2()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentTextIndex < textArray3.Length)
            {
                if (currentTextIndex < textArray3.Length)
                {
                    UpdateText(textArray3);
                }
            }
            else
            {
                Debug.Log("�� �̻� �ؽ�Ʈ�� �����!");
            }
        }
    }
    void ShowSelect()
    {
        // ��ȭ�� ������ �� Select ���� ������Ʈ�� ���̵��� ����
        Select.SetActive(true);
    }
}


