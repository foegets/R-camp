using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public bool isGround;
    public float checkRaduis;
    public LayerMask groundLayer;
    

    // Start is called before the first frame update
    public void Update()
    {
        
    }
    public void Check()
    {
        //
        
        isGround = Physics2D.OverlapCircle(transform.position, checkRaduis, groundLayer);
    }
}
