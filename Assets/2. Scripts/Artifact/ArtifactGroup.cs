using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactGroup : MonoBehaviour
{
    [SerializeField]
    private GameObject getArtifactWindow;
    [SerializeField]
    private GameObject artifactWindow;

    public List<GameObject> artifactGroupPos;

    //private static int getArtifactCount = 0; 

    private void Start()
    {
        for (int i = 0; i < 12; i++)
        {
            artifactGroupPos.Add(GetComponentsInChildren<Artifact>()[i].gameObject);
        }
    }


    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    foreach (var artifact in GetArtifact.Artifacts)
        //    {
        //        artifact.SetActive(true);
        //        artifact.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Artifact/{artifact.GetComponent<Artifact>().ID}");
        //    }
        //}
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetArtifact.artifactID = 9000;  //change value
            getArtifactWindow.SetActive(!getArtifactWindow.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            artifactWindow.SetActive(!artifactWindow.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetArtifact.artifactID++;
        }
    }

}
