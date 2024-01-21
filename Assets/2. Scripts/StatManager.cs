using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    public static StatManager instance;
    public PlayerCtrl playerCtrl;

    // 0 == main / 1 == expected / 2 == fluctuation
    public Image[] inGameHunger = new Image[3];
    public Image[] inGameThirst = new Image[3];
    public TMP_Text text;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
