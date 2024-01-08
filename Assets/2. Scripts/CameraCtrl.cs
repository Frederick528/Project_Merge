using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    float moveSpeed = 1000f;
    float zoomSpeed = 50f;

    //float height;
    //float width;
    [SerializeField]
    GameObject map;
    Vector3 mapSize;

    [SerializeField]
    Vector3 center;

    Camera mainCamera;

    Dictionary<float, float> confineX = new Dictionary<float, float>()
    {
        {100f, 6.78f}, {95f, 20.96f}, {90f, 33.85f},
        {85f, 45.66f}, {80f, 56.56f}, {75f, 66.69f},
        {70f, 76.16f}, {65f, 85.07f}, {60f,93.50f},
        {55f, 101.52f}, {50f, 109.18f}, {45f,116.53f},
        {40f, 123.62f}, {35f, 130.49f}, {30f,137.17f},
    };

    Dictionary<float, float> confineZ = new Dictionary<float, float>()
    {
        {100f, 30.47f}, {95f, 38.44f}, {90f, 45.68f},
        {85f, 52.32f}, {80f, 58.44f}, {75f, 64.13f},
        {70f, 69.46f}, {65f, 74.469f}, {60f,79.20f},
        {55f, 83.71f}, {50f, 88.01f}, {45f,92.14f},
        {40f, 96.13f}, {35f, 99.99f}, {30f,103.74f},
    };

    void Start()
    {
        mapSize = new Vector3 (map.transform.localScale.x, map.transform.localScale.y, map.transform.localScale.z);

        mainCamera = GetComponent<Camera>();
        //height = Camera.main.orthographicSize;
        //width = height * Screen.width / Screen.height;
    }

    void Update()
    {
        Move();
    }

    private void Zoom()
    {
        float distance = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed;
        if (distance != 0 && mainCamera.fieldOfView < 100 && mainCamera.fieldOfView > 30)
        {
            mainCamera.fieldOfView += distance;
        }
        else if (mainCamera.fieldOfView >= 100)
        {
            mainCamera.fieldOfView = 100;
            if (distance < 0)
            {
                mainCamera.fieldOfView += distance;
            }
        }
        else if (mainCamera.fieldOfView <= 30)
        {
            mainCamera.fieldOfView = 30;
            if (distance > 0)
            {
                mainCamera.fieldOfView += distance;
            }
        }
    }

    private void Move()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 pos = transform.position;
            pos.x += Input.GetAxis("Mouse X") * -moveSpeed; // 마우스 X 위치 * 이동 스피드
            pos.z += Input.GetAxis("Mouse Y") * -moveSpeed; // 마우스 Y 위치 * 이동 스피드

            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
        }
        Zoom();
        //float lx = (mapSize.x - width) * 1/mainCamera.fieldOfView;
        float lx = confineX[mainCamera.fieldOfView];
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        //float ly = mapSize.y - height;
        //float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        //float lz = (mapSize.z - height) * 1/mainCamera.fieldOfView;
        float lz = confineZ[mainCamera.fieldOfView];
        float clampz = Mathf.Clamp(transform.position.z, -lz + center.z, lz + center.z);

        transform.position = new Vector3(clampX, transform.position.y, clampz);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize);
    }
}
