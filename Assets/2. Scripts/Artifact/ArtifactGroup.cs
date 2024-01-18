using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactGroup : MonoBehaviour
{
    public static List<GameObject> artifactGroupPos = new();

    //private static int getArtifactCount = 0; 

    private void Start()
    {
        for (int i = 0; i < 12; i++)
        {
            artifactGroupPos.Add(transform.Find($"Artifact{i+1}").gameObject);
        }
    }
}
