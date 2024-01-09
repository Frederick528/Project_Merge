using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//DoNotUsed.
public class MouseDrag : Entity
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnMouseDown()
    {
        selectCard = true;              // 카드 선택 상태 On
        transform.SetParent(null);      // 부모오브젝트에서 빠져나오기
        rb.isKinematic = true;          // 중력 영향 및 위 오브젝트 무게 영향 안 받도록 설정
        transform.position = new Vector3(transform.position.x, 2f, transform.position.z);   // y축 2만큼 올리기
    }

    protected virtual void OnMouseDrag()
    {
        float distance = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = objPos;

    }

    protected override void OnMerge(GameObject t1, GameObject t2)
    {
        throw new System.NotImplementedException();
    }
    

    public override void OnMouseUp()
    {
        rb.isKinematic = false;
    }

}
