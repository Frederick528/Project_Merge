using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Artifact : MonoBehaviour
{
    private ArtifactData data;
    public ArtifactData Data { get { return data; } }

    public int ID;

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
        try
        {
            if (EventSystem.current.IsPointerOverGameObject() == false && ReadSpreadSheet.TryGetData(ID, out ArtifactData data))
            {
                print(data.Name);
            }
        }
        catch (Exception ex)
        {
            print("없습니다.");
        }
    }
}
