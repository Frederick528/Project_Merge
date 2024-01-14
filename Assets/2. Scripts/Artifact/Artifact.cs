using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YamlDotNet.Core.Tokens;

public class Artifact : MonoBehaviour
{
    private ArtifactData data;
    public ArtifactData Data { get { return data; } }
    public int ID;

    private Camera _uiCamera;
    private Collider _collider;

    private bool _isMouseOver;
    //private bool _isMousePushed;

    private void Awake()
    {
        _uiCamera = FindObjectOfType<Camera>(); // �ش� ������Ʈ�� ���߰� �ִ� ī�޶�
        _collider = GetComponent<Collider>(); // ������Ʈ�� Collider
    }

        // Update is called once per frame
    void Update()
    {
        var ray = _uiCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        if (hit.collider != null && hit.collider == _collider && !_isMouseOver)
        {
            _isMouseOver = true;
            OnMouseEnter();
        }
        else if (hit.collider != _collider && _isMouseOver)
        {
            _isMouseOver = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            init();
        }
        try
        {
            if(!ReadSpreadSheet.TryGetData(ID, out ArtifactData data))
            {
            }
            else
            {
            }
        }

        catch (Exception ex)
        {
            
        }
    }
    public void init()
    {
        if (ReadSpreadSheet.TryGetData(ID, out ArtifactData data))
        {
            print("SD");
        }
    }
    private void OnMouseEnter()
    {
        if (ReadSpreadSheet.TryGetData(ID, out ArtifactData data))
        {
            print(data.Name);
        }
    }
}
