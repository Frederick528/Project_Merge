using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Entity : Mergeable
{
    protected bool selectCard = false; 
    
    protected Rigidbody _rigid;
    // Start is called before the first frame update
    protected void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected void Update()
    { 
        
    }

    public virtual void InitEntity()
    {
        Init(0);
    }
    public virtual void UpdateEntity()
    {
        
    }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     // ���� ������ ī��� �浹�� ��ü�� ī���̰�, ī�峢�� ����� ���, ������ ī�� ��ġ ����
    //     if (selectCard && collision.transform.CompareTag("Card") && Mathf.Abs(collision.transform.position.magnitude - transform.position.magnitude) < 3f)
    //     {
    //         //transform.position = new Vector3(collision.transform.position.x, transform.position.y, collision.transform.position.z - 3f);
    //         //transform.SetParent(collision.transform);
    //         //selectCard = false;
    //     }
    //
    //     else if (collision.transform.CompareTag("Floor"))
    //     {
    //         selectCard = false;
    //     }
    // }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        _rigid.isKinematic = true;
    }

    public override void OnMouseUp()
    {
        base.OnMouseUp();
        _rigid.isKinematic = false;
        
    }

}
