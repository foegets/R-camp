using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallPlatform : MonoBehaviour
{
    BoxCollider2D fbox;
    Rigidbody2D rig;
    LayerMask layer;
    public float accumTime;
    public float fallTime;
    
    void Start()
    {
        fbox = GetComponent<BoxCollider2D>();
        rig = GetComponent<Rigidbody2D>();
        layer = LayerMask.GetMask("Player");
        accumTime = 0;
    }

    void Update()
    {
        if (fbox.IsTouchingLayers(layer))
        {
            accumTime += Time.deltaTime;
        }
       
        Fall();
        
    }
     void Fall()
    {
        if (accumTime.CompareTo(fallTime)>0)
        {
            Destroy(gameObject);
        }
    }
}
