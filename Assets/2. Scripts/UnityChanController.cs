using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityChan;
using UnityEngine;

public class UnityChanController : MonoBehaviour
{
    // Start is called before the first frame update
    private SpringManager _spring;
    private IKLookAt _iKController;
    private Rigidbody _rigid;
    private Camera _mainCam;

    public Transform eyeLine;
    public Transform lookPos;

    private void Awake()
    {
        _spring = GetComponent<SpringManager>();
        _iKController = GetComponent<IKLookAt>();
        _rigid = GetComponent<Rigidbody>();
        _mainCam = Camera.main;

        _iKController.lookAtObj = lookPos;
        _rigid.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void Update()
    {
        var target = _mainCam.transform.position;
        var targetPos = new Vector3(
            target.x,transform.position.y, target.z);
        var v = (_mainCam.fieldOfView / 30);

        var distance = Vector3.Distance(this.transform.position, targetPos);
        if ( distance > 4f * Mathf.Log(v * 4, 2))
        {
            _rigid.MovePosition(
                Vector3.Lerp(this.transform.position, targetPos, 0.005f * Mathf.Log(v * 4, 2)));
            
            _iKController.lookAtObj.transform.position =
                Vector3.Lerp(_iKController.lookAtObj.transform.position, eyeLine.position, 0.4f);
            
            transform.LookAt(targetPos);
        }
        else
        {
            _iKController.lookAtObj.transform.position =
                Vector3.Lerp(_iKController.lookAtObj.transform.position, target, 0.01f);
        }
        
        
    }
}
