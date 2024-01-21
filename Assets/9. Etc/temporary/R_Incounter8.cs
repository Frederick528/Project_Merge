    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class R_Incounter8 : MonoBehaviour
    {
        public bool isWaitingForInput = true;
        public int bifurcation = 0;
        public Text myText, mytext2;
        public GameObject incounter, transparency, Character, select1;
        public Image canvasImage;
        public float fadeSpeed = 0.5f; // ������ �پ��� �ӵ�

        [SerializeField]
        Button nextBtn;
        [SerializeField]
        GameObject blockUI, closeBtn;

        int currentTextIndex, currentTextIndex1, currentTextIndex2, currentTextIndex3= 0;
        private string[] textArray1 = { "�ν����ν���.",
                                        "���ӿ��� �������� �ν��� �Ҹ��� ����.",
                                        "������ ���� �ִ�. ",
                                        "���� ���� ������ ����� ���� �ѾƳ��°� ���� �� ����." };

 
         private string[] R_CharacterName1 = { "", "", "", "����","","",""};
         private string[] R_CharacterName2 = { "", "", "", "����","",""};



        private string[] result1 = { "", "�������� ���� ������ ������.", "���׸��� ���� �������� �׸���..", " \"�ƾ�!!\"", "���డ �¾Ҵ�." , "����� ���ߴ١� ", "�׳��� ���ָ� �ް� �Ǿ���." };
        private string[] result2 = { "", "���� �������� �ɾ��..", "���� �ڿ��� ���� �Ĵٺ��� ���డ �־���.", "��! �ȳ�! ������ �ò� �ƴϾ��µ�.. ���׳�..?", "�׳�� ã���迡 ������ �ְڴٸ� ���� �ϳ��� �����ְ� ����.", "���� �� Ư���ϰ� ��¦�δ١�" };


        void Start()
        {
            myText.text = textArray1[0];
            closeBtn.SetActive(true);
            nextBtn.interactable = false;
            if (canvasImage == null)
                {
                    canvasImage = GetComponent<Image>();
                    if (canvasImage == null)
                    {
                        Debug.LogError("Image component not found.");
                        return;
                    }
                }
        }

        void Update()
        {
            if (currentTextIndex < textArray1.Length)
            {
                Incounter1();
            }
            else if (currentTextIndex >= textArray1.Length && currentTextIndex2 < result1.Length && bifurcation == 0)
            {
                Result1();
            }
            else if (currentTextIndex >= textArray1.Length && currentTextIndex3 < result2.Length && bifurcation == 1)
            { 
                Result2();
            }

            else
            {
                incounter.SetActive(false);
                nextBtn.interactable = true;
                blockUI.SetActive(false);
                closeBtn.SetActive(false);
                GameManager.CardCanvasOn = false;
            }
        }

        void UpdateText(string[] textArray)
        {
            // �迭 ���� Ȯ�� �� ������Ʈ
            if (currentTextIndex < textArray1.Length && isWaitingForInput)
            {
                myText.text = textArray1[currentTextIndex];
            }
            else if (currentTextIndex2 < result2.Length && isWaitingForInput && bifurcation == 0)
            {
                myText.text = result1[currentTextIndex2];
            }
            else if (currentTextIndex3 < result2.Length && isWaitingForInput && bifurcation == 1)
            {
                myText.text = result2[currentTextIndex3];
            }
            else
            {
                Debug.Log("Not Text");
            }
        }

        void UpdateText2(string[] textArray)
        {
        // �迭 ���� Ȯ�� �� ������Ʈ
        if (currentTextIndex2 < R_CharacterName1.Length && isWaitingForInput)
        {
            mytext2.text = R_CharacterName1[currentTextIndex2];
            if (mytext2.text == "����")
            {
                StartCoroutine(FadeIn());
            }
        }
        if (currentTextIndex3 < R_CharacterName2.Length && isWaitingForInput)
        {
            mytext2.text = R_CharacterName1[currentTextIndex3];
            if (mytext2.text == "����")
            {
                StartCoroutine(FadeIn());
            }
        }
        else
        {
            Debug.Log("Not Text");
        }
        }

        void Incounter1()
        {
            if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true)
            {
                currentTextIndex++;
                if (currentTextIndex < textArray1.Length)
                {
                    UpdateText(textArray1);
                }
                if (currentTextIndex >= textArray1.Length)
                {
                    transparency.SetActive(true);
                    select1.SetActive(true);
                    isWaitingForInput = false;
                }
            }
        }

        public void Result1()
        {
            if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 0)
            {
                currentTextIndex2++;
                if (currentTextIndex2 < result1.Length)
                {
                    UpdateText(result1);
                    UpdateText2(R_CharacterName1);
                }
                if (currentTextIndex2 >= result1.Length)
                {
                    transparency.SetActive(true);
                    isWaitingForInput = false;
                }

            }
        }

        public void Result2()
        {
            bifurcation = 1;

            if (Input.GetKeyDown(KeyCode.Space) && isWaitingForInput == true && bifurcation == 1)
            {
                currentTextIndex3++;
                if (currentTextIndex3 < result2.Length)
                {
                    UpdateText(result2);
                    UpdateText2(R_CharacterName2);
                }
                if (currentTextIndex3 >= result2.Length)
                {
                    transparency.SetActive(true);
                    isWaitingForInput = false;
                }
            }
        }
        IEnumerator FadeOut()
        {
            float targetAlpha = 0f;

            while (canvasImage.color.a >= targetAlpha)
            {
                float currentAlpha = canvasImage.color.a;
                currentAlpha -= fadeSpeed * Time.deltaTime;
                canvasImage.color = new Color(canvasImage.color.r, canvasImage.color.g, canvasImage.color.b, currentAlpha);

                yield return null;
            }
        }
        IEnumerator FadeIn()
        {
            float targetAlpha = 1f;

            while (canvasImage.color.a < targetAlpha)
            {
                float currentAlpha = canvasImage.color.a;
                currentAlpha += fadeSpeed * Time.deltaTime;
                canvasImage.color = new Color(canvasImage.color.r, canvasImage.color.g, canvasImage.color.b, currentAlpha);

                yield return null;
            }

            // ������ 1 �̻����� �ö��� ���� ó��
            Debug.Log("Image faded in completely.");
        }
    }
