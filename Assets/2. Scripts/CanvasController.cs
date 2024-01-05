using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public GameObject canvas;

    void Start()
    {
        //Button closeButton = canvas.GetComponentsInChildren<Button>()[2];

        //closeButton.onClick.AddListener(() => CloseCanvas());
    }

    public void CloseCanvas()
    {
        canvas.SetActive(false);
        Debug.Log("캔버스가 닫혔습니다.");
    }

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;

    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            if (hit.collider.gameObject != canvas && canvas.activeSelf)
    //            {
    //                Debug.Log("클릭할 수 없습니다.");
    //                return;
    //            }
    //        }
    //    }
    //}
}

