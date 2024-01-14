using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityChan;
using UnityEngine;

public class UnityChanController : Draggable
{
    // Start is called before the first frame update
    private SpringManager _spring;
    private IKLookAt _iKController;
    private Rigidbody _rigid;
    private Camera _mainCam;
    private Animator _anim;
    private bool _isMouseDown = false;

    public Transform eyeLine;
    public Transform lookPos;

    private void Awake()
    {
        _spring = GetComponent<SpringManager>();
        _iKController = GetComponent<IKLookAt>();
        _rigid = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
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
        transform.LookAt(Vector3.Lerp(new Vector3()
        {
            x = lookPos.position.x,
            y = targetPos.y,
            z = lookPos.position.z,
        }, targetPos, 0.4f));
        
        if (_isMouseDown) return;
        if (_rigid.velocity.magnitude > 0.0001f)
        {
            if (Physics.Raycast(transform.position, Vector3.down, 1))
            {
                _anim.SetTrigger("Land");
            }
            return;
        }
        
        // 2.8 은 약  2^(3/2)
        var distance = Vector3.Distance(this.transform.position, targetPos);
        if ( distance > 4f * Mathf.Log(v * 2.8f, 2))
        {
            var p = Vector3.Lerp(this.transform.position, targetPos, 0.005f * Mathf.Log(v * 2.8f, 2));
            var temp = (this.transform.position - p).magnitude > 0 ? (this.transform.position - p).magnitude : 0;
            //Debug.Log(temp);
            _anim.SetFloat("MoveSpd", temp / 1.5f);
                //Mathf.Lerp(_anim.GetFloat("MoveSpd"), temp, 0.1f));
            _rigid.MovePosition(p);
            
            if (Vector3.Distance(_iKController.lookAtObj.transform.position, eyeLine.position) > 0.01f)
                _iKController.lookAtObj.transform.position =
                    Vector3.Lerp(_iKController.lookAtObj.transform.position, eyeLine.position, 0.4f);
        }
        else
        {
            var f= _anim.GetFloat("MoveSpd");
            if(f > 0.001f)
                _anim.SetFloat("MoveSpd", 
                    Mathf.Lerp(f, 0 , 0.2f));
            
            _iKController.lookAtObj.transform.position =
                Vector3.Lerp(_iKController.lookAtObj.transform.position, target, 0.01f);
        }
    }
    
    private void OnMouseDown()
    {
        base.OnMouseDown();
        _isMouseDown = true;
        _rigid.isKinematic = true;
        if (_rigid.velocity.magnitude > 0.001f) return;
        this.transform.position += Vector3.up * 5;
        _anim.ResetTrigger("Land");
        _anim.SetTrigger("Hang");
    }

    private void OnMouseUp()
    {
        _rigid.isKinematic = false;
        _isMouseDown = false;
        //_anim.SetBool("isHang", false);
        _rigid.velocity = Vector3.down * 5;
    }
}
