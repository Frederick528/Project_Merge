using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCanvasOnClick : MonoBehaviour
{
    public Canvas exchangeCanvas; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Card"))
                {
                    exchangeCanvas.gameObject.SetActive(true);
                }
            }
        }
    }
}
