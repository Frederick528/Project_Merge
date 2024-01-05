using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRightClick : MonoBehaviour
{
    public Vector3 targetPosition;
    public GameObject canvas;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    canvas.SetActive(!canvas.activeSelf);
                }
            } 
        }
    }
    //if (input.getmousebuttondown(1))
    //{

    //    ray ray = camera.main.screenpointtoray(input.mouseposition);
    //    raycasthit hit;


    //    if (physics.raycast(ray, out hit) && hit.collider.comparetag("card"))
    //    {
    //        debug.log("true object");
    //        hit.rigidbody.iskinematic = true;
    //        moveobjecttotargetposition(hit.transform.gameobject);
    //    }
    //}
}
    //void MoveObjectToTargetPosition(GameObject objToMove)
    //{
    //    objToMove.transform.position = targetPosition;
        
    //}



