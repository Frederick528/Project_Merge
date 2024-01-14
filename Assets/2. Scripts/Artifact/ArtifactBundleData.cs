using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArtifactType { ATK, LUCK, CURSE }
public class ArtifactBundleData : MonoBehaviour
{
    // ������ �̸�, ����, ������, ������ ���� �� �ִ� ����
    // ������
    [System.Serializable]
    public class Artifact
    {
        public string ArtifactName;
        public ArtifactType ArtifactType;
        public Sprite ArtifactIcon;
        public string ArtifactDesc;
    }
    //// ��Ʈ�� ������ �������� ���� �� �ִ� ����
    //// ��Ʈ������
    //[System.Serializable]
    //public class ArtifactBundle
    //{
    //    public int numberOfBundledItems;
    //    public Artifact[] artifacts;
    //}
    //// ������ ��Ʈ�� ���� �� �ִ� ���� 
    //// ��Ʈ������ ����Ʈ
    //public List<ArtifactBundle> artifactBundles;


}
