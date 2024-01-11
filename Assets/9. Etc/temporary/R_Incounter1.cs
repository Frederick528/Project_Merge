    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class R_Incounter1 : MonoBehaviour
    {
        public bool isWaitingForInput = true;
        public int bifurcation = 0;
        public Text myText, mytext2;
        public GameObject incounter, transparency, Character, select1;
        public Image canvasImage;
        public float fadeSpeed = 0.5f; // 투명도가 줄어드는 속도

        [SerializeField]
        Button nextBtn;
        [SerializeField]
        GameObject blockUI, closeBtn;

        int currentTextIndex, currentTextIndex1, currentTextIndex2, currentTextIndex3= 0;
        private string[] textArray1 = { "평화로우면서도 평화롭지 않은 하루이다..",
                                       "그러나 갑자기.. 닌자가 나타났다..!.", };

 
         private string[] CharacterName1 = { "", "닌자", "", ""};



        private string[] result1 = { "", "나를 못봐도 괜찮지만 나를 무시하는건 용서할 수 없다!!!", "닌자는 왼손에서 나온 수리검으로 나를 공격했다!", "스쳤지만.. 많이 따갑다..", "시선을 돌리니, 닌자가 사라졌다.." };
        private string[] result2 = { "", "고맙다.", "닌자는 자신이 들고있던 음식을 선물로 두고갔다.", "대체 닌자란 무엇일까.." };


        void Start()
        {
            myText.text = textArray1[0];
            mytext2.text = CharacterName1[0];
            nextBtn.interactable = false;
            //closeBtn.SetActive(true);
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
                //closeBtn.SetActive(false);
                GameManager.CardCanvasOn = false;
            }
        }

        void UpdateText(string[] textArray)
        {
            // 배열 길이 확인 후 업데이트
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
            // 배열 길이 확인 후 업데이트
            if (currentTextIndex3 < CharacterName1.Length && isWaitingForInput)
            {
                mytext2.text = CharacterName1[currentTextIndex3];
                if (mytext2.text == "닌자")
                {
                    StartCoroutine(FadeIn());
                }
                else
                {
                    StartCoroutine(FadeOut());
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
                    UpdateText2(CharacterName1);
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
                    UpdateText2(CharacterName1);
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

            // 투명도가 1 이상으로 올라갔을 때의 처리
            Debug.Log("Image faded in completely.");
        }
    }
