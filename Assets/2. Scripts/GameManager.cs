using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool CardCanvasOn = false;

    public Dictionary<int, bool> ArtifactDict = new();
    
    [SerializeField]
    public bool isTutorial = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Screen.SetResolution(1920, 1080, true);
        DontDestroyOnLoad(gameObject);
        
        StartCoroutine(ReadSpreadSheet.LoadData("https://docs.google.com/spreadsheets/d/1CqNR2Rh_OIVe8n0CG8vC7YVpbNUn_-0rXeBab72gXvs", "A3:D14", 0));
        if (!CardDataDeserializer.TryGetData(1015, out CardData row))
        {
            Debug.Log("데이터 테이블을 불러오는 과정에서 문제가 발생했습니다.");
        }

        if (!isTutorial && !TutorialDataDeserializer.TryGetData(1001, out string[] data))
        {
            Debug.Log("데이터를 불러오는 과정에서 문제가 발생했습니다.");
        }
        
        YamlDeserializer.saveData.Init();
    }
    
    private void Start()
    {
        if (isTutorial)
        {
            Rect rect1 = new Rect(Screen.width - 120, Screen.height - 40, 100, 30);
        }
    }
}
