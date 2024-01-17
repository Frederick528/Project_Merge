using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class GetArtifact : MonoBehaviour
{
    public static List<GameObject> Artifacts = new();
    public static ReactiveProperty<int> artifactID = new();

    [SerializeField]
    private GameObject getArtifactWindow;
    [SerializeField]
    private Button getArtifactBtn;
    [SerializeField]
    private Image getArtifactImg;
    // Start is called before the first frame update

    private void Start()
    {
        artifactID.Subscribe(x =>
            getArtifactImg.sprite = Resources.Load<Sprite>($"Images/Artifact/{artifactID}")
            );
        getArtifactBtn.onClick.AddListener(() =>
        {
            AddArtifact();
            getArtifactWindow.SetActive(false);
        });
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            artifactID.Value = 9000;  //change value
            getArtifactWindow.SetActive(!getArtifactWindow.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            artifactID.Value++;
        }
    }
    private void AddArtifact()
    {
        if (!ReadSpreadSheet.TryGetData(artifactID.Value, out ArtifactData data))
            return;
        //print(data.Name);
        foreach (var item in ArtifactGroup.artifactGroupPos)
        {
            if (item.GetComponent<Image>().sprite.name == "UIMask")
            {
                item.GetComponent<Artifact>().ID = artifactID.Value;
                Artifacts.Add(item);
                //print(Artifacts.Count);
                break;
            }
        }
        foreach (var artifact in Artifacts)
        {
            if (artifact.GetComponent<Image>().sprite.name == "UIMask")
            {
                TextMeshProUGUI[] textMeshPro = artifact.GetComponentsInChildren<TextMeshProUGUI>(true);
                artifact.SetActive(true);
                artifact.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Artifact/{artifact.GetComponent<Artifact>().ID}");
                textMeshPro[0].text = data.Name;
                textMeshPro[1].text = data.Description;
                break;
            }
        }
        //artifact.transform.SetParent(this.transform);
        //artifact.transform.localPosition = Vector3.zero;
    }
}
