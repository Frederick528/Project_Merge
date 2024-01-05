using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    public void Init(int level)
    {
        base.Init(level);
        _rigid.isKinematic = false;

        switch (cardType)
        {
            case CardType.Food:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/DarkGreen");
                break;
            case CardType.Water:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/Blue");
                break;
            case CardType.Object1:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/Red");
                break;
            case CardType.Object2:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/Purple");
                break;
            case CardType.Combination:
                GetComponent<MeshRenderer>().material = 
                    Resources.Load<Material>("Prefabs/Materials/White");
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
        var destroyTarget = new[] { t1.GetComponent<Card>(), t2.GetComponent<Card>() };

        //병합 분기
        if (destroyTarget[0].cardType == destroyTarget[1].cardType)
        {
            CardManager.Areas ??= GameObject.FindGameObjectsWithTag("Merge");
        
            var cardInstance = CardManager.CreateCard(level + 1, (int)cardType);
            CardManager.DestroyCard(destroyTarget);
        
            //cardInstance.transform.localScale = Vector3.one;
            cardInstance.transform.localPosition =
                Vector3.up * 2f ;
        }
    }
}
