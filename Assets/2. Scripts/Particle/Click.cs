using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Click : MonoBehaviour
{
    public Animator anim;
    public RectTransform particleCanvas;
    Vector2 particleCanvasSize;


    private void Start()
    {
        particleCanvasSize = particleCanvas.sizeDelta / 2;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.transform.localPosition = new Vector3(Input.mousePosition.x - particleCanvasSize.x, Input.mousePosition.y - particleCanvasSize.y, 0);
            anim.Play("ClickAnimation", -1, 0);

        }
    }
}
