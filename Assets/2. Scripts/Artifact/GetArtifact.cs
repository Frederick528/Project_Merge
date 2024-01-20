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
                if (GameManager.Instance.ArtifactDict[9009])
                {
                    CoreController.ArtifactSubThirst -= 1;
                    //CoreController.Core.ThirstDifficulty -= 1;
                    CoreController.ThirstFluctuation.Value -= 1;
                    CoreController.ModifyDifficulty(0, +1);
                }
                else
                {
                    CoreController.ThirstFluctuation.Value -= (CoreController.ThirstFluctuation.Value == 0) ? 0 : 1;
                    CoreController.ArtifactAddThirst += 1;
                }
                break;
            case 9006:
                if (GameManager.Instance.ArtifactDict[9010])
                {
                    CoreController.ArtifactSubHunger -= 1;
                    //CoreController.Core.HungerDifficulty -= 1;
                    CoreController.HungerFluctuation.Value -= 1;
                    CoreController.ModifyDifficulty(+1, 0);
                }
                else
                {
                    CoreController.HungerFluctuation.Value -= (CoreController.HungerFluctuation.Value == 0) ? 0 : 1;
                    CoreController.ArtifactAddHunger += 1;
                }
                break;
            case 9007:
                break;
            case 9008:
                break;
            case 9009:
                if (GameManager.Instance.ArtifactDict[9005])
                {
                    CoreController.ThirstFluctuation.Value += (CoreController.ThirstFluctuation.Value == 0) ? 0 : 1;
                    CoreController.ArtifactAddThirst -= 1;
                }
                else
                {
                    CoreController.ArtifactSubThirst += 1;
                    //CoreController.Core.ThirstDifficulty += 1;
                    CoreController.ThirstFluctuation.Value += 1;
                    CoreController.ModifyDifficulty(0, -1);
                    coreController.StatUICanvas.statUI.Texts[6].text = (CoreController.Core.ThirstDifficulty + CoreController.ArtifactSubThirst != 0) ? (-CoreController.Core.ThirstDifficulty - CoreController.ArtifactSubThirst).ToString() : "";
                }
                break;
            case 9010:
                if (GameManager.Instance.ArtifactDict[9006])
                {
                    CoreController.HungerFluctuation.Value += (CoreController.HungerFluctuation.Value == 0) ? 0 : 1;
                    CoreController.ArtifactAddHunger -= 1;
                }
                else
                {
                    CoreController.ArtifactSubHunger += 1;
                    //CoreController.Core.HungerDifficulty += 1;
                    CoreController.HungerFluctuation.Value += 1;
                    CoreController.ModifyDifficulty(-1, 0);
                    coreController.StatUICanvas.statUI.Texts[5].text = (CoreController.Core.HungerDifficulty + CoreController.ArtifactSubHunger != 0) ? (-CoreController.Core.HungerDifficulty - CoreController.ArtifactSubHunger).ToString() : "";
                }
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
