using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 摄像机跟随 : MonoBehaviour
{
    public Transform gobj;
    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate()
    {
        if (gobj != null)
        {
            transform.position = gobj.position;
            transform.rotation = gobj.rotation;
        }
    }
}
