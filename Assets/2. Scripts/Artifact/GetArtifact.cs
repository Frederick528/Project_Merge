using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetArtifact : MonoBehaviour
{
    public static List<GameObject> Artifacts = new();
    public static int artifactID;
    [SerializeField]
    private Button getArtifactBtn;
    [SerializeField]
    private Image getArtifactImg;

    [SerializeField]
    private ArtifactGroup artifactGroup;
    // Start is called before the first frame update

    private void OnEnable()
    {
        getArtifactImg.sprite = Resources.Load<Sprite>($"Images/Artifact/{artifactID}");
    }
    // Update is called once per frame
    void Update()
    {
        getArtifactBtn.onClick.RemoveAllListeners();
        getArtifactBtn.onClick.AddListener(() =>
        {
            AddArtifact();
            this.gameObject.SetActive(false);
        });
    }
    private void AddArtifact()
    {
        if (!ReadSpreadSheet.TryGetData(artifactID, out ArtifactData data))
        {
            return;
        }
        print(data.Name);
        foreach (var item in artifactGroup.artifactGroupPos)
        {
            if (item.GetComponent<Image>().sprite.name == "UIMask")
            {
                item.GetComponent<Artifact>().ID = artifactID;
                Artifacts.Add(item);
                print(Artifacts.Count);
                break;
            }
        }
        foreach (var artifact in Artifacts)
        {
            artifact.SetActive(true);
            artifact.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Artifact/{artifact.GetComponent<Artifact>().ID}");
        }
        //artifact.transform.SetParent(this.transform);
        //artifact.transform.localPosition = Vector3.zero;
    }
}
