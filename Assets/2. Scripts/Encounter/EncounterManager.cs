using System;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    private static Encounter _encounterCanvas;

    private static EncounterManager _instance;
    
    private void Awake()
    {
        _instance ??= this;
    }

    private void Start()
    {
        //Occur();
    }

    private static void SetUp()
    {
        if(_encounterCanvas == null)
            _encounterCanvas = Instantiate(
            Resources.Load<Encounter>("Prefabs/EncounterCanvas"), _instance.transform
            );
        else
        {
            _encounterCanvas.gameObject.SetActive(true);
        }
    }
    
    //인카운터를 발생시키는 메소드
    public static void Occur()
    {
        SetUp();
        
        _encounterCanvas.Init(() =>
        {
            Debug.Log("Accept");
            Resolve();
        },() =>
        {
            Debug.Log("Decline");
            Resolve();
        });
        
        _encounterCanvas.SetText(0, "창 너머 멀리 언덕 위로 아침해가 안개를 뚫고 솟아올라 고요한 초원을 비출 때\n잎새가 떨어진 버드나무 사이로 강물이 부드럽게 흘러오는 모습을 보고 있네.\n아, 이런 찬란한 자연도 이제 네게는 니스 칠로 죽어버린 그림처럼 보일 뿐이네.\n그 즐거운 광경이 이제는 내 가슴에서 단 한 방울의 행복감도 머리로 뿜어 올리지 못한다네. ");
        _encounterCanvas.SetText(1, "아! 베르테르");
        _encounterCanvas.SetText(2, "뭔 헛소리를 하는거야...");
    }


    public static void Occur(string [] texts, Action[] onClicks)
    {
        GameManager.CardCanvasOn = true;
        SetUp();
        
        _encounterCanvas.Init(() =>
        {
            onClicks[0]();
            Resolve();
        },() =>
        {
            onClicks[1]();
            Resolve();
        });

        _encounterCanvas.SetText(0, texts[0]);
        _encounterCanvas.SetText(1, texts[1]);
        _encounterCanvas.SetText(2, texts[2]);
    }

    private static void Resolve()
    {
        _encounterCanvas.gameObject.SetActive(false);
        GameManager.CardCanvasOn = false;
    }
}