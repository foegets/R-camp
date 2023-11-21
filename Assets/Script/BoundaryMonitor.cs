using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryMonitor : MonoBehaviour
{
    public float Xmax = 10.0f;
    public float Xmin = -10.0f;
    public float Ymax = 5.0f;
    public float Ymin = -5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 fixpos = transform.position;
        if (transform.position.x > Xmax)
        {
            fixpos.x = Xmax;
        }
        if (transform.position.y > Ymax)
        {
            fixpos.y = Ymax;
        }
        if (transform.position.x < Xmin)
        {
            fixpos.x = Xmin;
        }
        if (transform.position.y < Ymin)
        {
            fixpos.y = Ymin;
        }
        transform.position = fixpos;
    }
}
