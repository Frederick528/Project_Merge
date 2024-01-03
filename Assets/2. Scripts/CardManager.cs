using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static GameObject[] Areas; // 0 - Merge | 1 - Export
    // Start is called before the first frame update
    void Start()
    {
        Areas = GameObject.FindGameObjectsWithTag("Merge");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
