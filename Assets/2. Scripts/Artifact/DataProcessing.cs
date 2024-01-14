//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class DataProcessing : MonoBehaviour
//{
//    public ArtifactDB artifactDB;
//    ArtifactBundleData artifactBundleData;
//    private void OnEnable()
//    {
//        // ArtifactBundleData.cs가 컨포넌트로 붙을 GameObject를 생성하여 자식으로 붙입니다.
//        GameObject ArtifactBundleDataGO = new GameObject("ArtifactBundle DB");
//        ArtifactBundleDataGO.transform.position = transform.position;
//        ArtifactBundleDataGO.transform.parent = transform;

//        // 생성한 게임오브젝트에 ArtifactBundleData 컨포넌트를 추가
//        artifactBundleData = ArtifactBundleDataGO.AddComponent<ArtifactBundleData>();

//        // ArtifactBundleData의 리스트에 메모리를 할당
//        artifactBundleData.artifactBundles = new List<ArtifactBundleData.ArtifactBundle>();

//        // 세트 아이템 숫자 확인
//        // 세트 아이템은 아이템이 2개로 구성되어 2로 나누기
//        int artifactBundleCount = artifactDB.data.Split('\n').Length / 2;

//        // 확인 된 세트아이템의 수만큼 메모리를 할당
//        for (int j = 0; j < artifactBundleCount; j++)
//        {
//            artifactBundleData.artifactBundles.Add(new ArtifactBundleData.ArtifactBundle());
//        }

//        // 생성 된 세트아이템의 Item에 대한 메모리 할당 과정
//        for (int i = 0; i < artifactBundleCount; i++)
//        {
//            //세트아이템별 아이템 배열 생성
//            artifactBundleData.artifactBundles[i].artifacts = new ArtifactBundleData.Artifact[2];

//            //배열내부 아이템 데이터 메모리 할당
//            for (int j = 0; j < 2; j++)
//            {
//                artifactBundleData.artifactBundles[i].artifacts[j] = new ArtifactBundleData.Artifact();
//            }
//        }

//        // 스크립터블 오브젝트에 입력된 엑셀데이터를 줄별로 짤라 string배열에 할당
//        string[] row = artifactDB.data.Split('\n');
//        string[] columns;

//        int rowSize = row.Length; // 행의 숫자 확인
//        int columnSize = row[0].Split(',').Length; // 열의 숫자 확인

//        int currentColumns = 0; // 현재 열의 커서
//        int currentBundle = 0; // 현재 세트아이템의 커서

//        for (int j = 0; j < rowSize / 2; j++) // 세트아이템별 한바퀴 돌리는 for 구문
//        {
//            for (int k = 0; k < 2; k++) //세트아이템내 아이템을 한바퀴 돌리는 for 구문
//            {
//                columns = row[currentColumns].Split(','); // 현재 행을 열별로 짤라 string배열에 할당

//                //string인 숫자를 ItemType으로 형변환하여 데이터를 할당한다.
//                artifactBundleData.artifactBundles[currentBundle].artifacts[k].ArtifactName = columns[1]; //이름 할당
//                artifactBundleData.artifactBundles[currentBundle].artifacts[k].ArtifactDesc = columns[2]; //디스크립션 할당
//                artifactBundleData.artifactBundles[currentBundle].artifacts[k].ArtifactType = ConvertFromString(columns[3]);

//                //// 아이콘 별도 처리
//                //if (artifactDB.artifactIcon.Length > currentColumns)
//                //    artifactBundleData.artifactBundles[currentBundle].artifacts[k].ArtifactIcon = artifactDB.artifactIcon[currentColumns];

//                // 열 커서를 +1
//                if (currentColumns < rowSize)
//                    currentColumns++;
//            }
//            // 세트아이템 커서 +1
//            currentBundle++;
//        }
//    }

//    // string 입력을 받아 정수로 파싱하고 다시 ArtifactType 형변환 하여 리턴
//    ArtifactType ConvertFromString(string artifactTypeString)
//    {
//        return (ArtifactType)int.Parse(artifactTypeString);
//    }
//}
