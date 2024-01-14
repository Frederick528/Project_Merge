using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArtifactType { ATK, LUCK, CURSE }
public class ArtifactBundleData : MonoBehaviour
{
    // 아이템 이름, 형태, 아이콘, 설명을 담을 수 있는 구조
    // 아이템
    [System.Serializable]
    public class Artifact
    {
        public string ArtifactName;
        public ArtifactType ArtifactType;
        public Sprite ArtifactIcon;
        public string ArtifactDesc;
    }
    //// 세트로 구성된 아이템을 담을 수 있는 구조
    //// 세트아이템
    //[System.Serializable]
    //public class ArtifactBundle
    //{
    //    public int numberOfBundledItems;
    //    public Artifact[] artifacts;
    //}
    //// 아이템 세트를 담을 수 있는 구조 
    //// 세트아이템 리스트
    //public List<ArtifactBundle> artifactBundles;


}
