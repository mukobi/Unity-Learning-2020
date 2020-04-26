using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFall : MonoBehaviour
{
    public GameObject camera;
    private BoxCollider c;
    public float y = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        c = gameObject.GetComponent<Collider>() as BoxCollider;

        
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = camera.transform.localPosition;
        c.center = new Vector3(camPos[0], y, camPos[2]);
        //print(c.center);

        if (Input.GetKeyDown("r"))
        {
            gameObject.transform.position = new Vector3(0f, 0f, 0f);
        }



    }
}
