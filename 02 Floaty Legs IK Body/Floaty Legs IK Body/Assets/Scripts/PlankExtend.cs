using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankExtend : MonoBehaviour
{
   
    public KeyCode plankExtendKey;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(plankExtendKey))
        {
            GetComponent<Animator>().SetInteger("Extend", 1);
        }

    }
}

