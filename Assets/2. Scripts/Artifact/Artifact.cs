using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Artifact : MonoBehaviour
{
    private ArtifactData data;
    public ArtifactData Data { get { return data; } }

    public int ID;

    private Image image;

    private void Start()
    {
        image = GetComponentsInChildren<Image>(true)[1];
    }
    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    init();
        //}
        //try
        //{
        //    if(!ReadSpreadSheet.TryGetData(ID, out ArtifactData data))
        //    {
        //    }
        //    else
        //    {
        //    }
        //}

        //catch (Exception ex)
        //{
            
        //}
    }
    //public void init()
    //{
    //    if (ReadSpreadSheet.TryGetData(ID, out ArtifactData data))
    //    {
    //        artifactGroup.AddArtifact(this);
    //    }
    //}
    private void OnMouseEnter()
    {
        //if (EventSystem.current.IsPointerOverGameObject() == false ReadSpreadSheet.TryGetData(ID, out ArtifactData data))
        //{
            image.gameObject.SetActive(true);
        //}

        //try
        //{
        //    //if (/*EventSystem.current.IsPointerOverGameObject() == false && */ReadSpreadSheet.TryGetData(ID, out ArtifactData data))
        //    //{
        //    //    image[1].gameObject.SetActive(true);
        //    //}
        //}
        //catch
        //{

        //}
    }

    private void OnMouseExit()
    {
        //if (ReadSpreadSheet.TryGetData(ID, out ArtifactData data))
        //{
            image.gameObject.SetActive(false);
        //}

        //try
        //{
        //    //if (ReadSpreadSheet.TryGetData(ID, out ArtifactData data))
        //    //{
        //    //    image[1].gameObject.SetActive(false);
        //    //}
        //}
        //catch
        //{

        //}
    }
}
