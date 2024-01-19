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

    private Image[] image;

    private void Start()
    {
        image = GetComponentsInChildren<Image>(true);

        switch (ID)
        {
            case 9000:
                print("SD");
                break;
            case 9001:
                print("asdasd");
                break;
            case 9002:
                print("asd");
                break;
            case 9003:
                print("dwq");
                break;
            case 9004:
                print("xcz");
                break;
            case 9005:
                print("v");
                break;
            case 9006:
                print("sdvs");
                break;
            case 9007:
                StatManager.instance.playerCtrl.stat.maxAp += 1;
                StatManager.instance.playerCtrl.stat.curAp += 1;
                break;
            case 9008:
                print("iuy");
                break;
            case 9009:
                print(",mn");
                break;
            case 9010:
                print("jhg");
                break;
            case 9011:
                print("poi");
                break;
        }
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
        if (/*EventSystem.current.IsPointerOverGameObject() == false && */ReadSpreadSheet.TryGetData(ID, out ArtifactData data))
        {
            image[1].gameObject.SetActive(true);
        }
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
        if (ReadSpreadSheet.TryGetData(ID, out ArtifactData data))
        {
            image[1].gameObject.SetActive(false);
        }
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
