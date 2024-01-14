using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Artifact DataBase",
    menuName = "Scriptable Object/Artifact DataBase", order = int.MaxValue)]
public class ArtifactDB : ScriptableObject
{
    [SerializeField]
    private int artifactID; 
    public int ArtifactID { get { return artifactID; } }

    [SerializeField]
    private string artifactName; 
    public string ArtifactName { get { return artifactName; } }

    [SerializeField]
    private string artifactDesc;
    public string ArtifactDesc { get { return artifactDesc; } }

    [SerializeField]
    private int artifactType;
    public int ArtifactType { get { return artifactType; } }
}
