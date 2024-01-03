using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : Entity
{
    // Start is called before the first frame update
    public void Init(int level)
    {
        base.Init(level);
        _rigid.isKinematic = false;
    }
    void Start()
    {
        base.Start();
        this.Init(0);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    protected override void OnMerge(GameObject t1, GameObject t2)
    {
        Debug.Log("Merged");
        CardManager.Areas ??= GameObject.FindGameObjectsWithTag("Merge");
        var cardInstance =
            Instantiate(this.gameObject, CardManager.Areas[1].transform, true);
        Destroy(t1); Destroy(t2);
        
        cardInstance.GetComponent<MeshRenderer>().material = 
            Resources.Load<Material>("Prefabs/Materials/Purple");
        //cardInstance.transform.localScale = Vector3.one;
        cardInstance.transform.localPosition =
          Vector3.up * 2f ;
    }
}
