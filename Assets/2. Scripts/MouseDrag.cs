using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnMouseDown()
    {
        selectCard = true;              // ī�� ���� ���� On
        transform.SetParent(null);      // �θ������Ʈ���� ����������
        rb.isKinematic = true;          // �߷� ���� �� �� ������Ʈ ���� ���� �� �޵��� ����
        transform.position = new Vector3(transform.position.x, 2f, transform.position.z);   // y�� 2��ŭ �ø���
    }

    void OnMouseDrag()
    {
        float distance = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = objPos;

    }

    private void OnMouseUp()
    {
        rb.isKinematic = false;
    }

}
