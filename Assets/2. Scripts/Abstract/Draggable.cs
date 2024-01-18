using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Draggable : MonoBehaviour
{
    protected static Vector3 _defPos;
    private static Vector3 _crntPos;

    private static readonly Vector3 _offset = Vector3.back * 2;

    protected virtual void OnMouseDown()
    {
        _defPos = this.transform.position;
    }

    protected virtual void OnMouseDrag()
    {
        float distance = Camera.main.WorldToScreenPoint(transform.position).z;

        var mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        _crntPos = Camera.main.ScreenToWorldPoint(mousePos);
        this.transform.position = _crntPos;
    }
    
}