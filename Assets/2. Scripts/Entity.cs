using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected bool selectCard = false; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void InitEntity()
    {

    }
    public virtual void UpdateEntity()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 내가 선택한 카드랑 충돌한 물체가 카드이고, 카드끼리 가까울 경우, 선택한 카드 위치 조정
        if (selectCard && collision.transform.CompareTag("Card") && Mathf.Abs(collision.transform.position.magnitude - transform.position.magnitude) < 3f)
        {
            //transform.position = new Vector3(collision.transform.position.x, transform.position.y, collision.transform.position.z - 3f);
            //transform.SetParent(collision.transform);
            //selectCard = false;
        }

        else if (collision.transform.CompareTag("Floor"))
        {
            selectCard = false;
        }
    }

}
