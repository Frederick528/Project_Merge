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
        public float fadeSpeed = 0.5f; // 투명도가 줄어드는 속도

        [SerializeField]
        Button nextBtn;
        [SerializeField]
        GameObject blockUI, closeBtn;

        int currentTextIndex, currentTextIndex1, currentTextIndex2, currentTextIndex3= 0;
        private string[] textArray1 = { "부스럭부스럭.",
                                        "숲속에서 인의적인 부스럭 소리가 난다.",
                                        "동물일 수도 있다. ",
                                        "굳이 여기 들어오면 민폐니 돌로 쫓아내는게 좋을 것 같다." };

 
         private string[] R_CharacterName1 = { "", "", "", "마녀","","",""};
         private string[] R_CharacterName2 = { "", "", "", "마녀","",""};



        private string[] result1 = { "", "숲속으로 돌을 가볍게 던졌다.", "조그마한 돌은 포물선을 그리며..", " \"아야!!\"", "마녀가 맞았다." , "결과는 뻔했다… ", "그녀의 저주를 받게 되었다." };
        private string[] result2 = { "", "나는 숲속으로 걸어갔다..", "나무 뒤에서 빼꼼 쳐다보니 마녀가 있었다.", "엇! 안녕! 오늘은 올껀 아니었는데.. 들켰네..?", "그녀는 찾은김에 보상을 주겠다며 보석 하나를 던져주고 갔다.", "뭔가 좀 특이하게 반짝인다…" };


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
        if (currentTextIndex2 < R_CharacterName1.Length && isWaitingForInput)
        {
            mytext2.text = R_CharacterName1[currentTextIndex2];
            if (mytext2.text == "마녀")
            {
                StartCoroutine(FadeIn());
            }
        }
        if (currentTextIndex3 < R_CharacterName2.Length && isWaitingForInput)
        {
            mytext2.text = R_CharacterName1[currentTextIndex3];
            if (mytext2.text == "마녀")
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

            // 투명도가 1 이상으로 올라갔을 때의 처리
            Debug.Log("Image faded in completely.");
        }
    }
