using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static CancellationTokenSource Cts;
    public static bool CardCanvasOn = false;

    public Dictionary<int, bool> ArtifactDict = new();
    public List<int> ObtainableArtifact = new();

    [SerializeField]
    public bool isTutorial = false;

    async void Awake()
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
        Cts = new CancellationTokenSource();
        await TokenRebuilder();
    }
    
    private void Start()
    {
       
        if (isTutorial)
        {
            Rect rect1 = new Rect(Screen.width - 120, Screen.height - 40, 100, 30);
        }
        else
        {
            SoundManager.instance.Play("Sounds/Bgm/StoryBgm", Sound.Bgm, 0.2f);
        }
    }

    private async UniTask TokenRebuilder()
    {
        while (true)
        {
            if (Cts.Token.IsCancellationRequested)
            {
                Cts.Cancel();
                Cts.Dispose();
                Thread.MemoryBarrier();

                Cts = new CancellationTokenSource();
            }

            if (this.gameObject == null) break;
            
            await UniTask.Delay(100);
        }
    }
}
