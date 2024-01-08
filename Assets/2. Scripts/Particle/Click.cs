using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Click : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update

    void OnMouseDown()
    {
        anim.Play("ClickAnimation", -1, 0);
    }
}
