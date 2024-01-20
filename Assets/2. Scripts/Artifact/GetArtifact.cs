using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class GetArtifact : MonoBehaviour
{
    //public ReactiveProperty<int> artifactID = new();

    [SerializeField]
    private GameObject getArtifactWindow;
    [SerializeField]
    private Button getArtifactBtn;
    [SerializeField]
    private Image getArtifactImg;

    private int artifactNum;
    public CoreController coreController;
    // Start is called before the first frame update

    private void Start()
    {
        //artifactID.Subscribe(x =>
        //    getArtifactImg.sprite = Resources.Load<Sprite>($"Images/Artifact/{artifactID}")
        //    );
        //getArtifactBtn.onClick.AddListener(() =>
        //{
        //    AddArtifact();
        //    getArtifactWindow.SetActive(false);
        //});
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetArtifactWindow(9005);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetArtifactWindow(9006);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SetArtifactWindow(9009);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            SetArtifactWindow(9010);
        }
    }
    public void AddArtifact(int artifactID)    // 아티팩트창에 아티팩트 추가
    {
        if (!ReadSpreadSheet.TryGetData(artifactID, out ArtifactData data))
            return;
        //print(data.Name);

        ActiveArtifact(artifactID);
        GameObject artifact = ArtifactGroup.artifactGroupPos[artifactNum];
        artifact.GetComponent<Artifact>().ID = artifactID;
        TextMeshProUGUI[] textMeshPro = artifact.GetComponentsInChildren<TextMeshProUGUI>(true);
        artifact.SetActive(true);
        artifact.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Artifact/{artifactID}");
        textMeshPro[0].text = data.Name;
        textMeshPro[1].text = data.Description;
        GameManager.Instance.ArtifactDict[artifactID] = true;
        artifactNum++;
    }
    
    public void SetArtifactWindow(int artifactID)   // 아티팩트 윈도우 창
    {
        getArtifactWindow.SetActive(!getArtifactWindow.activeSelf); // 나중에 true로 바꿀 것
        getArtifactImg.sprite = Resources.Load<Sprite>($"Images/Artifact/{artifactID}");
        getArtifactBtn.onClick.RemoveAllListeners();
        getArtifactBtn.onClick.AddListener(() =>
        {
            AddArtifact(artifactID);
            getArtifactWindow.SetActive(false);
        });
    }

    public void ActiveArtifact(int artifactID)
    {
        switch (artifactID)
        {
            case 9000:
                break;
            case 9001:
                break;
            case 9002:
                break;
            case 9003:
                break;
            case 9004:
                break;
            case 9005:
                CoreController.ThirstFluctuation.Value -= (CoreController.ThirstFluctuation.Value == 0) ? 0 : 1;
                CoreController.ArtifactThirst += 1;
                break;
            case 9006:
                CoreController.HungerFluctuation.Value -= (CoreController.HungerFluctuation.Value == 0) ? 0 : 1;
                CoreController.ArtifactHunger += 1;
                break;
            case 9007:
                break;
            case 9008:
                break;
            case 9009:
                coreController.StatUICanvas.statUI.Thirst[1].fillAmount -= 0.01f;
                coreController.StatUICanvas.statUI.Thirst[2].fillAmount -= 0.01f;
                break;
            case 9010:
                coreController.StatUICanvas.statUI.Hunger[1].fillAmount -= 0.01f;
                coreController.StatUICanvas.statUI.Hunger[2].fillAmount -= 0.01f;
                break;
            case 9011:
                break;
        }
    }

    //foreach (var item in ArtifactGroup.artifactGroupPos)
    //{
    //    print(item);
    //    if (item.GetComponent<Image>().sprite == null)
    //    {
    //        item.GetComponent<Artifact>().ID = artifactID.Value;
    //        Artifacts.Add(item);
    //        //print(Artifacts.Count);
    //        break;
    //    }
    //}
    //foreach (var artifact in Artifacts)
    //{
    //    print(artifact);
    //    if (artifact.GetComponent<Image>().sprite == null)
    //    {
    //        TextMeshProUGUI[] textMeshPro = artifact.GetComponentsInChildren<TextMeshProUGUI>(true);
    //        artifact.SetActive(true);
    //        artifact.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Artifact/{artifact.GetComponent<Artifact>().ID}");
    //        textMeshPro[0].text = data.Name;
    //        textMeshPro[1].text = data.Description;
    //        break;
    //    }
    //}



    //artifact.transform.SetParent(this.transform);
    //artifact.transform.localPosition = Vector3.zero;
}
