using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Encounter : MonoBehaviour
{
    public TMP_Text problemText;
    public TMP_Text[] choiceTexts;

    public Image problemImage;
    public Button[] choiceButton;

    private static Encounter _instance;
    
    private enum TextField
    {
        Problem,
        Accept,
        Decline
    }

    public void Init( Action accept, Action decline)
    {
        Bind();
        
        choiceButton[0].onClick.AddListener(new UnityAction(accept));
        choiceButton[1].onClick.AddListener(new UnityAction(decline));
    }

    public void SetText(int targetId, string content)
    {
        switch ((TextField)targetId)
        {
            case TextField.Problem:
                problemText.text = content;
                break;
            case TextField.Accept:
                choiceTexts[0].text = content;
                break;
            case TextField.Decline:
                choiceTexts[1].text = content;
                break;
        }
    }

    private void Bind()
    {
        problemImage = GetComponentInChildren<Image>();
        problemText = GetComponentInChildren<TMP_Text>();
        
        choiceButton = GetComponentsInChildren<Button>();
        choiceTexts = new TMP_Text [choiceButton.Length];
        for (int i = 0; i < choiceButton.Length; i++ )
        {
           
            var btn = choiceButton[i];
            choiceTexts[i] =  btn.GetComponentInChildren<TMP_Text>();
        }
    }
}