//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class DataProcessing : MonoBehaviour
//{
//    public ArtifactDB artifactDB;
//    ArtifactBundleData artifactBundleData;
//    private void OnEnable()
//    {
//        // ArtifactBundleData.cs�� ������Ʈ�� ���� GameObject�� �����Ͽ� �ڽ����� ���Դϴ�.
//        GameObject ArtifactBundleDataGO = new GameObject("ArtifactBundle DB");
//        ArtifactBundleDataGO.transform.position = transform.position;
//        ArtifactBundleDataGO.transform.parent = transform;

//        // ������ ���ӿ�����Ʈ�� ArtifactBundleData ������Ʈ�� �߰�
//        artifactBundleData = ArtifactBundleDataGO.AddComponent<ArtifactBundleData>();

//        // ArtifactBundleData�� ����Ʈ�� �޸𸮸� �Ҵ�
//        artifactBundleData.artifactBundles = new List<ArtifactBundleData.ArtifactBundle>();

//        // ��Ʈ ������ ���� Ȯ��
//        // ��Ʈ �������� �������� 2���� �����Ǿ� 2�� ������
//        int artifactBundleCount = artifactDB.data.Split('\n').Length / 2;

//        // Ȯ�� �� ��Ʈ�������� ����ŭ �޸𸮸� �Ҵ�
//        for (int j = 0; j < artifactBundleCount; j++)
//        {
//            artifactBundleData.artifactBundles.Add(new ArtifactBundleData.ArtifactBundle());
//        }

//        // ���� �� ��Ʈ�������� Item�� ���� �޸� �Ҵ� ����
//        for (int i = 0; i < artifactBundleCount; i++)
//        {
//            //��Ʈ�����ۺ� ������ �迭 ����
//            artifactBundleData.artifactBundles[i].artifacts = new ArtifactBundleData.Artifact[2];

//            //�迭���� ������ ������ �޸� �Ҵ�
//            for (int j = 0; j < 2; j++)
//            {
//                artifactBundleData.artifactBundles[i].artifacts[j] = new ArtifactBundleData.Artifact();
//            }
//        }

//        // ��ũ���ͺ� ������Ʈ�� �Էµ� ���������͸� �ٺ��� ©�� string�迭�� �Ҵ�
//        string[] row = artifactDB.data.Split('\n');
//        string[] columns;

//        int rowSize = row.Length; // ���� ���� Ȯ��
//        int columnSize = row[0].Split(',').Length; // ���� ���� Ȯ��

//        int currentColumns = 0; // ���� ���� Ŀ��
//        int currentBundle = 0; // ���� ��Ʈ�������� Ŀ��

//        for (int j = 0; j < rowSize / 2; j++) // ��Ʈ�����ۺ� �ѹ��� ������ for ����
//        {
//            for (int k = 0; k < 2; k++) //��Ʈ�����۳� �������� �ѹ��� ������ for ����
//            {
//                columns = row[currentColumns].Split(','); // ���� ���� ������ ©�� string�迭�� �Ҵ�

//                //string�� ���ڸ� ItemType���� ����ȯ�Ͽ� �����͸� �Ҵ��Ѵ�.
//                artifactBundleData.artifactBundles[currentBundle].artifacts[k].ArtifactName = columns[1]; //�̸� �Ҵ�
//                artifactBundleData.artifactBundles[currentBundle].artifacts[k].ArtifactDesc = columns[2]; //��ũ���� �Ҵ�
//                artifactBundleData.artifactBundles[currentBundle].artifacts[k].ArtifactType = ConvertFromString(columns[3]);

//                //// ������ ���� ó��
//                //if (artifactDB.artifactIcon.Length > currentColumns)
//                //    artifactBundleData.artifactBundles[currentBundle].artifacts[k].ArtifactIcon = artifactDB.artifactIcon[currentColumns];

//                // �� Ŀ���� +1
//                if (currentColumns < rowSize)
//                    currentColumns++;
//            }
//            // ��Ʈ������ Ŀ�� +1
//            currentBundle++;
//        }
//    }

//    // string �Է��� �޾� ������ �Ľ��ϰ� �ٽ� ArtifactType ����ȯ �Ͽ� ����
//    ArtifactType ConvertFromString(string artifactTypeString)
//    {
//        return (ArtifactType)int.Parse(artifactTypeString);
//    }
//}
