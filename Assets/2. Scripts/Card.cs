using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : Entity
{
    public enum CardType
    {
        Food,
        Water,
        Object1,
        Object2,
        Combination
    }

    public CardType cardType;

    private MeshRenderer renderer;
    // Start is called before the first frame update
    public void Init(int code)
    {
        base.Init(code);
        _rigid.isKinematic = false;
        

        switch (code)
        {
            case 101:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/Purple");
                break;
            case 102:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/Red");
                break;
            case 103:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/DarkGreen");
                break;
            case 104:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/Blue");
                break;
            default:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/Black");
                break;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        if (Input.GetMouseButtonUp(1))
            Debug.Log(true);
    }

    protected override void OnMerge(GameObject t1, GameObject t2)
    {
        CardManager.Areas ??= GameObject.FindGameObjectsWithTag("Merge");
        
        var cardInstance = CardManager.CreateCard();
        CardManager.DestroyCard(new [] { t1.GetComponent<Card>(), t2.GetComponent<Card>() });
        
        //cardInstance.transform.localScale = Vector3.one;
        cardInstance.transform.localPosition =
          Vector3.up * 2f ;
    }
}
