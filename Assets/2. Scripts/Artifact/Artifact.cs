using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : MonoBehaviour
{
    public Dictionary<string, bool> artifactActivate = new Dictionary<string, bool>();
    // Start is called before the first frame update
    void Start()
    {
        artifactActivate.Add("test1", false);
        artifactActivate.Add("test2", false);
        artifactActivate.Add("test3", false);

    }

    // Update is called once per frame
    void Update()
    {
        if (artifactActivate["test1"])
        {
            // shop discount
        }
    }
    

}
