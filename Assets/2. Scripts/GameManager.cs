using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool CardCanvasOn = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Screen.SetResolution(1920, 1080, true);
        DontDestroyOnLoad(gameObject);
        
        if (!CardDataDeserializer.TryGetData(1015, out CardData row))
        {
            Debug.Log("데이터 테이블을 불러오는 과정에서 문제가 발생했습니다.");
        }
        
    }
    
    private void Start()
    {
        
    }
}
